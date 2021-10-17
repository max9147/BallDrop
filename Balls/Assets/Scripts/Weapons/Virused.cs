using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virused : MonoBehaviour
{
    private GameObject curVirusPS;
    private GameObject virusPS;

    public bool isDecaying = true;
    public float virusTime;

    private void Start()
    {
        virusTime = 4f;
        curVirusPS = Instantiate(virusPS, transform);
    }

    private void FixedUpdate()
    {
        if (isDecaying)
        {
            virusTime -= Time.deltaTime;
            if (virusTime <= 0)
            {
                Destroy(curVirusPS);
                Destroy(this);
            }
        }
        transform.localScale -= new Vector3(0.0006f, 0.0006f, 0.0006f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
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
    }

    public void SetVirusPS(GameObject PS)
    {
        virusPS = PS;
    }

    public void StartDecay()
    {
        isDecaying = true;
        virusTime = 4f;
    }

    public void StopDecay()
    {
        isDecaying = false;
    }
}