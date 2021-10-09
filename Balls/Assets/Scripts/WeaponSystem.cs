using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSystem : MonoBehaviour
{
    private bool weaponAvailable;
    private int weaponCost;

    public GameObject moneySystem;
    public GameObject weaponLaser;
    public GameObject weapons;
    public GameObject[] weaponsUnused;

    private void Start()
    {
        weaponCost = 5;
        RefreshWeaponCost();
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

    public int GetWeaponCost()
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
        Instantiate(weaponLaser, spot.transform.position, Quaternion.identity, weapons.transform);
        Destroy(spot);
        weaponCost *= 2;
        RefreshWeaponCost();
    }
}