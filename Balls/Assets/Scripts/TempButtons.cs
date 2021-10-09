using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempButtons : MonoBehaviour
{
    public GameObject[] weapons;

    public void PressBuy(int id)
    {
        switch (id)
        {
            case 1:
                weapons[0].GetComponent<BuyWeapons>().BuyWeapon();
                break;
            case 2:
                weapons[1].GetComponent<BuyWeapons>().BuyWeapon();
                break;
            case 3:
                weapons[2].GetComponent<BuyWeapons>().BuyWeapon();
                break;
            default:
                break;
        }
    }
}
