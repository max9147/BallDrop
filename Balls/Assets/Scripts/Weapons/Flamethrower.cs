using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    private float checkRadius;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    private void Start()
    {
        checkRadius = 1f;
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
            transform.Find("Flame").GetComponent<Flames>().StartFiring(target);
            if (target.transform.localScale.x <= 0.05f || !ballsInRadius.Contains(target))
            {
                target = null;
                transform.Find("Flame").GetComponent<Flames>().StopFiring();
            }
        }
        else if (transform.Find("Flame").GetComponent<Flames>().GetStatus())
        {
            transform.Find("Flame").GetComponent<Flames>().StopFiring();
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
}