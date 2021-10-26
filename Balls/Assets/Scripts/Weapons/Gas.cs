using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private bool isActive = false;
    private bool isDamaging = false;
    private float inactiveTime = 0f;
    private float DPSIncrease = 0;
    private float rangeIncrease = 0;
    private float damageBoost = 0;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        DPSIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[1];
        rangeIncrease = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[1];
        damageBoost = 1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[1];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.gasRange + rangeIncrease;
        var particleSettings = transform.Find("GasCloud").GetComponent<ParticleSystem>().main;
        particleSettings.startLifetime = settings.gasRange + (7f * rangeIncrease);
    }

    private void FixedUpdate()
    {
        if (isDamaging)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100)
                {
                    item.transform.localScale -= new Vector3((settings.gasDPS + DPSIncrease + (damageBoost * (1 - item.transform.localScale.x))) / 10000, (settings.gasDPS + DPSIncrease + (damageBoost * (1 - item.transform.localScale.x))) / 10000, 0);
                    UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(1, (settings.gasDPS + DPSIncrease + (damageBoost * (1 - item.transform.localScale.x))) / 100);
                }
            }
        }
        if (isActive)
        {
            inactiveTime = 0;
            if (!transform.Find("GasCloud").GetComponent<ParticleSystem>().isPlaying)
            {
                transform.Find("GasCloud").GetComponent<ParticleSystem>().Play();
                isDamaging = true;
            }
        }
        else if (transform.Find("GasCloud").GetComponent<ParticleSystem>().isPlaying)
        {
            inactiveTime += Time.deltaTime;
            if (inactiveTime >= 1f)
            {
                inactiveTime = 0;
                transform.Find("GasCloud").GetComponent<ParticleSystem>().Stop();
                transform.Find("GasCloud").GetComponent<ParticleSystem>().Clear();
                isDamaging = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isActive = true;
        inactiveTime = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isActive = true;
        inactiveTime = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isActive = false;
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
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.gasRange + rangeIncrease;
        var particleSettings = transform.Find("GasCloud").GetComponent<ParticleSystem>().main;
        particleSettings.startLifetime = settings.gasRange + (7f * rangeIncrease);
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.1f * level;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.gasRange + rangeIncrease;
        var particleSettings = transform.Find("GasCloud").GetComponent<ParticleSystem>().main;
        particleSettings.startLifetime = settings.gasRange + (7f * rangeIncrease);
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
}