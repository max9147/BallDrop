using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    private bool isFiring = false;
    private float DPSIncrease = 0;
    private float rangeIncrease = 0;
    private float areaIncrease = 0;
    private GameObject currentTarget;
    private GameObject UISystem;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        DPSIncrease = 0.8f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[3];
        GetComponent<CapsuleCollider2D>().size = new Vector2(1 + 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[3], 2 + 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[3]);
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.5f + 0.1f *UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[3] / 2);
        var particleSettings = GetComponent<ParticleSystem>().main;
        particleSettings.startLifetime = 0.3f + (0.3f * 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[3]);
    }

    private void FixedUpdate()
    {
        if (isFiring)
        {
            transform.up = currentTarget.transform.position - transform.position;
            foreach (var item in ballsInRadius)
            {
                item.transform.localScale -= new Vector3((settings.flamethrowerDPS + DPSIncrease) / 10000, (settings.flamethrowerDPS + DPSIncrease) / 10000, 0);
                UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(3, (settings.flamethrowerDPS + DPSIncrease) / 100);
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

    public void UpgradeDPS()
    {
        DPSIncrease += 0.8f;
    }

    public void SetDPS(int level)
    {
        DPSIncrease = 0.8f * level;
    }

    public void UpgradeRange()
    {
        rangeIncrease += 0.1f;
        GetComponent<CapsuleCollider2D>().size = new Vector2(1 + areaIncrease, 2 + rangeIncrease);
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.5f + rangeIncrease / 2);
        var particleSettings = GetComponent<ParticleSystem>().main;
        particleSettings.startLifetime = 0.3f + (0.3f * rangeIncrease);
    }

    public void SetRange(int level)
    {
        rangeIncrease = 0.1f * level;
        GetComponent<CapsuleCollider2D>().size = new Vector2(1 + areaIncrease, 2 + rangeIncrease);
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.5f + rangeIncrease / 2);
        var particleSettings = GetComponent<ParticleSystem>().main;
        particleSettings.startLifetime = 0.3f + (0.3f * rangeIncrease);
    }

    public void UpgradeArea()
    {
        areaIncrease += 0.1f;
        GetComponent<CapsuleCollider2D>().size = new Vector2(1 + areaIncrease, 2 + rangeIncrease);
    }

    public void SetArea(int level)
    {
        areaIncrease = 0.1f * level;
        GetComponent<CapsuleCollider2D>().size = new Vector2(1 + areaIncrease, 2 + rangeIncrease);
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