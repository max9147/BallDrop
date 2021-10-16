using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    private List<GameObject> damagedBalls = new List<GameObject>();

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") && !damagedBalls.Contains(collision.gameObject))
        {
            transform.parent.GetComponent<Sniper>().DealDamage(collision.gameObject);
            damagedBalls.Add(collision.gameObject);
        }
    }
}