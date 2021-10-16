using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocker : MonoBehaviour
{
    private bool isReloading = false;
    private float checkRadius;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    private void Start()
    {
        checkRadius = 0.8f;
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
        if (target && !isReloading)
        {
            Shoot();
            target = null;
        }
    }

    private void Shoot()
    {
        transform.Find("Shock").gameObject.SetActive(true);
        StartCoroutine(ResetStatus());
        foreach (var item in ballsInRadius)
        {
            if (item.transform.localScale.x > 0.05f)
            {
                DealDamage(item);
            }
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

    public void DealDamage(GameObject damagedBall)
    {
        damagedBall.transform.localScale -= new Vector3(0.03f, 0.03f, 0.03f);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        isReloading = false;
    }

    private IEnumerator ResetStatus()
    {
        yield return new WaitForSeconds(0.2f);
        transform.Find("Shock").gameObject.SetActive(false);
    }
}