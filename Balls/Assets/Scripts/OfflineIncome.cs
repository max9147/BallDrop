using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfflineIncome : MonoBehaviour
{
    private double offlineRevenue;
    private int maxOfflineTime = 2;
    private string revenueString;
    private string doubleRevenueString;
    private DateTime lastLogin;
    private TimeSpan offlineTime;

    public GameObject moneySystem;
    public GameObject offlineMenu;
    public TextMeshProUGUI offlineTimeText;
    public TextMeshProUGUI collectText;
    public TextMeshProUGUI doubleCollectText;

    public void CallOfflineProgress(string lastSavedTime, double moneyPerSecond)
    {
        offlineMenu.SetActive(true);
        lastLogin = DateTime.Parse(lastSavedTime);
        offlineTime = DateTime.Now - lastLogin;
        if (offlineTime.TotalHours > maxOfflineTime)
        {
            offlineTime = TimeSpan.FromHours(maxOfflineTime);
        }
        offlineRevenue = Math.Floor(moneyPerSecond * offlineTime.TotalSeconds * 0.1d);
        if (offlineRevenue < 1000d)
        {
            revenueString = "$" + offlineRevenue.ToString("F0");
            doubleRevenueString = "$" + (offlineRevenue * 2).ToString("F0");
        }
        else if (offlineRevenue < 1000000d)
        {
            revenueString = "$" + (offlineRevenue / 1000d).ToString("F2") + "K";
            doubleRevenueString = "$" + (offlineRevenue * 2 / 1000d).ToString("F2") + "K";
        }
        else if (offlineRevenue < 1000000000d)
        {
            revenueString = "$" + (offlineRevenue / 1000000d).ToString("F2") + "M";
            doubleRevenueString = "$" + (offlineRevenue * 2 / 1000000d).ToString("F2") + "M";
        }
        else if (offlineRevenue < 1000000000000d)
        {
            revenueString = "$" + (offlineRevenue / 1000000000d).ToString("F2") + "B";
            doubleRevenueString = "$" + (offlineRevenue * 2 / 1000000000d).ToString("F2") + "B";
        }
        else
        {
            revenueString = "$" + (offlineRevenue / 1000000000000d).ToString("F2") + "T";
            doubleRevenueString = "$" + (offlineRevenue * 2 / 1000000000000d).ToString("F2") + "T";
        }
        offlineTimeText.text = "You were offline for " + offlineTime.Hours + "h " + offlineTime.Minutes + "m " + offlineTime.Seconds + "s and accumulated " + revenueString;
        collectText.text = "Get " + revenueString;
        doubleCollectText.text = "Get " + doubleRevenueString;
    }

    public void SelectNormal()
    {
        moneySystem.GetComponent<MoneySystem>().AddMoney(offlineRevenue, false);
        offlineMenu.SetActive(false);
    }

    public void SelectDouble()
    {
        moneySystem.GetComponent<MoneySystem>().AddMoney(offlineRevenue * 2, false);
        offlineMenu.SetActive(false);
    }

    public void UpgradeMaxTime()
    {
        maxOfflineTime++;
    }

    public void SetMaxTime(int level)
    {
        maxOfflineTime += level;
    }
}