using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    private bool weaponAvailable;
    private float weaponCost;
    private GameObject weaponPochinko;
    private GameObject weaponFunnel;
    private GameObject weaponGaps;
    private GameObject weaponSqueeze;
    private GameObject weaponMills;
    private GameObject weaponVerticals;
    private GameObject weaponMovement;
    private GameObject weaponTraps;

    public GameObject ballSystem;
    public GameObject levelSystem;
    public GameObject moneySystem;
    public GameObject UISystem;
    public GameObject[] weaponTypes;
    public GameObject[] weaponsUnused;    

    private void Start()
    {
        weaponCost = 5f;
        RefreshWeaponCost();
    }

    public void AssignWeapon(int id)
    {
        UISystem.GetComponent<ButtonGraphics>().ChangeButtonWeapon(id);
        ballSystem.GetComponent<BallSystem>().SpawnBall(levelSystem.GetComponent<LevelSystem>().GetCurrentLevel());
        switch (levelSystem.GetComponent<LevelSystem>().GetCurrentLevel())
        {
            case 0:
                weaponPochinko = weaponTypes[id];
                break;
            case 1:
                weaponFunnel = weaponTypes[id];
                break;
            case 2:
                weaponGaps = weaponTypes[id];
                break;
            case 3:
                weaponSqueeze = weaponTypes[id];
                break;
            case 4:
                weaponMills = weaponTypes[id];
                break;
            case 5:
                weaponVerticals = weaponTypes[id];
                break;
            case 6:
                weaponMovement = weaponTypes[id];
                break;
            case 7:
                weaponTraps = weaponTypes[id];
                break;
            default:
                break;
        }
    }

    public bool CheckWeapon(int id)
    {        
        switch (id)
        {
            case 0:
                return weaponPochinko;
            case 1:
                return weaponFunnel;
            case 2:
                return weaponGaps;
            case 3:
                return weaponSqueeze;
            case 4:
                return weaponMills;
            case 5:
                return weaponVerticals;
            case 6:
                return weaponMovement;
            case 7:
                return weaponTraps;
            default:
                return false;
        }
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

    public GameObject GetLevelWeapon(int id)
    {
        switch (id)
        {
            case 0:
                return weaponPochinko;
            case 1:
                return weaponFunnel;
            case 2:
                return weaponGaps;
            case 3:
                return weaponSqueeze;
            case 4:
                return weaponMills;
            case 5:
                return weaponVerticals;
            case 6:
                return weaponMovement;
            case 7:
                return weaponTraps;
            default:
                return null;
        }
    }

    public float GetWeaponCost()
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
                item.transform.Find("Canvas").Find("Cost").GetComponent<TextMeshProUGUI>().text = "$" + weaponCost;
            }
        }
        CheckWeaponAvaliable();
    }

    public void SpawnWeapon(GameObject spot)
    {
        switch (spot.tag)
        {
            case "LevelPochinko":
                Instantiate(weaponPochinko, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelFunnel":                
                Instantiate(weaponFunnel, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelGaps":
                Instantiate(weaponGaps, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelSqueeze":
                Instantiate(weaponSqueeze, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelMills":
                Instantiate(weaponMills, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelVerticals":
                Instantiate(weaponVerticals, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelMovement":
                Instantiate(weaponMovement, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelTraps":
                Instantiate(weaponTraps, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            default:
                break;
        }
        Destroy(spot);
        weaponCost = Mathf.Ceil(weaponCost * 1.5f);
        RefreshWeaponCost();
    }
}