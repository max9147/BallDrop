using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractWeapons : MonoBehaviour
{
    private bool isSelling = false;
    private GameObject curSellingWeapon;
    private GameObject touchedObject;
    private RaycastHit2D touchHit;
    private Sprite curSellingSprite;
    private Touch touch;
    private Vector3 touchPos;

    public Camera cam;
    public GameObject moneySystem;
    public GameObject UISystem;
    public GameObject soundSystem;
    public Sprite weaponSelling;

    private void Update()
    {
        if (Input.touchCount > 0 && !UISystem.GetComponent<WeaponSelection>().GetWeaponSelectionScreenStatus())
        {
            if (UISystem.GetComponent<UpgradeSystem>().CheckOpened() || UISystem.GetComponent<Settings>().GetSettingsStatus() || UISystem.GetComponent<OfflineIncome>().GetOfflineMenuStatus() || UISystem.GetComponent<Tutorial>().GetStatus())
            {
                return;
            }
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPos = cam.ScreenToWorldPoint(touch.position);
                touchHit = Physics2D.Raycast(touchPos, Vector2.zero);
                if (touchHit)
                {
                    touchedObject = touchHit.collider.gameObject;
                }                
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchPos = cam.ScreenToWorldPoint(touch.position);
                touchHit = Physics2D.Raycast(touchPos, Vector2.zero);
                if (touchHit && touchHit.collider.gameObject == touchedObject)
                {                 
                    if (touchHit.collider.gameObject.layer == LayerMask.NameToLayer("WeaponSpot"))
                    {
                        soundSystem.GetComponent<SoundSystem>().PlayWeaponSet();
                        BuyWeapon(touchHit.collider.gameObject);
                    }
                    else if (touchHit.collider.gameObject.layer == LayerMask.NameToLayer("Weapon"))
                    {
                        if (!isSelling)
                        {
                            isSelling = true;
                            curSellingWeapon = touchHit.collider.gameObject;
                            curSellingSprite = curSellingWeapon.GetComponent<SpriteRenderer>().sprite;
                            curSellingWeapon.GetComponent<SpriteRenderer>().sprite = weaponSelling;
                            curSellingWeapon.transform.Find("Radius").gameObject.SetActive(true);
                        }
                        else if (curSellingWeapon == touchHit.collider.gameObject)
                        {
                            soundSystem.GetComponent<SoundSystem>().PlayCoin();
                            SellWeapon(curSellingWeapon);                     
                            isSelling = false;
                            curSellingWeapon = null;
                        }
                        else
                        {
                            curSellingWeapon.GetComponent<SpriteRenderer>().sprite = curSellingSprite;
                            curSellingWeapon.transform.Find("Radius").gameObject.SetActive(false);
                            curSellingWeapon = touchHit.collider.gameObject;
                            curSellingSprite = curSellingWeapon.GetComponent<SpriteRenderer>().sprite;
                            curSellingWeapon.GetComponent<SpriteRenderer>().sprite = weaponSelling;
                            curSellingWeapon.transform.Find("Radius").gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    CancelSelection();
                }
            }
        }
    }

    private void BuyWeapon(GameObject weaponSpot)
    {
        if (GetComponent<WeaponSystem>().GetWeaponAvailability())
        {
            moneySystem.GetComponent<MoneySystem>().SpendMoney(GetComponent<WeaponSystem>().GetWeaponCost());
            GetComponent<WeaponSystem>().SpawnWeapon(weaponSpot);
        }
    }

    private void CancelSelection()
    {
        if (isSelling)
        {
            curSellingWeapon.GetComponent<SpriteRenderer>().sprite = curSellingSprite;
            curSellingWeapon.transform.Find("Radius").gameObject.SetActive(false);
            isSelling = false;
            curSellingWeapon = null;
        }
    }

    private void SellWeapon(GameObject weapon)
    {
        weapon.transform.Find("WeaponUnused").gameObject.SetActive(true);
        weapon.transform.Find("WeaponUnused").parent = weapon.transform.parent;
        Destroy(weapon);
        moneySystem.GetComponent<MoneySystem>().AddMoney(GetComponent<WeaponSystem>().GetWeaponCost() / 1.5f / 2f, false);
        GetComponent<WeaponSystem>().SellWeapon();
    }
}