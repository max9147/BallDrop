using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    private bool isFiring = false;
    private GameObject currentTarget;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    private void FixedUpdate()
    {
        if (isFiring)
        {
            transform.up = currentTarget.transform.position - transform.position;
            foreach (var item in ballsInRadius)
            {
                item.transform.localScale -= new Vector3(0.0008f, 0.0008f, 0.0008f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballsInRadius.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ballsInRadius.Remove(collision.gameObject);
    }

    public bool GetStatus()
    {
        return isFiring;
    }

    public void StartFiring(GameObject target)
    {
        currentTarget = target;
        isFiring = true;
        GetComponent<ParticleSystem>().Play();
    }

    public void StopFiring()
    {
        isFiring = false;
        GetComponent<ParticleSystem>().Stop();
    }
}