using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour
{
    private bool isDecaying = true;
    private float poisonTime;
    private GameObject curPoisonPS;
    private GameObject poisonPS;
    private GameObject UISystem;
    private GameSettings settings;

    private void Start()
    {
        poisonTime = settings.poisonTime;
        curPoisonPS = Instantiate(poisonPS, transform);
        UISystem = GameObject.Find("UISystem");
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
        transform.localScale -= new Vector3(settings.poisonDPS / 10000, settings.poisonDPS / 10000, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(8, settings.poisonDPS / 100);
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
        poisonTime = settings.poisonTime;
    }

    public void StopDecay()
    {
        isDecaying = false;
    }
}