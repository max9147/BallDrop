using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public GameObject bee;

    private bool canAttack = true;
    private GameObject curBee;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x < 0.05f)
                {
                    ballsInRadius.Remove(item);
                }
            }
        }
        if (ballsInRadius.Count > 0)
        {
            target = ballsInRadius[Random.Range(0, ballsInRadius.Count)];
        }
        if (target && canAttack)
        {
            Attack(target);
            canAttack = false;
            StartCoroutine(ResetStatus());
        }
    }

    private void Attack(GameObject attackTarget)
    {
        curBee = Instantiate(bee, transform.position, Quaternion.identity);
        curBee.GetComponent<Bee>().TakeAim(attackTarget);
        curBee.GetComponent<Bee>().SetParentHive(gameObject);
    }

    public void DealDamage(GameObject curTarget)
    {
        curTarget.transform.localScale -= new Vector3(0.0005f, 0.0005f, 0.0005f);
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    private IEnumerator ResetStatus()
    {
        yield return new WaitForSeconds(5f);
        canAttack = true;
    }
}