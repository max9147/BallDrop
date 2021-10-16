using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private bool isReloading = false;
    private int bulletCount;
    private float checkRadius;
    private GameObject currentBullet;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bullet;

    private void Start()
    {
        bulletCount = 3;
        checkRadius = 2f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = checkRadius;
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > 0.05f)
                {
                    target = item;
                    break;
                }
            }
        }
        if (target)
        {
            if (target.transform.localScale.x <= 0.05f || !ballsInRadius.Contains(target))
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
            currentBullet.transform.Rotate(0, 0, (-(bulletCount / 2) + i) * 25);
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
            target.transform.localScale -= new Vector3(0.03f, 0.03f, 0.03f);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(3f);
        isReloading = false;
    }
}