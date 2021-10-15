using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private bool canAttack = true;
    private float checkRadius;
    private GameObject curBolt;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bolt;

    private void Start()
    {
        checkRadius = 1.5f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = checkRadius;
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            if (!target)
            {
                target = ballsInRadius[Random.Range(0, ballsInRadius.Count)];
                if (target.transform.localScale.x <= 0.05f)
                {
                    target = null;
                }
            }
        }
        if (target && canAttack)
        {
            Attack();
            target = null;
            canAttack = false;
            StartCoroutine(ResetStatus());
        }
    }

    private void Attack()
    {
        target.transform.localScale -= new Vector3(0.025f, 0.025f, 0.025f);
        curBolt = Instantiate(bolt, transform);
        curBolt.GetComponent<Bolt>().StartObject = gameObject;
        curBolt.GetComponent<Bolt>().EndObject = target;
        StartCoroutine(DestroyBolt(curBolt));
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    private IEnumerator DestroyBolt(GameObject boltToDestroy)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(boltToDestroy);
    }

    private IEnumerator ResetStatus()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
}