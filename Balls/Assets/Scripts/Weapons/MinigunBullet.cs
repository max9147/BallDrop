using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBullet : MonoBehaviour
{
    private GameObject currentTarget;

    private void FixedUpdate()
    {
        if (currentTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, 0.2f);
            if (transform.position == currentTarget.transform.position)
            {
                transform.parent.GetComponent<Minigun>().DealDamage();
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeAim(GameObject target)
    {
        currentTarget = target;
    }
}