using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSystem : MonoBehaviour
{
    private bool weaponAvailable;
    private float weaponCost;
    private GameObject weaponPochinko;
    private GameObject weaponFunnel;

    public GameObject levelSystem;
    public GameObject moneySystem;
    public GameObject weapons;
    public GameObject[] weaponTypes;
    public GameObject[] weaponsUnused;

    private void Start()
    {
        weaponCost = 5f;
        RefreshWeaponCost();
    }

    public void AssignWeapon(int id)
    {
        switch (levelSystem.GetComponent<LevelSystem>().GetCurrentLevel())
        {
            case 0:
                weaponPochinko = weaponTypes[id];
                break;
            case 1:
                weaponFunnel = weaponTypes[id];
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
                if (weaponPochinko)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 1:
                if (weaponFunnel)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                Instantiate(weaponPochinko, spot.transform.position, Quaternion.identity, weapons.transform);
                break;
            case "LevelFunnel":                
                Instantiate(weaponFunnel, spot.transform.position, Quaternion.identity, weapons.transform);
                break;
            default:
                break;
        }
        Destroy(spot);
        weaponCost *= 2f;
        RefreshWeaponCost();
    }
}