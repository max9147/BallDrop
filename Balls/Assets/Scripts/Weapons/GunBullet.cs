using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private GameObject currentTarget;

    private void FixedUpdate()
    {
        if (currentTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, 0.2f);
            if (transform.position == currentTarget.transform.position)
            {
                transform.parent.GetComponent<Gun>().DealDamage();
                Destroy(gameObject);
            }
        }
    }

    public void TakeAim(GameObject target)
    {
        currentTarget = target;
    }
}