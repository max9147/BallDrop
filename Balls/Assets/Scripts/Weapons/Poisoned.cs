using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour
{
    private GameObject curPoisonPS;
    private GameObject poisonPS;

    public bool isDecaying = true;
    public float poisonTime;

    private void Start()
    {
        poisonTime = 4f;
        curPoisonPS = Instantiate(poisonPS, transform);
    }

    private void FixedUpdate()
    {
        if (isDecaying)
        {
            poisonTime -= Time.deltaTime;
            if (poisonTime <= 0)
            {
                Destroy(curPoisonPS);
                Destroy(this);
            }
        }
        transform.localScale -= new Vector3(0.0006f, 0.0006f, 0.0006f);
    }

    public void SetPoisonPS(GameObject PS)
    {
        poisonPS = PS;
    }

    public void StartDecay()
    {
        isDecaying = true;
        poisonTime = 4f;
    }

    public void StopDecay()
    {
        isDecaying = false;
    }
}