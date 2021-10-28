using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int targetCount = 1;
    private float DPSIncrease = 0;
    private float rangeIncrease = 0;
    private List<GameObject> targets = new List<GameObject>();
    private List<GameObject> ballsInRadius = new List<GameObject>();
    private GameObject UISystem;

    public GameObject[] lasers;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        DPSIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[0];
        rangeIncrease = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[0];
        targetCount = 1 + UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[0];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.laserRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.laserRange + rangeIncrease, settings.laserRange + rangeIncrease, 1);
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100 && !targets.Contains(item) && targets.Count < targetCount)
                {
                    targets.Add(item);
                    break;
                }
            }
        }
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (!targets[i])
                {
                    targets.RemoveAt(i);
                    for (int j = lasers.Length - 1; j >= 0; j--)
                    {
                        if (lasers[j].activeInHierarchy)
                        {
                            lasers[j].SetActive(false);
                            break;
                        }
                    }
                }
                else
                {
                    targets[i].transform.localScale -= new Vector3((settings.laserDPS + DPSIncrease) / 10000, (settings.laserDPS + DPSIncrease) / 10000, 0);
                    UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(0, (settings.laserDPS + DPSIncrease) / 100);
                    if (!lasers[i].gameObject.activeInHierarchy)
                    {
                        lasers[i].gameObject.SetActive(true);
                    }
                    lasers[i].GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1f));
                    lasers[i].GetComponent<LineRenderer>().SetPosition(1, new Vector3(targets[i].transform.position.x, targets[i].transform.position.y, -1f));
                    lasers[i].GetComponent<LineRenderer>().endWidth = targets[i].transform.localScale.x / 5f;
                    if (targets[i].transform.localScale.x <= settings.ballMinHP / 100 || !ballsInRadius.Contains(targets[i]))
                    {
                        targets.Remove(targets[i]);
                        for (int j = lasers.Length - 1; j >= 0; j--)
                        {
                            if (lasers[j].activeInHierarchy)
                            {
                                lasers[j].SetActive(false);
                                break;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            foreach (var item in lasers)
            {
                if (item.activeInHierarchy)
                {
                    item.SetActive(false);
                }
            }
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
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.laserRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(1 + rangeIncrease, 1 + rangeIncrease, 1);
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.1f * level;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.laserRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(1 + rangeIncrease, 1 + rangeIncrease, 1);
    }

    public void UpgradeTargets()
    {
        targetCount++;
    }

    public void SetTargets(int level)
    {
        targetCount = 1 + level;
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