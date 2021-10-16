using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
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
                case "WeaponCannon":
                    transform.parent.GetComponent<Cannon>().AddBallInRadius(collision.gameObject);
                    break;
                case "WeaponLightning":
                    transform.parent.GetComponent<Lightning>().AddBallInRadius(collision.gameObject);
                    break;
                case "WeaponSpikes":
                    transform.parent.GetComponent<Spikes>().AddBallInRadius(collision.gameObject);
                    break;
                case "WeaponDarkMagic":
                    transform.parent.GetComponent<DarkMagic>().AddBallInRadius(collision.gameObject);
                    break;
                case "WeaponSaw":
                    transform.parent.GetComponent<Saw>().AddBallInRadius(collision.gameObject);
                    break;
                case "WeaponSniper":
                    transform.parent.GetComponent<Sniper>().AddBallInRadius(collision.gameObject);
                    break;
                case "WeaponShocker":
                    transform.parent.GetComponent<Shocker>().AddBallInRadius(collision.gameObject);
                    break;
                case "WeaponShotgun":
                    transform.parent.GetComponent<Shotgun>().AddBallInRadius(collision.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
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
                case "WeaponCannon":
                    transform.parent.GetComponent<Cannon>().RemoveBallFromRadius(collision.gameObject);
                    break;
                case "WeaponLightning":
                    transform.parent.GetComponent<Lightning>().RemoveBallFromRadius(collision.gameObject);
                    break;
                case "WeaponSpikes":
                    transform.parent.GetComponent<Spikes>().RemoveBallFromRadius(collision.gameObject);
                    break;
                case "WeaponDarkMagic":
                    transform.parent.GetComponent<DarkMagic>().RemoveBallFromRadius(collision.gameObject);
                    break;
                case "WeaponSaw":
                    transform.parent.GetComponent<Saw>().RemoveBallFromRadius(collision.gameObject);
                    break;
                case "WeaponSniper":
                    transform.parent.GetComponent<Sniper>().RemoveBallFromRadius(collision.gameObject);
                    break;
                case "WeaponShocker":
                    transform.parent.GetComponent<Shocker>().RemoveBallFromRadius(collision.gameObject);
                    break;
                case "WeaponShotgun":
                    transform.parent.GetComponent<Shotgun>().RemoveBallFromRadius(collision.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}