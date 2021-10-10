using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWeapons : MonoBehaviour
{
    private Touch touch;
    private Vector3 touchPos;
    private RaycastHit2D touchHit;

    public Camera cam;
    public GameObject moneySystem;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPos = cam.ScreenToWorldPoint(touch.position);
            touchHit = Physics2D.Raycast(touchPos, Vector2.zero);
            if (touchHit && touchHit.collider.gameObject.layer == LayerMask.NameToLayer("WeaponSpot"))
            {
                BuyWeapon(touchHit.collider.gameObject);
            }
        }
    }

    public void BuyWeapon(GameObject weaponSpot)
    {
        if (GetComponent<WeaponSystem>().GetWeaponAvailability())
        {
            moneySystem.GetComponent<MoneySystem>().SpendMoney(GetComponent<WeaponSystem>().GetWeaponCost());
            GetComponent<WeaponSystem>().SpawnWeapon(weaponSpot);
        }
    }
}