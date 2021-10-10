using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFail : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Destroy(collision.gameObject);
        }
    }
}
