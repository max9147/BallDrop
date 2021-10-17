using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.laserRange;
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
            target.transform.localScale -= new Vector3(settings.laserDPS / 10000, settings.laserDPS / 10000, 0);   
            if (!transform.Find("Laser").gameObject.activeInHierarchy)
            {
                transform.Find("Laser").gameObject.SetActive(true);
            }
            transform.Find("Laser").GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1f));
            transform.Find("Laser").GetComponent<LineRenderer>().SetPosition(1, new Vector3(target.transform.position.x, target.transform.position.y, -1f));
            transform.Find("Laser").GetComponent<LineRenderer>().endWidth = target.transform.localScale.x / 5f;
            if (target.transform.localScale.x <= settings.ballMinHP / 100 || !ballsInRadius.Contains(target))
            {
                target = null;
                transform.Find("Laser").gameObject.SetActive(false);
            }
        }
        else if (transform.Find("Laser").gameObject.activeInHierarchy)
        {
            transform.Find("Laser").gameObject.SetActive(false);
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