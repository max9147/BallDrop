using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    private bool isFiring = false;
    private GameObject currentTarget;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
    }

    private void FixedUpdate()
    {
        if (isFiring)
        {
            transform.up = currentTarget.transform.position - transform.position;
            foreach (var item in ballsInRadius)
            {
                item.transform.localScale -= new Vector3(settings.flamethrowerDPS / 10000, settings.flamethrowerDPS / 10000, 0);
                UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(3, settings.flamethrowerDPS / 100);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballsInRadius.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ballsInRadius.Remove(collision.gameObject);
    }

    public bool GetStatus()
    {
        return isFiring;
    }

    public void StartFiring(GameObject target)
    {
        currentTarget = target;
        isFiring = true;
        GetComponent<ParticleSystem>().Play();
    }

    public void StopFiring()
    {
        isFiring = false;
        GetComponent<ParticleSystem>().Stop();
    }
}