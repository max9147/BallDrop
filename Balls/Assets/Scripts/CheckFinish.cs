using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    private int levelID;
    public float[] defaultMultipliers = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    private float prestigeMultiplier = 1f;

    public GameObject moneySystem;
    public GameObject soundSystem;

    private void Start()
    {
        switch (transform.parent.parent.name)
        {
            case "LevelPochinko":
                levelID = 0;
                break;
            case "LevelFunnel":
                levelID = 1;
                break;
            case "LevelGaps":
                levelID = 2;
                break;
            case "LevelSqueeze":
                levelID = 3;
                break;
            case "LevelMills":
                levelID = 4;
                break;
            case "LevelVerticals":
                levelID = 5;
                break;
            case "LevelMovement":
                levelID = 6;
                break;
            case "LevelTraps":
                levelID = 7;
                break;
            case "LevelMixer":
                levelID = 8;
                break;
            case "LevelElevator":
                levelID = 9;
                break;
            case "LevelBoulders":
                levelID = 10;
                break;
            case "LevelShrink":
                levelID = 11;
                break;
            case "LevelPlatforms":
                levelID = 12;
                break;
            case "LevelDiamonds":
                levelID = 13;
                break;
            case "LevelSpinners":
                levelID = 14;
                break;
            case "LevelChoise":
                levelID = 15;
                break;
            case "LevelZigzag":
                levelID = 16;
                break;
            case "LevelFinal":
                levelID = 17;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            soundSystem.GetComponent<SoundSystem>().PlayBallPop();
            switch (tag)
            {
                case "RewardLow":
                    if (collision.CompareTag("Ball"))
                    {
                        moneySystem.GetComponent<BallScoring>().ScoreBall(1d * defaultMultipliers[levelID] * prestigeMultiplier, collision.transform.parent.name);
                    }
                    else if (collision.CompareTag("GoldenBall"))
                    {
                        moneySystem.GetComponent<BallScoring>().ScoreBall(1d * defaultMultipliers[levelID] * prestigeMultiplier * 10, collision.transform.parent.name);
                    }
                    break;
                case "RewardHigh":
                    if (collision.CompareTag("Ball"))
                    {
                        moneySystem.GetComponent<BallScoring>().ScoreBall(1.5d * defaultMultipliers[levelID] * prestigeMultiplier, collision.transform.parent.name);
                    }
                    else if (collision.CompareTag("GoldenBall"))
                    {
                        moneySystem.GetComponent<BallScoring>().ScoreBall(1.5d * defaultMultipliers[levelID] * prestigeMultiplier * 10, collision.transform.parent.name);
                    }
                    break;
                default:
                    break;
            }
            Destroy(collision.gameObject);
        }
    }

    public void SetDefaultMultiplier(int[] levels)
    {
        for (int i = 0; i < defaultMultipliers.Length; i++)
        {
            defaultMultipliers[i] = 1 + levels[i] * 0.5f;
        }
        if (CompareTag("RewardLow"))
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (defaultMultipliers[levelID] * prestigeMultiplier).ToString("F1");
        }
        else
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (1.5f * defaultMultipliers[levelID] * prestigeMultiplier).ToString("F1");
        }
    }

    public void ResetDefaultMultiplier()
    {
        for (int i = 0; i < defaultMultipliers.Length; i++)
        {
            defaultMultipliers[i] = 1;
        }
        if (CompareTag("RewardLow"))
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (defaultMultipliers[levelID] * prestigeMultiplier).ToString("F1");
        }
        else
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (1.5f * defaultMultipliers[levelID] * prestigeMultiplier).ToString("F1");
        }
    }

    public void IncreaseMultiplier()
    {
        prestigeMultiplier += 0.5f;
        if (CompareTag("RewardLow"))
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (defaultMultipliers[levelID] * prestigeMultiplier).ToString("F1");
        }
        else
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (1.5f * defaultMultipliers[levelID] * prestigeMultiplier).ToString("F1");
        }
    }

    public void SetMultiplier(int level)
    {
        prestigeMultiplier = 1 + 0.5f * level;
        if (CompareTag("RewardLow"))
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (defaultMultipliers[levelID] * prestigeMultiplier).ToString("F1");
        }
        else
        {
            transform.Find("Canvas").Find("Multiplier").GetComponent<TextMeshProUGUI>().text = "x" + (1.5f * defaultMultipliers[levelID] * prestigeMultiplier).ToString("F1");
        }
    }
}