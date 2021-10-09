using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    public GameObject moneySystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (tag)
        {
            case "RewardLow":
                moneySystem.GetComponent<BallScoring>().ScoreBall(1f);
                break;
            case "RewardMedium":
                moneySystem.GetComponent<BallScoring>().ScoreBall(1.2f);
                break;
            case "RewardHigh":
                moneySystem.GetComponent<BallScoring>().ScoreBall(1.5f);
                break;
            default:
                break;
        }
        Destroy(collision.gameObject);
    }
}