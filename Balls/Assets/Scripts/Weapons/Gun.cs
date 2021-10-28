using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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
        damageIncrease = UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[2];
        rangeIncrease = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[2];
        speedIncrease = 0.05f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[2];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.gunRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.gunRange + rangeIncrease, settings.gunRange + rangeIncrease, 1);
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100)
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
                Shoot(target);
            }            
        }
    }

    private void Shoot(GameObject target)
    {
        currentBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
        currentBullet.GetComponent<GunBullet>().TakeAim(target);
        isReloading = true;
        StartCoroutine(Reload());
    }

    public void UpgradeDPS()
    {
        damageIncrease++;
    }

    public void SetDPS(int level)
    {
        damageIncrease = level;
    }

    public void UpgradeRange()
    {
        rangeIncrease += 0.1f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.gunRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.gunRange + rangeIncrease, settings.gunRange + rangeIncrease, 1);
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.1f * level;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.gunRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.gunRange + rangeIncrease, settings.gunRange + rangeIncrease, 1);
    }

    public void UpgradeSpeed()
    {
        speedIncrease += 0.05f;
    }

    public void SetSpeed(int level)
    {
        speedIncrease = 0.05f * level;
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    public void DealDamage()
    {
        if (target)
        {
            target.transform.localScale -= new Vector3((settings.gunDamage + damageIncrease) / 100, (settings.gunDamage + damageIncrease) / 100, 0);
            UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(2, settings.gunDamage + damageIncrease);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.gunReload - speedIncrease);
        isReloading = false;
    }
}