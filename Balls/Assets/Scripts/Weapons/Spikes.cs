using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool isReloading = false;
    private int spikeCount;
    private float damageIncrease;
    private float spikeIncrease;
    private float speedIncrease;
    private GameObject curSpike;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject spike;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        spikeCount = settings.spikesCount;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.spikesRange;
        damageIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[7];
        spikeIncrease = 2 * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[7];
        speedIncrease = 0.2f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[7];
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
        for (int i = 0; i < spikeCount + spikeIncrease; i++)
        {
            curSpike = Instantiate(spike, transform.position, Quaternion.identity, transform);
            curSpike.transform.Rotate(0, 0, i * (360 / (spikeCount + spikeIncrease)));
        }
        isReloading = true;
        StartCoroutine(Reload());
    }

    public void UpgradeDPS()
    {
        damageIncrease += 0.5f;
    }

    public void SetDPS(int level)
    {
        damageIncrease = 0.5f * level;
    }

    public void UpgradeSpikes()
    {
        spikeIncrease += 2;
    }

    public void SetSpikes(int level)
    {
        spikeIncrease = 2 * level;
    }

    public void UpgradeSpeed()
    {
        speedIncrease += 0.2f;
    }

    public void SetSpeed(int level)
    {
        speedIncrease = 0.2f * level;
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
        damagedBall.transform.localScale -= new Vector3((settings.spikesDamage + damageIncrease) / 100, (settings.spikesDamage + damageIncrease) / 100, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(7, settings.spikesDamage + damageIncrease);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.spikesReload - speedIncrease);
        isReloading = false;
    }
}