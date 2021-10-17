using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private bool isReloading = false;
    private GameObject currentBullet;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bullet;
    public GameSettings settings;

    private void Start()
    {
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.cannonRange;
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
        damagedBall.transform.localScale -= new Vector3(settings.cannonDamage / 100, settings.cannonDamage / 100, 0);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.cannonReload);
        isReloading = false;
    }
}