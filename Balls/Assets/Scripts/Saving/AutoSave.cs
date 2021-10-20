using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    public GameObject moneySystem;
    public GameObject UISystem;

    private void Awake()
    {
        StartCoroutine(AutoSaveDelay());
        SaveData save = SaveGameSystem.LoadGame();
        if (save != null)
        {
            moneySystem.GetComponent<MoneySystem>().SetMoney(save.money);
            UISystem.GetComponent<PrestigeSystem>().SetPrestigeValues(save.prestigePointsCurrent, save.prestigePointsTotal, save.prestigePointsGain, save.prestigeValueBoost, save.totalEarnings);
            UISystem.GetComponent<PrestigeUpgrades>().SetUpgrade1(save.prestigeUpgrade1Level);
        }
        else
        {
            moneySystem.GetComponent<MoneySystem>().ResetMoney();
            UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(0);
            UISystem.GetComponent<PrestigeUpgrades>().InitializeValues();
        }
    }

    IEnumerator AutoSaveDelay()
    {
        yield return new WaitForSeconds(1);
        SaveGameSystem.SaveGame(moneySystem.GetComponent<MoneySystem>(), UISystem.GetComponent<PrestigeSystem>(), UISystem.GetComponent<PrestigeUpgrades>());
        StartCoroutine(AutoSaveDelay());
    }
}