using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocker : MonoBehaviour
{
    private bool isReloading = false;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.shockerRange;
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
            if (item.transform.localScale.x > settings.ballMinHP / 100)
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
        damagedBall.transform.localScale -= new Vector3(settings.shockerDamage / 100, settings.shockerDamage / 100, 0);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.shockerReload);
        isReloading = false;
    }

    private IEnumerator ResetStatus()
    {
        yield return new WaitForSeconds(0.2f);
        transform.Find("Shock").gameObject.SetActive(false);
    }
}