using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virused : MonoBehaviour
{
    private GameObject curVirusPS;
    private GameObject virusPS;
    private GameSettings settings;

    public bool isDecaying = true;
    public float virusTime;

    private void Start()
    {
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
        transform.localScale -= new Vector3(settings.virusDamage / 10000, settings.virusDamage / 10000, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            if (!collision.gameObject.GetComponent<Virused>())
            {
                collision.gameObject.AddComponent<Virused>();
                collision.gameObject.GetComponent<Virused>().SetVirusPS(virusPS);
                collision.gameObject.GetComponent<Virused>().SetSettings(settings);
            }
            else
            {
                collision.gameObject.GetComponent<Virused>().StopDecay();
            }
        }
    }

    public void SetSettings(GameSettings curSettings)
    {
        settings = curSettings;
        virusTime = settings.virusTime;
    }

    public void SetVirusPS(GameObject PS)
    {
        virusPS = PS;
    }

    public void StartDecay()
    {
        isDecaying = true;
        virusTime = settings.virusTime;
    }

    public void StopDecay()
    {
        isDecaying = false;
    }
}