using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private double money;
    private double moneyBuffer;

    public GameObject weaponSystem;
    public TextMeshProUGUI mainMoneyCounter;
    public TextMeshProUGUI mpsCounter;

    private void Start()
    {
        money = 10000000d;
        RefreshMoneyCounters();
    }

    private void FixedUpdate()
    {
        if (moneyBuffer < 100d)
        {
            mpsCounter.text = "$" + (moneyBuffer / 10f).ToString("F2") + " / sec";
        }
        else if (moneyBuffer < 10000d)
        {
            mpsCounter.text = "$" + (moneyBuffer / 10f).ToString("F0") + " / sec";
        }
        else if (moneyBuffer < 10000000d)
        {
            mpsCounter.text = "$" + (moneyBuffer / 10f / 1000d).ToString("F2") + "K / sec";
        }
        else if (moneyBuffer < 10000000000d)
        {
            mpsCounter.text = "$" + (moneyBuffer / 10f / 1000000d).ToString("F2") + "M / sec";
        }
        else if (moneyBuffer < 10000000000000d)
        {
            mpsCounter.text = "$" + (moneyBuffer / 10f / 1000000000d).ToString("F2") + "B / sec";
        }
        else
        {
            mpsCounter.text = "$" + (moneyBuffer / 10f / 1000000000000d).ToString("F2") + "T / sec";
        }
    }

    private void RefreshMoneyCounters()
    {
        if (money < 100d)
        {
            mainMoneyCounter.text = "$" + money.ToString("F2");
        }
        else if (money < 1000d)
        {
            mainMoneyCounter.text = "$" + money.ToString("F0");
        }
        else if (money < 1000000d)
        {
            mainMoneyCounter.text = "$" + (money / 1000d).ToString("F2") + "K";
        }
        else if (money < 1000000000d)
        {
            mainMoneyCounter.text = "$" + (money / 1000000d).ToString("F2") + "M";
        }
        else if (money < 1000000000000d)
        {
            mainMoneyCounter.text = "$" + (money / 1000000000d).ToString("F2") + "B";
        }
        else
        {
            mainMoneyCounter.text = "$" + (money / 1000000000000d).ToString("F2") + "T";
        }
    }

    public void AddMoney(double amount, bool isBuffered)
    {
        money += amount;
        if (isBuffered)
        {
            moneyBuffer += amount;
            StartCoroutine(RemoveFromBuffer(amount));
        }        
        RefreshMoneyCounters();
        CheckAllAvailabilities();
    }

    public void CheckAllAvailabilities()
    {
        weaponSystem.GetComponent<WeaponSystem>().CheckWeaponAvaliable();
    }

    public double GetMoneyAmount()
    {
        return money;
    }

    public void SpendMoney(double amount)
    {
        money -= amount;
        RefreshMoneyCounters();
        CheckAllAvailabilities();
    }

    private IEnumerator RemoveFromBuffer(double amount)
    {
        yield return new WaitForSeconds(10f);
        moneyBuffer -= amount;
    }
}