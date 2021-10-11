using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float checkRadius;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    private void Start()
    {
        checkRadius = 1f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = checkRadius;
        ResetLaser();
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
            target.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
            transform.Find("Laser").GetComponent<LineRenderer>().SetPosition(1, new Vector3(target.transform.position.x, target.transform.position.y, -1f));
            transform.Find("Laser").GetComponent<LineRenderer>().endWidth = target.transform.localScale.x / 5f;
            if (target.transform.localScale.x < 0.05f || !ballsInRadius.Contains(target))
            {
                target = null;
                ResetLaser();
            }
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

    public void ResetLaser()
    {
        transform.Find("Laser").GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1f));
        transform.Find("Laser").GetComponent<LineRenderer>().SetPosition(1, new Vector3(transform.position.x, transform.position.y, -1f));
    }
}