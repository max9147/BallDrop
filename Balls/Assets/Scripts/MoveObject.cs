using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Vector3 pos1;
    private Vector3 target;

    public GameObject pos2;

    private void Start()
    {
        pos1 = transform.position;
        target = pos2.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 0.01f);
        if (transform.position == target)
        {
            if (target == pos1)
            {
                target = pos2.transform.position;
            }
            else
            {
                target = pos1;
            }
        }
    }
}
