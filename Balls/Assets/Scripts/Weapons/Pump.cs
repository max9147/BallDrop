using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour
{
    private float DPSIncrease = 0;
    private float damageBoost = 0;
    private float timeIncrease = 0;
    private GameObject UISystem;
    private List<float> times = new List<float>();
    private List<GameObject> hoses = new List<GameObject>();
    private List<GameObject> targets = new List<GameObject>();

    public GameObject hose;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        DPSIncrease = 0.7f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[15];
        damageBoost = 0.7f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[15];
        timeIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[15];
    }

    private void FixedUpdate()
    {
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (!targets[i] || times[i] <= 0)
                {
                    targets.RemoveAt(i);
                    Destroy(hoses[i]);
                    hoses.RemoveAt(i);
                    times.RemoveAt(i);
                }
                else
                {
                    times[i] -= Time.deltaTime;
                    targets[i].transform.localScale -= new Vector3((settings.pumpDamage + DPSIncrease + (damageBoost * (1 - targets[i].transform.localScale.x))) / 10000, (settings.pumpDamage + DPSIncrease + (damageBoost * (1 - targets[i].transform.localScale.x))) / 10000, 0);
                    UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(15, settings.pumpDamage + DPSIncrease + damageBoost * (1 - targets[i].transform.localScale.x) / 100);
                    hoses[i].GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1f));
                    hoses[i].GetComponent<LineRenderer>().SetPosition(1, new Vector3(targets[i].transform.position.x, targets[i].transform.position.y, -1f));
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!targets.Contains(collision.gameObject))
        {
            targets.Add(collision.gameObject);
            hoses.Add(Instantiate(hose, transform));
            times.Add(4f + timeIncrease);
        }
    }

    public void UpgradeDPS()
    {
        DPSIncrease += 0.7f;
    }

    public void SetDPS(int level)
    {
        DPSIncrease = 0.7f * level;
    }

    public void UpgradeBoost()
    {
        damageBoost += 0.7f;
    }

    public void SetBoost(int level)
    {
        damageBoost = 0.7f * level;
    }

    public void UpgradeTime()
    {
        timeIncrease += 0.5f;
    }

    public void SetTime(int level)
    {
        timeIncrease = 0.5f * level;
    }
}