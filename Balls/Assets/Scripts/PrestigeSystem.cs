using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrestigeSystem : MonoBehaviour
{
    private bool firstPress = true;
    private double prestigePoints = 0;
    private double prestigePointsGain = 0;
    private double totalEarnings;
    private GameObject[] toDestroy;

    public GameObject ballSystem;
    public GameObject levelSystem;
    public GameObject moneySystem;
    public GameObject weaponSystem;
    public TextMeshProUGUI prestigeButtonText;
    public TextMeshProUGUI moneyLeftText;

    public void AddTotalEarnings(double amount)
    {
        totalEarnings += amount;
        prestigePointsGain = Math.Floor(totalEarnings / 100);
        prestigeButtonText.text = "+" + prestigePointsGain;
        moneyLeftText.text = "Money until next prestige point: " + (Math.Ceiling((prestigePointsGain + 1) * 100 - totalEarnings)).ToString();
    }

    public void PressPrestige()
    {
        if (firstPress)
        {
            firstPress = false;
            prestigeButtonText.text = "Are you sure?";
        }
        else
        {
            PrestigeReset();
        }
    }

    public void RevokePressing()
    {
        firstPress = true;
        prestigeButtonText.text = "+0";
    }

    private void PrestigeReset()
    {
        toDestroy = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var item in toDestroy)
        {
            Destroy(item);
        }
        toDestroy = FindObjectsOfType<GameObject>();
        foreach (var item in toDestroy)
        {
            if (item.layer == LayerMask.NameToLayer("Weapon"))
            {
                item.transform.Find("WeaponUnused").gameObject.SetActive(true);
                item.transform.Find("WeaponUnused").SetParent(item.transform.parent);
                Destroy(item);
            }
        }
        weaponSystem.GetComponent<WeaponSystem>().UnassignWeapons();
        levelSystem.GetComponent<LevelSystem>().ChangeLevel(0);
        GetComponent<UpgradeSystem>().CloseUpgradeMenu();
        GetComponent<WeaponSelection>().ClearWeaponSelection();
        ballSystem.GetComponent<BallSystem>().StopAllCoroutines();
        moneySystem.GetComponent<MoneySystem>().ResetMoney();
    }
}