using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    private List<GameObject> damagedBalls = new List<GameObject>();
    private Vector3 direction;

    private void FixedUpdate()
    {
        transform.Translate(direction * 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") && !damagedBalls.Contains(collision.gameObject))
        {
            transform.parent.GetComponent<Cannon>().DealDamage(collision.gameObject);
            damagedBalls.Add(collision.gameObject);
        }
    }

    public void TakeAim(Vector3 target)
    {
        direction = target - transform.position;
    }
}