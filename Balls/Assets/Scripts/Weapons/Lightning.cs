using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private bool canAttack = true;
    private GameObject curBolt;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bolt;
    public GameSettings settings;

    private void Start()
    {
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.lightningRange;
        UISystem = GameObject.Find("UISystem");
    }

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
        target.transform.localScale -= new Vector3(settings.lightningDamage / 100, settings.lightningDamage / 100, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(6, settings.lightningDamage);
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
        yield return new WaitForSeconds(settings.lightningReload);
        canAttack = true;
    }
}