using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public GameObject poisonPS;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Poisoned>())
        {
            collision.gameObject.AddComponent<Poisoned>();
            collision.gameObject.GetComponent<Poisoned>().SetPoisonPS(poisonPS);
        }
        else
        {
            collision.gameObject.GetComponent<Poisoned>().StopDecay();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Poisoned>().StartDecay();
    }
}