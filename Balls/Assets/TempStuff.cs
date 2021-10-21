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
        moneySystem.GetComponent<MoneySystem>().AddMoney(30000000000000, false);
        UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(30000000000000);
    }

    public void DeleteSave()
    {
        Time.timeScale = 0;
        File.Delete(Path.Combine(Application.persistentDataPath, "data.save"));
        Application.Quit();
    }
}