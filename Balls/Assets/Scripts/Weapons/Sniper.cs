using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    private bool isReloading = false;
    private float damageIncrease = 0;
    private float speedIncrease = 0;
    private float critChance = 0;
    private GameObject currentBullet;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bullet;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        damageIncrease = UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[11];
        speedIncrease = 0.2f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[11];
        critChance = 0.05f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[11];
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100 && !item.transform.CompareTag("WeaponSniper"))
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
        damageIncrease++;
    }

    public void SetDPS(int level)
    {
        damageIncrease = level;
    }

    public void UpgradeSpeed()
    {
        speedIncrease += 0.2f;
    }

    public void SetSpeed(int level)
    {
        speedIncrease = 0.2f * level;
    }

    public void UpgradeCrit()
    {
        critChance += 0.05f;
    }

    public void SetCrit(int level)
    {
        critChance = 0.05f * level;
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
        float checkCrit = Random.Range(0, 1);
        if (checkCrit < critChance)
        {
            damagedBall.transform.localScale -= new Vector3((settings.sniperDamage + damageIncrease) / 100 * 2, (settings.sniperDamage + damageIncrease) / 100 * 2, 0);
            UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(11, (settings.sniperDamage + damageIncrease) * 2);
        }
        else
        {
            damagedBall.transform.localScale -= new Vector3((settings.sniperDamage + damageIncrease) / 100, (settings.sniperDamage + damageIncrease) / 100, 0);
            UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(11, settings.sniperDamage + damageIncrease);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.sniperReload - speedIncrease);
        isReloading = false;
    }
}