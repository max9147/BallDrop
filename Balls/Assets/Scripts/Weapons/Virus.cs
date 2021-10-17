using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public GameObject virusPS;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Virused>())
        {
            collision.gameObject.AddComponent<Virused>();
            collision.gameObject.GetComponent<Virused>().SetVirusPS(virusPS);
        }
        else
        {
            collision.gameObject.GetComponent<Virused>().StopDecay();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Virused>().StartDecay();
    }
}