using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    private float lifeTime;
    private GameObject parentDarkMagic;
    private List<GameObject> damagedBalls = new List<GameObject>();

    public GameSettings settings;

    private void Start()
    {
        lifeTime = settings.darkMagicLifeTime;
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
                parentDarkMagic.GetComponent<DarkMagic>().DealDamage(item.gameObject);
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