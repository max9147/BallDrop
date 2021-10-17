using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool isReloading = false;
    private int spikeCount;
    private GameObject curSpike;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject spike;
    public GameSettings settings;

    private void Start()
    {
        spikeCount = settings.spikesCount;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.spikesRange;
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100 && !item.transform.CompareTag("WeaponSpikes"))
                {
                    target = item;
                    break;
                }
            }
        }
        if (target)
        {
            if (target.transform.localScale.x <= settings.ballMinHP / 100 || !ballsInRadius.Contains(target))
            {
                target = null;
            }
            else if (!isReloading)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < spikeCount; i++)
        {
            curSpike = Instantiate(spike, transform.position, Quaternion.identity, transform);
            curSpike.transform.Rotate(0, 0, i * (360 / spikeCount));
        }
        isReloading = true;
        StartCoroutine(Reload());
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    public void DealDamage(GameObject damagedBall)
    {
        damagedBall.transform.localScale -= new Vector3(settings.spikesDamage / 100, settings.spikesDamage / 100, 0);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.spikesReload);
        isReloading = false;
    }
}