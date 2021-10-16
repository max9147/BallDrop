using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private float checkRadius;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    private void Start()
    {
        checkRadius = 1f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = checkRadius;
        transform.Find("Blade").localScale = new Vector3(checkRadius * 0.8f, checkRadius * 0.8f, 0);
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > 0.05f)
                {
                    DealDamage(item);
                    transform.Find("Blade").gameObject.SetActive(true);
                }
            }
        }
        else
        {
            transform.Find("Blade").gameObject.SetActive(false);
        }
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
        damagedBall.transform.localScale -= new Vector3(0.0015f, 0.0015f, 0.0015f);
    }
}