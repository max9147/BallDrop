using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virused : MonoBehaviour
{
    private float damageIncrease = 0;
    private float damageBoost = 0;
    private float timeIncrease = 0;
    private GameObject curVirusPS;
    private GameObject virusPS;
    private GameObject UISystem;
    private GameSettings settings;

    public bool isDecaying = true;
    public float virusTime;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        damageIncrease = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade1()[17];
        damageBoost = UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[17];
        timeIncrease = UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[17];
        curVirusPS = Instantiate(virusPS, transform);
    }

    private void FixedUpdate()
    {
        if (isDecaying)
        {
            virusTime -= Time.deltaTime;
            if (virusTime <= 0)
            {
                Destroy(curVirusPS);
                Destroy(this);
            }
        }
        transform.localScale -= new Vector3((settings.virusDamage + damageIncrease + (damageBoost * (1 - transform.localScale.x))) / 10000, (settings.virusDamage + damageIncrease + (damageBoost * (1 - transform.localScale.x))) / 10000, 0);
        UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(17, (settings.virusDamage + damageIncrease + (damageBoost * (1 - transform.localScale.x))) / 100);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            if (!collision.gameObject.GetComponent<Virused>())
            {
                collision.gameObject.AddComponent<Virused>();
                collision.gameObject.GetComponent<Virused>().SetVirusPS(virusPS);
                collision.gameObject.GetComponent<Virused>().SetSettings(settings);
            }
            else
            {
                collision.gameObject.GetComponent<Virused>().StopDecay();
            }
        }
    }

    public void SetSettings(GameSettings curSettings)
    {
        settings = curSettings;
        virusTime = settings.virusTime + timeIncrease;
    }

    public void SetVirusPS(GameObject PS)
    {
        virusPS = PS;
    }

    public void StartDecay()
    {
        isDecaying = true;
        virusTime = settings.virusTime + timeIncrease;
    }

    public void StopDecay()
    {
        isDecaying = false;
    }
}