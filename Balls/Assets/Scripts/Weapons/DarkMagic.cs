using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMagic : MonoBehaviour
{
    public GameObject pentagram;

    private bool canAttack = true;
    private GameObject curPentagram;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
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

    public void DealDamage(GameObject curTarget)
    {
        curTarget.transform.localScale -= new Vector3(settings.darkMagicDPS / 10000, settings.darkMagicDPS / 10000, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(9, settings.darkMagicDPS / 100);
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