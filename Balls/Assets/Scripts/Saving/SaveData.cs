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

    public SaveData(MoneySystem moneySystem, PrestigeSystem prestigeSystem, PrestigeUpgrades prestigeUpgrades)
    {
        money = moneySystem.GetMoneyAmount();
        prestigePointsCurrent = prestigeSystem.GetPrestigeCurrent();
        prestigePointsTotal = prestigeSystem.GetPrestigeTotal();
        prestigePointsGain = prestigeSystem.GetPrestigeGain();
        prestigeValueBoost = prestigeSystem.GetBallValueBoost();
        totalEarnings = prestigeSystem.GetTotalEarnings();
        prestigeUpgrade1Level = prestigeUpgrades.GetFirstUpgradeLevel();
    }
}