using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<Laser>().AddBallInRadius(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.GetComponent<Laser>().RemoveBallFromRadius(collision.gameObject);
    }
}