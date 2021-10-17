using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private void FixedUpdate()
    {
        switch (tag)
        {
            case "Clockwise":
                transform.Rotate(new Vector3(0, 0, -0.5f));
                break;
            case "CounterClockwise":
                transform.Rotate(new Vector3(0, 0, 0.5f));
                break;
            default:
                break;
        }        
    }
}