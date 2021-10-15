using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
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
                if (item.transform.localScale.x > 0.05f && !item.transform.CompareTag("WeaponCannon"))
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
        currentBullet.GetComponent<CannonBullet>().TakeAim(target.transform.position);
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

    public void DealDamage(GameObject damagedBall)
    {
        damagedBall.transform.localScale -= new Vector3(0.15f, 0.15f, 0.15f);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(5f);
        isReloading = false;
    }
}