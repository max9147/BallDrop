using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public GameObject poisonPS;
    public GameSettings settings;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Poisoned>())
        {
            collision.gameObject.AddComponent<Poisoned>();
            collision.gameObject.GetComponent<Poisoned>().SetPoisonPS(poisonPS);
            collision.gameObject.GetComponent<Poisoned>().SetSettings(settings);
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