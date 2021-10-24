using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    public GameObject ballSystem;
    public GameObject moneySystem;
    public GameObject UISystem;
    public GameObject weaponSystem;

    private void Awake()
    {
        StartCoroutine(AutoSaveDelay());
        SaveData save = SaveGameSystem.LoadGame();
        if (save != null)
        {
            moneySystem.GetComponent<MoneySystem>().SetMoney(save.money);
            UISystem.GetComponent<PrestigeSystem>().SetPrestigeValues(save.prestigePointsCurrent, save.prestigePointsTotal, save.prestigePointsGain, save.prestigeValueBoost, save.totalEarnings);
            UISystem.GetComponent<PrestigeUpgrades>().SetUpgrade1(save.prestigeUpgrade1Level);
            UISystem.GetComponent<PrestigeUpgrades>().SetUpgrade2(save.prestigeUpgrade2Level);
            UISystem.GetComponent<PrestigeUpgrades>().SetUpgrade3(save.prestigeUpgrade3Level);
            UISystem.GetComponent<PrestigeUpgrades>().SetUpgrade4(save.prestigeUpgrade4Level);
            UISystem.GetComponent<PrestigeUpgrades>().SetUpgrade5(save.prestigeUpgrade5Level);
            UISystem.GetComponent<PrestigeUpgrades>().SetUpgrade6(save.prestigeUpgrade6Level);
            UISystem.GetComponent<PrestigeUpgrades>().SetUpgrade7(save.prestigeUpgrade7Level);
            weaponSystem.GetComponent<WeaponSystem>().LoadLevelWeapons(save.weaponLevel);
            weaponSystem.GetComponent<WeaponSystem>().LoadValues(save.weaponAssignments, save.weaponBoughtCount, save.weaponCost);
            UISystem.GetComponent<OfflineIncome>().CallOfflineProgress(save.curDateTime, save.curIncome);
            ballSystem.GetComponent<BallSystem>().SetDroppedCounts(save.levelDroppedCounts);
            moneySystem.GetComponent<BallScoring>().SetLevelIncomes(save.levelIncomeCounts);
        }
        else
        {
            moneySystem.GetComponent<MoneySystem>().ResetMoney();
            UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(0);
            UISystem.GetComponent<PrestigeUpgrades>().InitializeValues();
            weaponSystem.GetComponent<WeaponSystem>().InitializeValues();
            moneySystem.GetComponent<BallScoring>().InitializeValues();
        }
    }

    IEnumerator AutoSaveDelay()
    {
        yield return new WaitForSeconds(1);
        SaveGameSystem.SaveGame(moneySystem.GetComponent<MoneySystem>(), UISystem.GetComponent<PrestigeSystem>(), UISystem.GetComponent<PrestigeUpgrades>(), weaponSystem.GetComponent<WeaponSystem>(), ballSystem.GetComponent<BallSystem>(), moneySystem.GetComponent<BallScoring>());
        StartCoroutine(AutoSaveDelay());
    }
}