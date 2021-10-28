using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    private bool isReloading = false;
    private float damageIncrease = 0;
    private float speedIncrease = 0;
    private GameObject currentGrenade;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject grenade;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        damageIncrease = UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[14];
        speedIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[14];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.grenadesRange;
        transform.Find("Radius").localScale = new Vector3(settings.grenadesRange, settings.grenadesRange, 1);
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100 && !item.transform.CompareTag("WeaponGrenades"))
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

    public void UpgradeDPS()
    {
        damageIncrease++;
    }

    public void SetDPS(int level)
    {
        damageIncrease = level;
    }
    
    public void UpgradeSpeed()
    {
        speedIncrease += 0.5f;
    }

    public void SetSpeed(int level)
    {
        speedIncrease = 0.5f * level;
    }

    private void Shoot()
    {
        currentGrenade = Instantiate(grenade, transform.position, Quaternion.identity, transform);
        currentGrenade.GetComponent<Grenade>().TakeAim(target);
        isReloading = true;
        StartCoroutine(Reload());
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
        damagedBall.transform.localScale -= new Vector3((settings.grenadesDamage + damageIncrease) / 100, (settings.grenadesDamage + damageIncrease) / 100, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(14, settings.grenadesDamage + damageIncrease);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.grenadesReload - speedIncrease);
        isReloading = false;
    }
}