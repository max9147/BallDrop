using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private bool canOpen = false;
    private int curOpen = -1;

    public GameObject[] upgradeMenus;
    public GameObject[] weaponUpgradeTabs;
    public GameObject[] levelUpgradeTabs;

    public void OpenUpgradeMenu(int id)
    {
        if (canOpen)
        {
            if (curOpen == id)
            {
                CloseUpgradeMenu();
            }
            else
            {
                if (curOpen != -1)
                {
                    upgradeMenus[curOpen].SetActive(false);
                }
                upgradeMenus[id].SetActive(true);
                curOpen = id;
            }
        }
    }

    public void CloseUpgradeMenu()
    {
        curOpen = -1;
        foreach (var item in upgradeMenus)
        {
            item.SetActive(false);
        }
    }

    public void AllowOpening(bool status)
    {
        canOpen = status;
    }

    public void SetUpgradedWeapon(int id)
    {
        foreach (var item in weaponUpgradeTabs)
        {
            item.SetActive(false);
        }
        weaponUpgradeTabs[id].SetActive(true);
    }

    public void SetUpgradedLevel(int id)
    {
        foreach (var item in levelUpgradeTabs)
        {
            item.SetActive(false);
        }
        levelUpgradeTabs[id].SetActive(true);
    }

    public bool CheckOpened()
    {
        if (curOpen == -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}