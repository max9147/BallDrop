using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private bool canOpen = false;
    private int curOpen = -1;

    public AnimationClip openClip;
    public AnimationClip closeClip;

    public GameObject adSystem;
    public GameObject soundSystem;
    public GameObject shopContainer;
    public GameObject[] upgradeMenus;
    public GameObject[] weaponUpgradeTabs;
    public GameObject[] levelUpgradeTabs;

    public void OpenUpgradeMenu(int id)
    {
        if (canOpen || id == 2)
        {
            shopContainer.GetComponent<Animation>().clip = openClip;
            shopContainer.GetComponent<Animation>().Play();
            if (adSystem.GetComponent<AdSystem>().GetPassiveAdStatus())
            {
                adSystem.GetComponent<AdSystem>().ShowAdScreen();
            }
            soundSystem.GetComponent<SoundSystem>().PlayWhoosh();
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
        shopContainer.GetComponent<Animation>().clip = closeClip;
        shopContainer.GetComponent<Animation>().Play();
        StartCoroutine(CloseWait());
    }

    private IEnumerator CloseWait()
    {
        yield return new WaitForSeconds(0.1f);
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

    public int GetCurOpen()
    {
        return curOpen;
    }
}