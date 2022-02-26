using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveGameSystem
{
    public static void SaveGame(MoneySystem moneySystem, PrestigeSystem prestigeSystem, PrestigeUpgrades prestigeUpgrades, WeaponSystem weaponSystem, BallSystem ballSystem, BallScoring ballScoring, LevelUpgrades levelUpgrades, WeaponUpgrades weaponUpgrades, Settings settings, AdDouble adDouble)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savegame.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(moneySystem, prestigeSystem, prestigeUpgrades, weaponSystem, ballSystem, ballScoring, levelUpgrades, weaponUpgrades, settings, adDouble);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}