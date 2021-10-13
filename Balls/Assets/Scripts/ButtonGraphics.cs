using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGraphics : MonoBehaviour
{
    public Image buttonWeaponImage;
    public Sprite weaponSpot;
    public Sprite[] weaponSprites;

    public void ChangeButtonWeapon(int id)
    {
        buttonWeaponImage.sprite = weaponSprites[id];
    }

    public void ChangeButtonWeapon(Sprite weaponSprite)
    {
        buttonWeaponImage.sprite = weaponSprite;
    }

    public void ResetButtonWeapon()
    {
        buttonWeaponImage.sprite = weaponSpot;
    }
}