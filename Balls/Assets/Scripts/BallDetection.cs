using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (tag)
        {
            case "WeaponGas":
                transform.parent.GetComponent<Gas>().AddBallInRadius(collision.gameObject);
                break;
            case "WeaponLaser":
                transform.parent.GetComponent<Laser>().AddBallInRadius(collision.gameObject);
                break;
            case "WeaponGun":
                transform.parent.GetComponent<Gun>().AddBallInRadius(collision.gameObject);
                break;
            case "WeaponFlamethrower":
                transform.parent.GetComponent<Flamethrower>().AddBallInRadius(collision.gameObject);
                break;
            case "WeaponHive":
                transform.parent.GetComponent<Hive>().AddBallInRadius(collision.gameObject);
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (tag)
        {
            case "WeaponGas":
                transform.parent.GetComponent<Gas>().RemoveBallFromRadius(collision.gameObject);
                break;
            case "WeaponLaser":
                transform.parent.GetComponent<Laser>().RemoveBallFromRadius(collision.gameObject);
                break;
            case "WeaponGun":
                transform.parent.GetComponent<Gun>().RemoveBallFromRadius(collision.gameObject);
                break;
            case "WeaponFlamethrower":
                transform.parent.GetComponent<Flamethrower>().RemoveBallFromRadius(collision.gameObject);
                break;
            case "WeaponHive":
                transform.parent.GetComponent<Hive>().RemoveBallFromRadius(collision.gameObject);
                break;
            default:
                break;
        }
    }
}