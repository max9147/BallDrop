using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private float lifeTime;
    private List<GameObject> damagedBalls = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        lifeTime = settings.spikesLifeTime;
    }

    private void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.up * 0.08f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") && !damagedBalls.Contains(collision.gameObject))
        {
            transform.parent.GetComponent<Spikes>().DealDamage(collision.gameObject);
            damagedBalls.Add(collision.gameObject);
        }
    }
}