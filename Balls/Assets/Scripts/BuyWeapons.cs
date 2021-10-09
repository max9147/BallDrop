using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWeapons : MonoBehaviour
{
    public GameObject moneySystem;
    public GameObject weaponSystem;

    public void BuyWeapon()
    {
        if (weaponSystem.GetComponent<WeaponSystem>().GetWeaponAvailability())
        {
            moneySystem.GetComponent<MoneySystem>().SpendMoney(weaponSystem.GetComponent<WeaponSystem>().GetWeaponCost());
            weaponSystem.GetComponent<WeaponSystem>().SpawnWeapon(gameObject);
        }
    }
}