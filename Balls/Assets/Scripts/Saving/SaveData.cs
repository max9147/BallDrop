using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public double money;
    public double prestigePointsCurrent;
    public double prestigePointsTotal;
    public double prestigePointsGain;
    public double prestigeValueBoost;
    public double totalEarnings;
    public int prestigeUpgrade1Level;
    public int prestigeUpgrade2Level;
    public int prestigeUpgrade3Level;
    public int prestigeUpgrade4Level;
    public int prestigeUpgrade5Level;
    public int prestigeUpgrade6Level;
    public int prestigeUpgrade7Level;
    public int[] levelUpgrades1 = new int[18];
    public int[] levelUpgrades2 = new int[18];
    public int[] levelUpgrades3 = new int[18];
    public int[] weaponLevel = new int[18];
    public bool[] weaponAssignments = new bool[66];
    public int weaponBoughtCount;
    public double weaponCost;
    public string curDateTime;
    public double curIncome;
    public int[] levelDroppedCounts = new int[18];
    public double[] levelIncomeCounts = new double[18];

    public SaveData(MoneySystem moneySystem, PrestigeSystem prestigeSystem, PrestigeUpgrades prestigeUpgrades, WeaponSystem weaponSystem, BallSystem ballSystem, BallScoring ballScoring, LevelUpgrades levelUpgrades)
    {
        money = moneySystem.GetMoneyAmount();
        prestigePointsCurrent = prestigeSystem.GetPrestigeCurrent();
        prestigePointsTotal = prestigeSystem.GetPrestigeTotal();
        prestigePointsGain = prestigeSystem.GetPrestigeGain();
        prestigeValueBoost = prestigeSystem.GetBallValueBoost();
        totalEarnings = prestigeSystem.GetTotalEarnings();
        prestigeUpgrade1Level = prestigeUpgrades.GetUpgradeLevel(0);
        prestigeUpgrade2Level = prestigeUpgrades.GetUpgradeLevel(1);
        prestigeUpgrade3Level = prestigeUpgrades.GetUpgradeLevel(2);
        prestigeUpgrade4Level = prestigeUpgrades.GetUpgradeLevel(3);
        prestigeUpgrade5Level = prestigeUpgrades.GetUpgradeLevel(4);
        prestigeUpgrade6Level = prestigeUpgrades.GetUpgradeLevel(5);
        prestigeUpgrade7Level = prestigeUpgrades.GetUpgradeLevel(6);
        levelUpgrades1 = levelUpgrades.GetUpgrade1();
        levelUpgrades2 = levelUpgrades.GetUpgrade2();
        levelUpgrades3 = levelUpgrades.GetUpgrade3();
        weaponLevel = weaponSystem.GetComponent<WeaponSystem>().saveLevelWeapons();
        weaponAssignments = weaponSystem.GetComponent<WeaponSystem>().SaveAssignments();
        weaponBoughtCount = weaponSystem.GetComponent<WeaponSystem>().GetBoughtCount();
        weaponCost = weaponSystem.GetComponent<WeaponSystem>().GetCost();
        curDateTime = DateTime.Now.ToString();
        curIncome = moneySystem.GetComponent<MoneySystem>().GetBuffer();
        levelDroppedCounts = ballSystem.GetComponent<BallSystem>().GetDroppedCounts();
        levelIncomeCounts = ballScoring.GetLevelIncomes();
    }
}