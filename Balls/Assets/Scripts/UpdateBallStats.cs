using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateBallStats : MonoBehaviour
{
    private void Update()
    {
        transform.Find("Canvas").transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -transform.localEulerAngles.z);
        if (transform.localScale.x == 1)
        {
            transform.Find("Canvas").Find("HP").GetComponent<TextMeshProUGUI>().text = "100";
        }
        else
        {
            transform.Find("Canvas").Find("HP").GetComponent<TextMeshProUGUI>().text = (transform.localScale.x * 100).ToString("F1");
        }
    }
}