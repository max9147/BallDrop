using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour
{
    private GameObject UISystem;
    private List<GameObject> hoses = new List<GameObject>();
    private List<GameObject> targets = new List<GameObject>();

    public GameObject hose;
    public GameSettings settings;

    private void Start()
    {
        UISystem = GameObject.Find("UISystem");
    }

    private void FixedUpdate()
    {
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (!targets[i])
                {
                    targets.RemoveAt(i);
                    Destroy(hoses[i]);
                    hoses.RemoveAt(i);
                }
                else
                {
                    targets[i].transform.localScale -= new Vector3(settings.pumpDamage / 10000, settings.pumpDamage / 10000, 0);
                    UISystem.GetComponent<WeaponUpgrades>().IncreaseDamage(15, settings.pumpDamage / 100);
                    hoses[i].GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1f));
                    hoses[i].GetComponent<LineRenderer>().SetPosition(1, new Vector3(targets[i].transform.position.x, targets[i].transform.position.y, -1f));
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!targets.Contains(collision.gameObject))
        {
            targets.Add(collision.gameObject);
            hoses.Add(Instantiate(hose, transform));
        }
    }
}