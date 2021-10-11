using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    public Button[] weaponSelectionButtons;
    public GameObject weaponSelectionScreen;
    public GameObject weaponSystem;

    public void CheckWeaponSelection(int id)
    {
        if (weaponSystem.GetComponent<WeaponSystem>().CheckWeapon(id))
        {
            weaponSelectionScreen.SetActive(false);
        }
        else
        {
            weaponSelectionScreen.SetActive(true);
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
}