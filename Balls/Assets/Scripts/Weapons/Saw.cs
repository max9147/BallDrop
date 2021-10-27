using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private float DPSIncrease = 0;
    private float rangeIncrease = 0;
    private float damageBoost = 0;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        DPSIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[10];
        rangeIncrease = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[10];
        damageBoost = UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[10];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.sawRange + rangeIncrease;
        transform.Find("Blade").localScale = new Vector3((settings.sawRange + rangeIncrease) * 0.8f, (settings.sawRange + rangeIncrease) * 0.8f, 0);
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100)
                {
                    DealDamage(item);
                    transform.Find("Blade").gameObject.SetActive(true);
                }
            }
        }
        else
        {
            transform.Find("Blade").gameObject.SetActive(false);
        }
    }

    public void UpgradeDPS()
    {
        DPSIncrease += 0.5f;
    }

    public void SetDPS(int level)
    {
        DPSIncrease = 0.5f * level;
    }

    public void UpgradeRange()
    {
        rangeIncrease += 0.1f;
        transform.Find("Blade").localScale = new Vector3((settings.sawRange + rangeIncrease) * 0.8f, (settings.sawRange + rangeIncrease) * 0.8f, 0);
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.1f * level;
        transform.Find("Blade").localScale = new Vector3((settings.sawRange + rangeIncrease) * 0.8f, (settings.sawRange + rangeIncrease) * 0.8f, 0);
    }

    public void UpgradeDamageBoost()
    {
        damageBoost++;
    }

    public void SetDamageBoost(int level)
    {
        damageBoost = level;
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
        damagedBall.transform.localScale -= new Vector3((settings.sawDPS + DPSIncrease + (damageBoost * (1 - damagedBall.transform.localScale.x))) / 10000, (settings.sawDPS + DPSIncrease + (damageBoost * (1 - damagedBall.transform.localScale.x))) / 10000, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(10, (settings.sawDPS + DPSIncrease + (damageBoost * (1 - damagedBall.transform.localScale.x))) / 100);
    }
}