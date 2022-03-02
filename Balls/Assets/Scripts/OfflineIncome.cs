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

    public GameObject adSystem;
    public GameObject moneySystem;
    public GameObject soundSystem;
    public GameObject offlineMenu;
    public TextMeshProUGUI offlineTimeText;
    public TextMeshProUGUI collectText;
    public TextMeshProUGUI doubleCollectText;

    public string curDateTime;
    public double curIncome;
    public bool haveSave = false;

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (haveSave)
            {
                CallOfflineProgress(curDateTime, curIncome);
            }
        }
        else
        {
            curDateTime = DateTime.Now.ToString();
            curIncome = moneySystem.GetComponent<MoneySystem>().GetBuffer();
            haveSave = true;
        }
    }

    public void CallOfflineProgress(string lastSavedTime, double moneyPerSecond)
    {
        lastLogin = DateTime.Parse(lastSavedTime);
        offlineTime = DateTime.Now - lastLogin;
        if (offlineTime.TotalSeconds < 120)
        {
            return;
        }
        if (offlineTime.TotalHours > maxOfflineTime)
        {
            offlineTime = TimeSpan.FromHours(maxOfflineTime);
        }
        offlineRevenue = Math.Floor(moneyPerSecond * offlineTime.TotalSeconds * 0.1d);
        if (offlineRevenue > 0)
        {
            offlineMenu.SetActive(true);
        }
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
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        moneySystem.GetComponent<MoneySystem>().AddMoney(offlineRevenue, false);
        GetComponent<PrestigeSystem>().AddTotalEarnings(offlineRevenue);
        offlineMenu.SetActive(false);
    }

    public void SelectDouble()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        if (GetComponent<IAP>().GetVIPStatus())
        {
            GetComponent<OfflineIncome>().AdCompleted();
        }
        else
        {
            adSystem.GetComponent<AdSystem>().ShowRewardedVideo(2);
        }
    }

    public void AdCompleted()
    {
        moneySystem.GetComponent<MoneySystem>().AddMoney(offlineRevenue * 2, false);
        GetComponent<PrestigeSystem>().AddTotalEarnings(offlineRevenue * 2);
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

    public bool GetOfflineMenuStatus()
    {
        return offlineMenu.activeInHierarchy;
    }
}