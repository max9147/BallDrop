using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour
{
    private bool isDecaying = true;
    private float poisonTime;
    private float damageIncrease = 0;
    private float timeIncrease = 0;
    private float damageBoost = 0;
    private GameObject curPoisonPS;
    private GameObject poisonPS;
    private GameObject UISystem;
    private GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        curPoisonPS = Instantiate(poisonPS, transform);
        damageIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[8];
        timeIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[8];
        damageBoost = UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[8];
        poisonTime = settings.poisonTime + timeIncrease;
    }

    private void FixedUpdate()
    {
        if (isDecaying)
        {
            poisonTime -= Time.deltaTime;
            if (poisonTime <= 0)
            {
                Destroy(curPoisonPS);
                Destroy(this);
            }
        }
        transform.localScale -= new Vector3((settings.poisonDPS + damageIncrease + (damageBoost * (1 - transform.localScale.x))) / 10000, (settings.poisonDPS + damageIncrease + (damageBoost * (1 - transform.localScale.x))) / 10000, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(8, (settings.poisonDPS + damageIncrease + (damageBoost * (1 - transform.localScale.x))) / 100);
    }

    public void SetSettings(GameSettings curSettings)
    {
        settings = curSettings;
    }

    public void SetPoisonPS(GameObject PS)
    {
        poisonPS = PS;
    }

    public void StartDecay()
    {
        isDecaying = true;
        poisonTime = settings.poisonTime + timeIncrease;
    }

    public void StopDecay()
    {
        isDecaying = false;
    }
}