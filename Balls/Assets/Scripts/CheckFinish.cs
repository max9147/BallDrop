using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    public GameObject moneySystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            switch (tag)
            {
                case "RewardLow":
                    moneySystem.GetComponent<BallScoring>().ScoreBall(1d);
                    break;
                case "RewardHigh":
                    moneySystem.GetComponent<BallScoring>().ScoreBall(1.5d);
                    break;
                default:
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}