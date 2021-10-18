using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    private bool canAttack = true;
    private GameObject curBee;
    public GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bee;
    public GameSettings settings;

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            if (!target)
            {
                target = ballsInRadius[Random.Range(0, ballsInRadius.Count)];
                if (target.transform.localScale.x <= settings.ballMinHP / 100)
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
        curBee = Instantiate(bee, transform.position, Quaternion.identity);
        curBee.GetComponent<Bee>().TakeAim(target);
        curBee.GetComponent<Bee>().SetParentHive(gameObject);
    }

    public void DealDamage(GameObject curTarget)
    {
        curTarget.transform.localScale -= new Vector3(settings.hiveDPS / 10000, settings.hiveDPS / 10000, 0);
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
        yield return new WaitForSeconds(settings.hiveSpawnTime);
        canAttack = true;
    }
}