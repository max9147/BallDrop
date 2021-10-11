using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private bool isActive = false;
    private bool isDamaging = false;
    private float checkRadius;
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
                    item.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isActive = true;
        isDamaging = true;
        transform.Find("GasCloud").GetComponent<ParticleSystem>().Play();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isActive = false;
        StartCoroutine("WaitTimeGas");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isActive = true;
    }

    private void StopGas()
    {
        if (!isActive)
        {
            isDamaging = false;
            transform.Find("GasCloud").GetComponent<ParticleSystem>().Stop();
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

    private IEnumerator WaitTimeGas()
    {
        yield return new WaitForSeconds(0.5f);
        StopGas();
    }
}