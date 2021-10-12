using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyWeapons : MonoBehaviour
{
    private RaycastHit2D touchHit;
    private Touch touch;
    private Vector3 touchPos;

    public Camera cam;
    public GameObject moneySystem;
    public GameObject UISystem;

    private void Update()
    {
        if (Input.touchCount > 0 && !UISystem.GetComponent<WeaponSelection>().GetWeaponSelectionScreenStatus())
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPos = cam.ScreenToWorldPoint(touch.position);
                touchHit = Physics2D.Raycast(touchPos, Vector2.zero);
                if (touchHit && touchHit.collider.gameObject.layer == LayerMask.NameToLayer("WeaponSpot"))
                {
                    BuyWeapon(touchHit.collider.gameObject);
                }
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