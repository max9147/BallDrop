using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private bool isReloading = false;
    private int bulletCount;
    private GameObject currentBullet;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bullet;
    public GameSettings settings;

    private void Start()
    {
        bulletCount = settings.shotgunBulletCount;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.shotgunRange;
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
            target.transform.localScale -= new Vector3(settings.shotgunDamage / 100, settings.shotgunDamage / 100, 0);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.shotgunReload);
        isReloading = false;
    }
}