using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.sawRange;
        transform.Find("Blade").localScale = new Vector3(settings.sawRange * 0.8f, settings.sawRange * 0.8f, 0);
        UISystem = GameObject.Find("UISystem");
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
        damagedBall.transform.localScale -= new Vector3(settings.sawDPS / 10000, settings.sawDPS / 10000, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(10, settings.sawDPS / 100);
    }
}