using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMagic : MonoBehaviour
{
    private bool canAttack = true;
    private float damageIncrease = 0;
    private GameObject curPentagram;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject pentagram;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        damageIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[9];
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
        curPentagram = Instantiate(pentagram, target.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        curPentagram.GetComponent<Pentagram>().SetParentDarkMagic(gameObject);
    }

    public void UpgradeDPS()
    {
        damageIncrease += 0.5f;
    }

    public void SetDPS(int level)
    {
        damageIncrease = 0.5f * level;
    }

    public void DealDamage(GameObject curTarget)
    {
        curTarget.transform.localScale -= new Vector3((settings.darkMagicDPS + damageIncrease) / 10000, (settings.darkMagicDPS + damageIncrease) / 10000, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(9, (settings.darkMagicDPS + damageIncrease) / 100);
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
        yield return new WaitForSeconds(settings.darkMagicSpawnTime);
        canAttack = true;
    }
}