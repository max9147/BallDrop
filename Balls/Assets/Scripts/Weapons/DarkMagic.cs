using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMagic : MonoBehaviour
{
    public GameObject pentagram;

    private bool canAttack = true;
    private GameObject curPentagram;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

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
        curPentagram = Instantiate(pentagram, target.transform.position, Quaternion.identity);
        curPentagram.GetComponent<Pentagram>().SetParentDarkMagic(gameObject);
    }

    public void DealDamage(GameObject curTarget)
    {
        curTarget.transform.localScale -= new Vector3(0.0006f, 0.0006f, 0.0006f);
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