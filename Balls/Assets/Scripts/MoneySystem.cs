using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private float money;

    public GameObject weaponSystem;
    public TextMeshProUGUI mainMoneyCounter;

    private void Start()
    {
        money =15000f;
        RefreshMoneyCounters();
    }

    private void RefreshMoneyCounters()
    {
        mainMoneyCounter.text = "Money: " + money;
    }

    public void AddMoney(float amount)
    {
        money += amount;
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
}