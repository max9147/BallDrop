using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private bool isDetonating = false;
    private GameObject currentTarget;
    private Vector3 currentTargetPos;
    private List<GameObject> ballsInRange = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = settings.grenadesDamageRange;
    }

    private void FixedUpdate()
    {
        if (!isDetonating && currentTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPos, 0.05f);
            if (transform.position == currentTargetPos)
            {
                StartCoroutine(Detonate());
                isDetonating = true;
            }
        }
        if (isDetonating)
        {
            transform.Find("RadiusImage").transform.localScale += new Vector3(settings.grenadesDamageRange / 100, settings.grenadesDamageRange / 100, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") && !ballsInRange.Contains(collision.gameObject))
        {
            ballsInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") && ballsInRange.Contains(collision.gameObject))
        {
            ballsInRange.Remove(collision.gameObject);
        }
    }

    public void TakeAim(GameObject target)
    {
        currentTarget = target;
        currentTargetPos = target.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }

    private IEnumerator Detonate()
    {
        yield return new WaitForSeconds(1f);
        foreach (var item in ballsInRange)
        {
            transform.parent.gameObject.GetComponent<Grenades>().DealDamage(item);
        }
        Destroy(gameObject);
    }
}