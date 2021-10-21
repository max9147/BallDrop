using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    private string[] weaponNames = new string[] { "Laser", "Gas", "Gun", "Flamethrower", "Hive", "Cannon", "Lightning", "Spikes", "Poison", "DarkMagic", "Saw", "Sniper", "Shocker", "Shotgun", "Grenades", "Pump", "Minigun", "Virus" };

    public Button[] weaponSelectionButtons;
    public GameObject weaponSelectionScreen;
    public GameObject weaponSystem;
    public Sprite weaponLock;
    public Sprite[] weaponSprites;

    public void CheckWeaponSelection(int id)
    {
        if (weaponSystem.GetComponent<WeaponSystem>().CheckWeapon(id))
        {
            weaponSelectionScreen.SetActive(false);
            GetComponent<ButtonGraphics>().ChangeButtonWeapon(weaponSystem.GetComponent<WeaponSystem>().GetLevelWeapon(id).GetComponent<SpriteRenderer>().sprite);
        }
        else
        {
            weaponSelectionScreen.SetActive(true);
            GetComponent<ButtonGraphics>().ResetButtonWeapon();
        }
    }

    public bool GetWeaponSelectionScreenStatus()
    {
        return weaponSelectionScreen.activeInHierarchy;
    }

    public void SelectWeapon(int id)
    {
        weaponSystem.GetComponent<WeaponSystem>().AssignWeapon(id);        
        weaponSelectionButtons[id].interactable = false;
        weaponSelectionScreen.SetActive(false);
    }

    public void ClearWeaponSelection()
    {
        foreach (var item in weaponSelectionButtons)
        {
            item.interactable = false;
            item.transform.Find("WeaponText").GetComponent<TextMeshProUGUI>().text = "?";
            item.transform.Find("WeaponImage").GetComponent<Image>().sprite = weaponLock;
        }
        for (int i = 0; i <= GetComponent<PrestigeUpgrades>().GetUpgradeLevel(0); i++)
        {
            weaponSelectionButtons[i].transform.Find("WeaponText").GetComponent<TextMeshProUGUI>().text = weaponNames[i];
            weaponSelectionButtons[i].transform.Find("WeaponImage").GetComponent<Image>().sprite = weaponSprites[i];
            if (weaponSystem.GetComponent<WeaponSystem>().GetAssignStatus(i))
            {
                weaponSelectionButtons[i].interactable = false;
            }
            else
            {
                weaponSelectionButtons[i].interactable = true;
            }
        }
    }
}