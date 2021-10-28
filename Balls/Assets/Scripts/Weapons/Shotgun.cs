using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private bool isReloading = false;
    private int bulletCount;
    private int bulletIncrease = 0;
    private float damageIncrease = 0;
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
        damageIncrease = 0.2f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[13];
        speedIncrease = 0.2f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[13];
        bulletIncrease = UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[13];
        bulletCount = settings.shotgunBulletCount + bulletIncrease;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.shotgunRange;
        transform.Find("Radius").localScale = new Vector3(settings.shotgunRange, settings.shotgunRange, 1);
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
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            currentBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
            currentBullet.transform.up = target.transform.position - currentBullet.transform.position;
            currentBullet.transform.Rotate(0, 0, (-(bulletCount / 2) + i) * 15);
        }
        isReloading = true;
        StartCoroutine(Reload());
    }

    public void UpgradeDPS()
    {
        damageIncrease += 0.2f;
    }

    public void SetDPS(int level)
    {
        damageIncrease = 0.2f * level;
    }

    public void UpgradeSpeed()
    {
        speedIncrease += 0.2f;
    }

    public void SetSpeed(int level)
    {
        speedIncrease = 0.2f * level;
    }

    public void UpgradeBullets()
    {
        bulletIncrease++;
        bulletCount = settings.shotgunBulletCount + bulletIncrease;
    }

    public void SetBullets(int level)
    {
        bulletIncrease = level;
        bulletCount = settings.shotgunBulletCount + bulletIncrease;
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    public void DealDamage(GameObject target)
    {
        if (target)
        {
            target.transform.localScale -= new Vector3((settings.shotgunDamage + damageIncrease) / 100, (settings.shotgunDamage + damageIncrease) / 100, 0);
            UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(13, settings.shotgunDamage);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.shotgunReload - speedIncrease);
        isReloading = false;
    }
}