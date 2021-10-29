using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocker : MonoBehaviour
{
    private bool isReloading = false;
    private float damageIncrease = 0;
    private float speedIncrese = 0;
    private float rangeIncrease = 0;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        damageIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[12];
        speedIncrese = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[12];
        rangeIncrease = 0.2f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[12];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.shockerRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.shockerRange + rangeIncrease, settings.shockerRange + rangeIncrease, 1);
        transform.Find("Shock").transform.localScale = new Vector3(0.6f + 0.15f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[12], 0.6f + 0.15f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[12], 1);
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100)
                {
                    target = item;
                    break;
                }
            }
        }
        if (target && !isReloading)
        {
            Shoot();
            target = null;
        }
    }

    private void Shoot()
    {
        transform.Find("Shock").gameObject.SetActive(true);
        StartCoroutine(ResetStatus());
        foreach (var item in ballsInRadius)
        {
            if (item.transform.localScale.x > settings.ballMinHP / 100)
            {
                DealDamage(item);
            }
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

    public void UpgradeSpeed()
    {
        speedIncrese += 0.1f;
    }

    public void SetSpeed(int level)
    {
        speedIncrese = 0.1f * level;
    }

    public void UpgradeRange()
    {
        rangeIncrease += 0.2f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.shockerRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.shockerRange + rangeIncrease, settings.shockerRange + rangeIncrease, 1);
        transform.Find("Shock").transform.localScale = new Vector3(0.6f + 0.15f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[12], 0.6f + 0.15f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[12], 1);
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.2f * level;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.shockerRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.shockerRange + rangeIncrease, settings.shockerRange + rangeIncrease, 1);        
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
        damagedBall.transform.localScale -= new Vector3((settings.shockerDamage + damageIncrease) / 100, (settings.shockerDamage + damageIncrease) / 100, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(12, settings.shockerDamage + damageIncrease);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.shockerReload - speedIncrese);
        isReloading = false;
    }

    private IEnumerator ResetStatus()
    {
        yield return new WaitForSeconds(0.2f);
        transform.Find("Shock").gameObject.SetActive(false);
    }
}