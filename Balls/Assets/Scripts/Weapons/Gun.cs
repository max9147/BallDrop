using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool isReloading = false;
    private float checkRadius;
    private GameObject currentBullet;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bullet;

    private void Start()
    {
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
                Shoot(target);
            }            
        }
    }

    private void Shoot(GameObject target)
    {
        currentBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
        currentBullet.GetComponent<GunBullet>().TakeAim(target);
        isReloading = true;
        StartCoroutine("Reload");
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
            target.transform.localScale -= new Vector3(0.08f, 0.08f, 0.08f);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(3f);
        isReloading = false;
    }
}