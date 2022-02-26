using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TempStuff : MonoBehaviour
{
    public GameObject moneySystem;
    public GameObject UISystem;

    public void DoubleMoney()
    {
        moneySystem.GetComponent<MoneySystem>().AddMoney(moneySystem.GetComponent<MoneySystem>().GetMoneyAmount(), false);
        UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(moneySystem.GetComponent<MoneySystem>().GetMoneyAmount());
    }

    public void DeleteSave()
    {
        Time.timeScale = 0;
        File.Delete(Path.Combine(Application.persistentDataPath, "savegame.save"));
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}