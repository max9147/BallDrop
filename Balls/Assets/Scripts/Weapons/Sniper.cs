using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    private bool isReloading = false;
    private GameObject currentBullet;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bullet;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
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
        damagedBall.transform.localScale -= new Vector3(settings.sniperDamage / 100, settings.sniperDamage / 100, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(11, settings.sniperDamage);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.sniperReload);
        isReloading = false;
    }
}