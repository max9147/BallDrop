using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    private float lifeTime;
    private float timeIncrese = 0;
    private float rangeIncrease = 0;
    private GameObject parentDarkMagic;
    private GameObject UISystem;
    private List<GameObject> damagedBalls = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
        timeIncrese = 0.5f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade2()[9];
        rangeIncrease = 0.1f * UISystem.GetComponent<WeaponUpgrades>().GetUpgrade3()[9];
        lifeTime = settings.darkMagicLifeTime + timeIncrese;
        transform.localScale = new Vector3(1 + rangeIncrease, 1 + rangeIncrease, 1);
    }

    private void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        foreach (var item in damagedBalls)
        {
            if (item)
            {
                if (parentDarkMagic)
                {
                    parentDarkMagic.GetComponent<DarkMagic>().DealDamage(item.gameObject);
                }
            }     
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") && !damagedBalls.Contains(collision.gameObject))
        {            
            damagedBalls.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") && damagedBalls.Contains(collision.gameObject))
        {
            damagedBalls.Remove(collision.gameObject);
        }
    }

    public void SetParentDarkMagic(GameObject darkMagic)
    {
        parentDarkMagic = darkMagic;
    }
}