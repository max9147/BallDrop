using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    private bool isReloading = false;
    private GameObject currentGrenade;
    private GameObject target;
    private List<GameObject> ballsInRadius = new List<GameObject>();

    public GameObject grenade;
    public GameSettings settings;

    private void Start()
    {
        transform.Find("BallCheck").GetComponent<CircleCollider2D>().radius = settings.grenadesRange;
    }

    private void FixedUpdate()
    {
        if (ballsInRadius.Count > 0)
        {
            foreach (var item in ballsInRadius)
            {
                if (item.transform.localScale.x > settings.ballMinHP / 100 && !item.transform.CompareTag("WeaponGrenades"))
                {
                    target = item;
                    break;
                }
            }
        }
        if (target)
        {
            if (target.transform.localScale.x <= settings.ballMinHP / 100 || !ballsInRadius.Contains(target))
            {
                target = null;
            }
            else if (!isReloading)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        currentGrenade = Instantiate(grenade, transform.position, Quaternion.identity, transform);
        currentGrenade.GetComponent<Grenade>().TakeAim(target);
        isReloading = true;
        StartCoroutine(Reload());
    }

    public void AddBallInRadius(GameObject ball)
    {
        ballsInRadius.Add(ball);
    }

    public void RemoveBallFromRadius(GameObject ball)
    {
        ballsInRadius.Remove(ball);
    }

    public void DealDamage(GameObject damagedBall)
    {
        damagedBall.transform.localScale -= new Vector3(settings.grenadesDamage / 100, settings.grenadesDamage / 100, 0);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(settings.grenadesReload);
        isReloading = false;
    }
}