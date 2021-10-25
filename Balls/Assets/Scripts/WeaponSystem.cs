using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    private bool weaponAvailable;
    private bool[] weaponsAssigned = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
    private bool[] boughtWeapons = new bool[66];
    private double weaponCost;
    private int weaponsBought;
    private int[] weaponLevelNum = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    private GameObject curSpawned;
    private GameObject[] weaponLevel = new GameObject[18];

    public GameObject ballSystem;
    public GameObject levelSystem;
    public GameObject moneySystem;
    public GameObject UISystem;
    public GameObject[] weaponTypes;
    public GameObject[] weaponsUnused;

    public void InitializeValues()
    {
        for (int i = 0; i < boughtWeapons.Length; i++)
        {
            boughtWeapons[i] = false;
        }
        weaponsBought = 0;
        weaponCost = Mathf.Floor(5f * Mathf.Pow(1.5f, weaponsBought));
        RefreshWeaponCost();
    }

    public void LoadValues(bool[] savedBought, int boughtCount, double cost)
    {
        boughtWeapons = savedBought;
        for (int i = 0; i < boughtWeapons.Length; i++)
        {
            if (boughtWeapons[i])
            {
                SpawnWeapon(weaponsUnused[i]);
            }
        }
        weaponsBought = boughtCount;
        weaponCost = cost;
        RefreshWeaponCost();
    }

    public bool[] SaveAssignments()
    {
        for (int i = 0; i < weaponsUnused.Length; i++)
        {
            if (!weaponsUnused[i].activeInHierarchy)
            {
                boughtWeapons[i] = true;
            }
        }
        return boughtWeapons;
    }

    public void AssignWeapon(int id)
    {
        UISystem.GetComponent<ButtonGraphics>().ChangeButtonWeapon(id);
        UISystem.GetComponent<UpgradeSystem>().SetUpgradedWeapon(id);
        UISystem.GetComponent<UpgradeSystem>().AllowOpening(true);
        ballSystem.GetComponent<BallSystem>().SpawnBall(levelSystem.GetComponent<LevelSystem>().GetCurrentLevel());
        weaponLevel[levelSystem.GetComponent<LevelSystem>().GetCurrentLevel()] = weaponTypes[id];
        weaponLevelNum[levelSystem.GetComponent<LevelSystem>().GetCurrentLevel()] = id;
        weaponsAssigned[id] = true;
    }

    public bool GetAssignStatus(int id)
    {
        return weaponsAssigned[id];
    }

    public void UnassignWeapons()
    {
        for (int i = 0; i < weaponLevel.Length; i++)
        {
            weaponLevel[i] = null;
            weaponLevelNum[i] = -1;
            weaponsAssigned[i] = false;
        }
        for (int i = 0; i < boughtWeapons.Length; i++)
        {
            boughtWeapons[i] = false;
        }
        weaponsBought = 0;
        weaponCost = Mathf.Floor(5f * Mathf.Pow(1.5f, weaponsBought));
        RefreshWeaponCost();
    }

    public bool CheckWeapon(int id)
    {
        return weaponLevel[id];
    }

    public void CheckWeaponAvaliable()
    {
        if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= weaponCost)
        {
            weaponAvailable = true;
            foreach (var item in weaponsUnused)
            {
                if (item)
                {
                    item.transform.Find("Canvas").Find("Cost").GetComponent<TextMeshProUGUI>().color = Color.green;
                }
            }
        }
        else
        {
            weaponAvailable = false;
            foreach (var item in weaponsUnused)
            {
                if (item)
                {
                    item.transform.Find("Canvas").Find("Cost").GetComponent<TextMeshProUGUI>().color = Color.red;
                }
            }
        }
    }

    public int[] saveLevelWeapons()
    {
        return weaponLevelNum;
    }

    public void LoadLevelWeapons(int[] weaponIDs)
    {
        for (int i = 0; i < weaponIDs.Length; i++)
        {
            if (weaponIDs[i] != -1)
            {
                weaponLevel[i] = weaponTypes[weaponIDs[i]];
                weaponLevelNum[i] = weaponIDs[i];
                weaponsAssigned[weaponIDs[i]] = true;
            }
        }
        UISystem.GetComponent<WeaponSelection>().ClearWeaponSelection();
    }

    public int GetBoughtCount()
    {
        return weaponsBought;
    }

    public double GetCost()
    {
        return weaponCost;
    }

    public GameObject GetLevelWeapon(int id)
    {
        return weaponLevel[id];
    }

    public int GetLevelWeaponID(int levelID)
    {
        return weaponLevelNum[levelID];
    }

    public double GetWeaponCost()
    {
        return weaponCost;
    }

    public bool GetWeaponAvailability()
    {
        return weaponAvailable;
    }

    public void RefreshWeaponCost()
    {
        foreach (var item in weaponsUnused)
        {
            if (item)
            {
                if (weaponCost < 1000d)
                {
                    item.transform.Find("Canvas").Find("Cost").GetComponent<TextMeshProUGUI>().text = "$" + weaponCost.ToString("F0");
                }
                else if (weaponCost < 1000000d)
                {
                    item.transform.Find("Canvas").Find("Cost").GetComponent<TextMeshProUGUI>().text = "$" + (weaponCost / 1000d).ToString("F2") + "K";
                }
                else if (weaponCost < 1000000000d)
                {
                    item.transform.Find("Canvas").Find("Cost").GetComponent<TextMeshProUGUI>().text = "$" + (weaponCost / 1000000d).ToString("F2") + "M";
                }
                else if (weaponCost < 1000000000000d)
                {
                    item.transform.Find("Canvas").Find("Cost").GetComponent<TextMeshProUGUI>().text = "$" + (weaponCost / 1000000000d).ToString("F2") + "B";
                }
                else
                {
                    item.transform.Find("Canvas").Find("Cost").GetComponent<TextMeshProUGUI>().text = "$" + (weaponCost / 1000000000000d).ToString("F2") + "T";
                }
            }
        }
        CheckWeaponAvaliable();
    }

    public void SellWeapon()
    {
        weaponsBought--;
        weaponCost = Mathf.Floor(5 * Mathf.Pow(1.5f, weaponsBought));
        RefreshWeaponCost();
    }

    public void SpawnWeapon(GameObject spot)
    {
        spot.SetActive(false);
        switch (spot.tag)
        {
            case "LevelPochinko":
                curSpawned = Instantiate(weaponLevel[0], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelFunnel":
                curSpawned = Instantiate(weaponLevel[1], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelGaps":
                curSpawned = Instantiate(weaponLevel[2], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelSqueeze":
                curSpawned = Instantiate(weaponLevel[3], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelMills":
                curSpawned = Instantiate(weaponLevel[4], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelVerticals":
                curSpawned = Instantiate(weaponLevel[5], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelMovement":
                curSpawned = Instantiate(weaponLevel[6], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelTraps":
                curSpawned = Instantiate(weaponLevel[7], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelMixer":
                curSpawned = Instantiate(weaponLevel[8], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelElevator":
                curSpawned = Instantiate(weaponLevel[9], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelBoulders":
                curSpawned = Instantiate(weaponLevel[10], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelShrink":
                curSpawned = Instantiate(weaponLevel[11], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelPlatforms":
                curSpawned = Instantiate(weaponLevel[12], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelDiamonds":
                curSpawned = Instantiate(weaponLevel[13], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelSpinners":
                curSpawned = Instantiate(weaponLevel[14], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelChoise":
                curSpawned = Instantiate(weaponLevel[15], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelZigzag":
                curSpawned = Instantiate(weaponLevel[16], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelFinal":
                curSpawned = Instantiate(weaponLevel[17], spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            default:
                break;
        }
        spot.transform.parent = curSpawned.transform;
        weaponsBought++;
        weaponCost = Mathf.Floor(5 * Mathf.Pow(1.5f, weaponsBought));
        RefreshWeaponCost();
    }
}