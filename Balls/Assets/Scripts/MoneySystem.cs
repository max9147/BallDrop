using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private float money;
    private float moneyBuffer;

    public GameObject weaponSystem;
    public TextMeshProUGUI mainMoneyCounter;
    public TextMeshProUGUI mpsCounter;

    private void Start()
    {
        money =15000f;
        RefreshMoneyCounters();
    }

    private void FixedUpdate()
    {
        if (moneyBuffer > 10000)
        {
            mpsCounter.text = "$" + (moneyBuffer / 10f).ToString("F0") + " / sec";
        }
        else
        {
            mpsCounter.text = "$" + (moneyBuffer / 10f).ToString("F2") + " / sec";
        }
    }

    private void RefreshMoneyCounters()
    {
        if (money > 1000)
        {
            mainMoneyCounter.text = "$" + money.ToString("F0");
        }
        else
        {
            mainMoneyCounter.text = "$" + money.ToString("F2");
        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
        moneyBuffer += amount;
        StartCoroutine(RemoveFromBuffer(amount));
        RefreshMoneyCounters();
        CheckAllAvailabilities();
    }

    public void CheckAllAvailabilities()
    {
        weaponSystem.GetComponent<WeaponSystem>().CheckWeaponAvaliable();
    }

    public float GetMoneyAmount()
    {
        return money;
    }

    public void SpendMoney(float amount)
    {
        money -= amount;
        RefreshMoneyCounters();
        CheckAllAvailabilities();
    }

    private IEnumerator RemoveFromBuffer(float amount)
    {
        yield return new WaitForSeconds(10f);
        moneyBuffer -= amount;
    }
}