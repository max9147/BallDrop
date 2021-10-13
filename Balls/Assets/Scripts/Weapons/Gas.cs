using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private bool isActive = false;
    private bool isDamaging = false;
    private float checkRadius;
    private float inactiveTime = 0f;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    private void Start()
    {
        checkRadius = 1f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = checkRadius;
    }

    private void FixedUpdate()
    {
        if (isDamaging)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > 0.05f)
                {
                    item.transform.localScale -= new Vector3(0.0005f, 0.0005f, 0.0005f);
                }
            }
        }
        if (isActive)
        {
            inactiveTime = 0;
            if (!transform.Find("GasCloud").GetComponent<ParticleSystem>().isPlaying)
            {
                transform.Find("GasCloud").GetComponent<ParticleSystem>().Play();
                isDamaging = true;
            }
        }
        else if (transform.Find("GasCloud").GetComponent<ParticleSystem>().isPlaying)
        {
            inactiveTime += Time.deltaTime;
            if (inactiveTime >= 1f)
            {
                inactiveTime = 0;
                transform.Find("GasCloud").GetComponent<ParticleSystem>().Stop();
                isDamaging = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isActive = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isActive = false;
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