using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    private float multiplier = 1f;

    public GameObject moneySystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            switch (tag)
            {
                case "RewardLow":
                    if (collision.CompareTag("Ball"))
                    {
                        moneySystem.GetComponent<BallScoring>().ScoreBall(1d * multiplier, collision.transform.parent.name);
                    }
                    else
                    {
                        moneySystem.GetComponent<BallScoring>().ScoreBall(1d * multiplier * 10, collision.transform.parent.name);
                    }
                    break;
                case "RewardHigh":                    
                    if (collision.CompareTag("Ball"))
                    {
                        moneySystem.GetComponent<BallScoring>().ScoreBall(1.5d * multiplier, collision.transform.parent.name);
                    }
                    else
                    {
                        moneySystem.GetComponent<BallScoring>().ScoreBall(1.5d * multiplier * 10, collision.transform.parent.name);
                    }
                    break;
                default:
                    break;
            }
            Destroy(collision.gameObject);
        }
    }

    public void IncreaseMultiplier()
    {
        multiplier += 0.5f;
        if (CompareTag("RewardLow"))
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + multiplier.ToString("F1");
        }
        else
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (1.5f * multiplier).ToString("F1");
        }
    }

    public void SetMultiplier(int level)
    {
        multiplier = 1 + 0.5f * level;
        if (CompareTag("RewardLow"))
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + multiplier.ToString("F1");
        }
        else
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (1.5f * multiplier).ToString("F1");
        }
    }
}