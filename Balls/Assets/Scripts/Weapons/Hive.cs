using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    private bool canAttack = true;
    private float DPSIncrease = 0;
    private float speedIncrease = 0;
    private GameObject curBee;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject bee;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        DPSIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[4];
        speedIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[4];
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
        curBee = Instantiate(bee, transform.position, Quaternion.identity);
        curBee.GetComponent<Bee>().TakeAim(target);
        curBee.GetComponent<Bee>().SetParentHive(gameObject);
    }

    public void UpgradeDPS()
    {
        DPSIncrease += 0.5f;
    }

    public void SetDPS(int level)
    {
        DPSIncrease = 0.5f * level;
    }

    public void UpgradeSpeed()
    {
        speedIncrease += 0.5f;
    }

    public void SetSpeed(int level)
    {
        speedIncrease = 0.5f * level;
    }

    public void DealDamage(GameObject curTarget)
    {
        curTarget.transform.localScale -= new Vector3((settings.hiveDPS + DPSIncrease) / 10000, (settings.hiveDPS + DPSIncrease) / 10000, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(4, (settings.hiveDPS + DPSIncrease) / 100);
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    private IEnumerator ResetStatus()
    {
        yield return new WaitForSeconds(settings.hiveSpawnTime - speedIncrease);
        canAttack = true;
    }
}