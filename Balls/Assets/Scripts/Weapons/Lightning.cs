using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private bool canAttack = true;
    private float damageIncrease = 0;
    private float rangeIncrease = 0;
    private float speedIncrease = 0;
    private GameObject curBolt;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bolt;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        damageIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[6];
        rangeIncrease = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[6];
        speedIncrease = 0.05f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[6];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.lightningRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.lightningRange + rangeIncrease, settings.lightningRange + rangeIncrease, 1);
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            if (!target)
            {
                target = ballsInRadius[Random.Range(0, ballsInRadius.Count)];
                if (target.transform.localScale.x <= settings.ballMinHP / 100)
                {
                    target = null;
                }
            }
        }
        if (target && canAttack)
        {
            Attack();
            target = null;
            canAttack = false;
            StartCoroutine(ResetStatus());
        }
    }

    private void Attack()
    {
        target.transform.localScale -= new Vector3((settings.lightningDamage + damageIncrease) / 100, (settings.lightningDamage + damageIncrease) / 100, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(6, settings.lightningDamage + damageIncrease);
        curBolt = Instantiate(bolt, transform);
        curBolt.GetComponent<Bolt>().StartObject = gameObject;
        curBolt.GetComponent<Bolt>().EndObject = target;
        StartCoroutine(DestroyBolt(curBolt));
    }

    public void UpgradeDPS()
    {
        damageIncrease += 0.5f;
    }

    public void SetDPS(int level)
    {
        damageIncrease = 0.5f * level;
    }

    public void UpgradeRange()
    {
        rangeIncrease += 0.1f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.lightningRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.lightningRange + rangeIncrease, settings.lightningRange + rangeIncrease, 1);
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.1f * level;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.lightningRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.lightningRange + rangeIncrease, settings.lightningRange + rangeIncrease, 1);
    }

    public void UpgradeSpeed()
    {
        speedIncrease += 0.05f;
    }

    public void SetSpeed(int level)
    {
        speedIncrease = 0.05f * level;
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    private IEnumerator DestroyBolt(GameObject boltToDestroy)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(boltToDestroy);
    }

    private IEnumerator ResetStatus()
    {
        yield return new WaitForSeconds(settings.lightningReload - speedIncrease);
        canAttack = true;
    }
}