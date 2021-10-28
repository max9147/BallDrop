using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    private float rangeIncrease = 0;
    private GameObject target;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        rangeIncrease = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[3];
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.flamethrowerRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.flamethrowerRange + rangeIncrease, settings.flamethrowerRange + rangeIncrease, 1);
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
        if (target)
        {
            transform.Find("Flame").GetComponent<Flames>().StartFiring(target);
            if (target.transform.localScale.x <= settings.ballMinHP / 100 || !ballsInRadius.Contains(target))
            {
                target = null;
                transform.Find("Flame").GetComponent<Flames>().StopFiring();
            }
        }
        else if (transform.Find("Flame").GetComponent<Flames>().GetStatus())
        {
            transform.Find("Flame").GetComponent<Flames>().StopFiring();
        }
    }

    public void UpgradeRange()
    {
        rangeIncrease += 0.1f;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.flamethrowerRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.flamethrowerRange + rangeIncrease, settings.flamethrowerRange + rangeIncrease, 1);
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.1f * level;
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.flamethrowerRange + rangeIncrease;
        transform.Find("Radius").localScale = new Vector3(settings.flamethrowerRange + rangeIncrease, settings.flamethrowerRange + rangeIncrease, 1);
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