using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private bool isAttacking = false;
    private float lifeTime;
    private GameObject currentTarget;
    private GameObject parentHive;

    private void Start()
    {
        lifeTime = 5f;
    }

    private void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0 || !currentTarget || !parentHive)
        {
            Destroy(gameObject);
        }
        if (currentTarget)
        {
            if (currentTarget.transform.localScale.x < 0.05f)
            {
                Destroy(gameObject);
            }
            if (Mathf.Abs(transform.position.x - currentTarget.transform.position.x) < 0.05f && Mathf.Abs(transform.position.y - currentTarget.transform.position.y) < 0.05f)
            {
                isAttacking = true;
            }
            if (isAttacking && parentHive)
            {
                parentHive.GetComponent<Hive>().DealDamage(currentTarget);
                transform.position = currentTarget.transform.position;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, 0.05f);
            }
        }
    }

    public void TakeAim(GameObject target)
    {
        currentTarget = target;
    }

    public void SetParentHive(GameObject hive)
    {
        parentHive = hive;
    }
}