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
    private GameObject weaponMixer;
    private GameObject weaponElevator;
    private GameObject weaponBoulders;
    private GameObject weaponShrink;
    private GameObject weaponPlatforms;
    private GameObject weaponDiamonds;
    private GameObject weaponSpinners;
    private GameObject weaponChoise;
    private GameObject weaponZigzag;
    private GameObject weaponFinal;

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
            case 8:
                weaponMixer = weaponTypes[id];
                break;
            case 9:
                weaponElevator = weaponTypes[id];
                break;
            case 10:
                weaponBoulders = weaponTypes[id];
                break;
            case 11:
                weaponShrink = weaponTypes[id];
                break;
            case 12:
                weaponPlatforms = weaponTypes[id];
                break;
            case 13:
                weaponDiamonds = weaponTypes[id];
                break;
            case 14:
                weaponSpinners = weaponTypes[id];
                break;
            case 15:
                weaponChoise = weaponTypes[id];
                break;
            case 16:
                weaponZigzag = weaponTypes[id];
                break;
            case 17:
                weaponFinal = weaponTypes[id];
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
            case 8:
                return weaponMixer;
            case 9:
                return weaponElevator;
            case 10:
                return weaponBoulders;
            case 11:
                return weaponShrink;
            case 12:
                return weaponPlatforms;
            case 13:
                return weaponDiamonds;
            case 14:
                return weaponSpinners;
            case 15:
                return weaponChoise;
            case 16:
                return weaponZigzag;
            case 17:
                return weaponFinal;
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
            case 8:
                return weaponMixer;
            case 9:
                return weaponElevator;
            case 10:
                return weaponBoulders;
            case 11:
                return weaponShrink;
            case 12:
                return weaponPlatforms;
            case 13:
                return weaponDiamonds;
            case 14:
                return weaponSpinners;
            case 15:
                return weaponChoise;
            case 16:
                return weaponZigzag;
            case 17:
                return weaponFinal;
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
            case "LevelMixer":
                Instantiate(weaponMixer, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelElevator":
                Instantiate(weaponElevator, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelBoulders":
                Instantiate(weaponBoulders, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelShrink":
                Instantiate(weaponShrink, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelPlatforms":
                Instantiate(weaponPlatforms, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelDiamonds":
                Instantiate(weaponDiamonds, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelSpinners":
                Instantiate(weaponSpinners, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelChoise":
                Instantiate(weaponChoise, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelZigzag":
                Instantiate(weaponZigzag, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            case "LevelFinal":
                Instantiate(weaponFinal, spot.transform.position, Quaternion.identity, spot.transform.parent);
                break;
            default:
                break;
        }
        Destroy(spot);
        weaponCost = Mathf.Ceil(weaponCost * 1.5f);
        RefreshWeaponCost();
    }
}