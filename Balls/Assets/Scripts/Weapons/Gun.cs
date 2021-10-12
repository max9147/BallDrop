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
            if (currentBullet)
            {
                currentBullet.transform.position = Vector3.MoveTowards(currentBullet.transform.position, target.transform.position, 0.5f);
                if (currentBullet.transform.position == target.transform.position)
                {
                    Destroy(currentBullet);
                }
            }
            if (target.transform.localScale.x <= 0.05f || !ballsInRadius.Contains(target))
            {
                target = null;
            }
            else if (!isReloading)
            {
                Shoot(target);
            }            
        }
        else if (currentBullet)
        {
            Destroy(currentBullet);
        }
    }

    private void Shoot(GameObject target)
    {
        target.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        currentBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
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

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(4f);
        isReloading = false;
    }
}