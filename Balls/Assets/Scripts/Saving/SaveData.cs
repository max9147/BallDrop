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
    public int[] weaponLevel = new int[18];
    public bool[] weaponAssignments = new bool[66];
    public int weaponBoughtCount;
    public double weaponCost;

    public SaveData(MoneySystem moneySystem, PrestigeSystem prestigeSystem, PrestigeUpgrades prestigeUpgrades, WeaponSystem weaponSystem)
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
        weaponLevel = weaponSystem.GetComponent<WeaponSystem>().saveLevelWeapons();
        weaponAssignments = weaponSystem.GetComponent<WeaponSystem>().SaveAssignments();
        weaponBoughtCount = weaponSystem.GetComponent<WeaponSystem>().GetBoughtCount();
        weaponCost = weaponSystem.GetComponent<WeaponSystem>().GetCost();
    }
}