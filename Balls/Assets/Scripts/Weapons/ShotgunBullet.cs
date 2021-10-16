using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            transform.parent.GetComponent<Shotgun>().DealDamage(collision.gameObject);
            Destroy(gameObject);
        }
    }
}