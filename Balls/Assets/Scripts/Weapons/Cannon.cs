using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private bool isReloading = false;
    private float damageIncrease = 0;
    private float rangeIncrease = 0;
    private float speedIncrease = 0;
    private GameObject currentBullet;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bullet;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        damageIncrease = 2 * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[5];
        rangeIncrease = 0.2f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[5];
        speedIncrease = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[5];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.cannonRange + rangeIncrease;
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100 && !item.transform.CompareTag("WeaponCannon"))
                {
                    target = item;
                    break;
                }
            }
        }
        if (target)
        {
            if (target.transform.localScale.x <= settings.ballMinHP / 100 || !ballsInRadius.Contains(target))
            {
                target = null;
            }
            else if (!isReloading)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        currentBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
        currentBullet.transform.up = target.transform.position - transform.position;
        isReloading = true;
        StartCoroutine(Reload());
    }

    public void UpgradeDPS()
    {
        damageIncrease += 2;
    }

    public void SetDPS(int level)
    {
        damageIncrease = 2 * level;
    }

    public void UpgradeRange()
    {
        rangeIncrease += 0.2f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.cannonRange + rangeIncrease;
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.2f * level;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.cannonRange + rangeIncrease;
    }

    public void UpgradeSpeed()
    {
        speedIncrease += 0.1f;
    }

    public void SetSpeed(int level)
    {
        speedIncrease = 0.1f * level;
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    public void DealDamage(GameObject damagedBall)
    {
        damagedBall.transform.localScale -= new Vector3((settings.cannonDamage + damageIncrease) / 100, (settings.cannonDamage + damageIncrease) / 100, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(5, settings.cannonDamage + damageIncrease);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.cannonReload - speedIncrease);
        isReloading = false;
    }
}