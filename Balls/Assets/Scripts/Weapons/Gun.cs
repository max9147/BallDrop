using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.gunRange;
        UISystem = GameObject.Find("UISystem");
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
            target.transform.localScale -= new Vector3(settings.gunDamage / 100, settings.gunDamage / 100, 0);
            UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(2, settings.gunDamage);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.gunReload);
        isReloading = false;
    }
}