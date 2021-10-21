using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeSystem : MonoBehaviour
{
    private bool firstPress = false;
    private double prestigePointsCurrent = 0;
    private double prestigePointsTotal = 0;
    private double prestigePointsGain = 0;
    private double prestigeValueBoost = 100;
    private double totalEarnings;
    private double totalEarningsLeft;
    private GameObject[] toDestroy;

    public GameObject ballSystem;
    public GameObject levelSystem;
    public GameObject moneySystem;
    public GameObject weaponSystem;
    public GameObject[] prestigeUpgradeButtons;
    public TextMeshProUGUI bottomPrestigeCounter;
    public TextMeshProUGUI currentPrestigePointsText;
    public TextMeshProUGUI prestigeButtonText;
    public TextMeshProUGUI moneyLeftText;
    public TextMeshProUGUI prestigeValueBoostText;

    public void AddTotalEarnings(double amount)
    {
        totalEarnings += amount;
        prestigePointsGain = Math.Floor(totalEarnings / 1000) - prestigePointsTotal;
        if (prestigePointsGain < 0)
        {
            prestigePointsGain = 0;
        }
        RefreshPrestigeStats();
    }

    public double GetPrestigeCurrent()
    {
        return prestigePointsCurrent;
    }

    public double GetPrestigeTotal()
    {
        return prestigePointsTotal;
    }

    public double GetPrestigeGain()
    {
        return prestigePointsGain;
    }

    public double GetTotalEarnings()
    {
        return totalEarnings;
    }

    public double GetBallValueBoost()
    {
        return prestigeValueBoost;
    }

    public void PressPrestige()
    {
        if (!firstPress)
        {
            firstPress = true;
            prestigeButtonText.text = "Press again to confirm";
        }
        else
        {
            PrestigeReset();
        }
    }

    public void RevokePressing()
    {
        firstPress = false;
        RefreshPrestigeStats();
    }

    public void RefreshButtonStatus()
    {
        for (int i = 0; i < prestigeUpgradeButtons.Length; i++)
        {
            if (GetComponent<PrestigeUpgrades>().CheckMaxed(i))
            {
                prestigeUpgradeButtons[i].GetComponent<Button>().interactable = false;
                prestigeUpgradeButtons[i].transform.Find("BuyButtonText").GetComponent<TextMeshProUGUI>().text = "Maxed";
            }
            else
            {
                if (prestigePointsCurrent >= GetComponent<PrestigeUpgrades>().GetUpgradeCost(i))
                {
                    prestigeUpgradeButtons[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    prestigeUpgradeButtons[i].GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void SetPrestigeValues(double pointsCurrent, double pointsTotal, double pointsGain, double valueBoost, double earnings)
    {
        prestigePointsCurrent = pointsCurrent;
        prestigePointsTotal = pointsTotal;
        prestigePointsGain = pointsGain;
        prestigeValueBoost = valueBoost;
        totalEarnings = earnings;
        RefreshPrestigeStats();
        RefreshButtonStatus();
    }

    public void SpendPrestige(double amount)
    {
        prestigePointsCurrent -= amount;
        RefreshPrestigeStats();
        RefreshButtonStatus();
    }

    private void PrestigeReset()
    {
        toDestroy = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var item in toDestroy)
        {
            Destroy(item);
        }
        toDestroy = FindObjectsOfType<GameObject>();
        foreach (var item in toDestroy)
        {
            if (item.layer == LayerMask.NameToLayer("Weapon"))
            {
                item.transform.Find("WeaponUnused").gameObject.SetActive(true);
                item.transform.Find("WeaponUnused").SetParent(item.transform.parent);
                Destroy(item);
            }
        }
        weaponSystem.GetComponent<WeaponSystem>().UnassignWeapons();
        levelSystem.GetComponent<LevelSystem>().ChangeLevel(0);
        GetComponent<UpgradeSystem>().CloseUpgradeMenu();
        GetComponent<WeaponSelection>().ClearWeaponSelection();
        ballSystem.GetComponent<BallSystem>().StopAllCoroutines();
        moneySystem.GetComponent<MoneySystem>().ResetMoney();
        prestigePointsCurrent += prestigePointsGain;
        prestigePointsTotal += prestigePointsGain;
        prestigeValueBoost += prestigePointsGain * 0.1f;
        totalEarnings = 0;
        prestigePointsGain = 0;
        RefreshPrestigeStats();
        RefreshButtonStatus();
    }

    private void RefreshPrestigeStats()
    {
        if (!firstPress)
        {
            if (prestigePointsGain < 1000d)
            {
                prestigeButtonText.text = "+" + prestigePointsGain.ToString("F0");
            }
            else if (prestigePointsGain < 1000000d)
            {
                prestigeButtonText.text = "+" + (prestigePointsGain / 1000d).ToString("F2") + "K";
            }
            else if (prestigePointsGain < 1000000000d)
            {
                prestigeButtonText.text = "+" + (prestigePointsGain / 1000000d).ToString("F2") + "M";
            }
            else if (prestigePointsGain < 1000000000000d)
            {
                prestigeButtonText.text = "+" + (prestigePointsGain / 1000000000d).ToString("F2") + "B";
            }
            else
            {
                prestigeButtonText.text = "+" + (prestigePointsGain / 1000000000000d).ToString("F2") + "T";
            }
        }
        totalEarningsLeft = Math.Ceiling((prestigePointsGain + prestigePointsTotal + 1) * 1000 - totalEarnings);
        if (totalEarningsLeft < 1000d)
        {
            moneyLeftText.text = "Money until next prestige point: " + totalEarningsLeft.ToString("F0");
        }
        else if (totalEarningsLeft < 1000000d)
        {
            moneyLeftText.text = "Money until next prestige point: " + (totalEarningsLeft / 1000d).ToString("F2") + "K";
        }
        else if (totalEarningsLeft < 1000000000d)
        {
            moneyLeftText.text = "Money until next prestige point: " + (totalEarningsLeft / 1000000d).ToString("F2") + "M";
        }
        else if (totalEarningsLeft < 1000000000000d)
        {
            moneyLeftText.text = "Money until next prestige point: " + (totalEarningsLeft / 1000000000d).ToString("F2") + "B";
        }
        else
        {
            moneyLeftText.text = "Money until next prestige point: " + (totalEarningsLeft / 1000000000000d).ToString("F2") + "T";
        }
        if (prestigePointsCurrent < 1000d)
        {
            currentPrestigePointsText.text = prestigePointsCurrent.ToString("F0");
        }
        else if (prestigePointsCurrent < 1000000d)
        {
            currentPrestigePointsText.text = (prestigePointsCurrent / 1000d).ToString("F2") + "K";
        }
        else if (prestigePointsCurrent < 1000000000d)
        {
            currentPrestigePointsText.text = (prestigePointsCurrent / 1000000d).ToString("F2") + "M";
        }
        else if (prestigePointsCurrent < 1000000000000d)
        {
            currentPrestigePointsText.text = (prestigePointsCurrent / 1000000000d).ToString("F2") + "B";
        }
        else
        {
            currentPrestigePointsText.text = (prestigePointsCurrent / 1000000000000d).ToString("F2") + "T";
        }
        bottomPrestigeCounter.text = currentPrestigePointsText.text;
        if (prestigeValueBoost < 1000d)
        {
            prestigeValueBoostText.text = "Prestige ball value boost: " + prestigeValueBoost.ToString("F1") + "%";
        }
        else if (prestigeValueBoost < 1000000d)
        {
            prestigeValueBoostText.text = "Prestige ball value boost: " + (prestigeValueBoost / 1000d).ToString("F2") + "K%";
        }
        else if (prestigeValueBoost < 1000000000d)
        {
            prestigeValueBoostText.text = "Prestige ball value boost: " + (prestigeValueBoost / 1000000d).ToString("F2") + "M%";
        }
        else if (prestigeValueBoost < 1000000000000d)
        {
            prestigeValueBoostText.text = "Prestige ball value boost: " + (prestigeValueBoost / 1000000000d).ToString("F2") + "B%";
        }
        else
        {
            prestigeValueBoostText.text = "Prestige ball value boost: " + (prestigeValueBoost / 1000000000000d).ToString("F2") + "T%";
        }
    }
}