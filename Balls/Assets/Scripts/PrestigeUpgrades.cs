using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeUpgrades : MonoBehaviour
{
    private bool[] upgradesMaxed = new bool[7];
    private double[] upgradeCosts = new double[] { 1, 1, 50, 10, 10, 1, 2 };
    private int[] upgradeLevels = new int[7];
    private int[] maxLevels = new int[] { 17, 20, 5, 9, 10, 15, 6 };
    private int[] levelCostIncrease = new int[] { 5, 4, 150, 20, 14, 6, 100 };

    public Button[] upgradeButtons;
    public GameObject ballSystem;
    public GameObject moneySystem;
    public GameObject soundSystem;
    public GameObject[] levelButtons;
    public RenderTexture[] levelRenders;
    public Slider[] progressSliders;
    public TextMeshProUGUI[] upgradeCostTexts;
    public TextMeshProUGUI[] upgradeDescriptions;

    public void InitializeValues()
    {
        for (int i = 0; i < 7; i++)
        {
            upgradesMaxed[i] = false;
            upgradeLevels[i] = 0;           
        }
    }

    public double GetUpgradeCost(int id)
    {
        return upgradeCosts[id];
    }

    public bool CheckMaxed(int id)
    {
        return upgradesMaxed[id];
    }

    public int GetUpgradeLevel(int id)
    {
        return upgradeLevels[id];
    }

    public void BuyUpgrade1()
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        UpgradeRefresh(0);
        GetComponent<WeaponSelection>().ClearWeaponSelection();
        levelButtons[upgradeLevels[0]].GetComponent<Button>().interactable = true;
        levelButtons[upgradeLevels[0]].GetComponent<RawImage>().texture = levelRenders[upgradeLevels[0]];
    }

    public void BuyUpgrade2()
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        UpgradeRefresh(1);
        moneySystem.GetComponent<BallScoring>().UpgradeGlobalMul();
        if (upgradeLevels[1] == maxLevels[1])
        {
            upgradeDescriptions[1].text = "Ball cost multiplier";
        }
        else
        {
            upgradeDescriptions[1].text = "Ball cost multiplier (" + Mathf.Pow(1.5f, upgradeLevels[1]).ToString("F1") + "x -> " + Mathf.Pow(1.5f, upgradeLevels[1] + 1).ToString("F1") + "x)";
        }
    }

    public void BuyUpgrade3()
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        UpgradeRefresh(2);
        ballSystem.GetComponent<BallSystem>().UpgradeGlobalMul();
        if (upgradeLevels[2] == maxLevels[2])
        {
            upgradeDescriptions[2].text = "Ball drop rate";
        }
        else
        {
            upgradeDescriptions[2].text = "Ball drop rate (" + (1 - 0.1f * upgradeLevels[2]) + "x -> " + (1 - 0.1f * (upgradeLevels[2] + 1)) + "x)";
        }
    }

    public void BuyUpgrade4()
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        UpgradeRefresh(3);
        moneySystem.GetComponent<BallScoring>().UpgradeFinishMul();
        if (upgradeLevels[3] == maxLevels[3])
        {
            upgradeDescriptions[3].text = "Increase finish multiplier";
        }
        else
        {
            upgradeDescriptions[3].text = "Increase finish multiplier (" + (1 + 0.5f * upgradeLevels[3]) + "x -> " + (1 + 0.5f * (upgradeLevels[3] + 1)) + "x)";
        }
    }

    public void BuyUpgrade5()
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        UpgradeRefresh(4);
        GetComponent<OfflineIncome>().UpgradeMaxTime();
        if (upgradeLevels[4] == maxLevels[4])
        {
            upgradeDescriptions[4].text = "Max offline income time";
        }
        else
        {
            upgradeDescriptions[4].text = "Max offline income time (" + (2 + upgradeLevels[4]) + "hr -> " + (2 + upgradeLevels[4] + 1) + "hr)";
        }
    }

    public void BuyUpgrade6()
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        UpgradeRefresh(5);
        ballSystem.GetComponent<BallSystem>().UpgradeGoldenChance();
        if (upgradeLevels[5] == maxLevels[5])
        {
            upgradeDescriptions[5].text = "Increase golden ball chance";
        }
        else
        {
            upgradeDescriptions[5].text = "Increase golden ball chance (" + upgradeLevels[5] + "% -> " + (upgradeLevels[5] + 1) + "%)";
        }
    }

    public void BuyUpgrade7()
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        UpgradeRefresh(6);
        moneySystem.GetComponent<MoneySystem>().UpgradeStartMoney();
        if (upgradeLevels[6] == maxLevels[6])
        {
            upgradeDescriptions[6].text = "More money after reset";
        }
        else
        {
            float temp = 10 * Mathf.Pow(100, upgradeLevels[6]);
            if (temp < 1000d)
            {
                upgradeDescriptions[6].text = "More money after reset (" + 10 * Mathf.Pow(100, upgradeLevels[6]) + " -> " + 10 * Mathf.Pow(100, upgradeLevels[6] + 1) + ")";
            }
            else if (temp < 1000000d)
            {
                upgradeDescriptions[6].text = "More money after reset (" + (10 * Mathf.Pow(100, upgradeLevels[6]) / 1000d).ToString("F0") + "K -> " + (10 * Mathf.Pow(100, upgradeLevels[6] + 1) / 1000d).ToString("F0") + "K)";
            }
            else if (temp < 1000000000d)
            {
                upgradeDescriptions[6].text = "More money after reset (" + (10 * Mathf.Pow(100, upgradeLevels[6]) / 1000000d).ToString("F0") + "M -> " + (10 * Mathf.Pow(100, upgradeLevels[6] + 1) / 1000000d).ToString("F0") + "M)";
            }
            else if (temp < 1000000000000d)
            {
                upgradeDescriptions[6].text = "More money after reset (" + (10 * Mathf.Pow(100, upgradeLevels[6]) / 1000000000d).ToString("F0") + "B -> " + (10 * Mathf.Pow(100, upgradeLevels[6] + 1) / 1000000000d).ToString("F0") + "B)";
            }
            else
            {
                upgradeDescriptions[6].text = "More money after reset (" + (10 * Mathf.Pow(100, upgradeLevels[6]) / 1000000000000d).ToString("F0") + "T -> " + (10 * Mathf.Pow(100, upgradeLevels[6] + 1) / 1000000000000d).ToString("F0") + "T)";
            }
        }
    }

    private void UpgradeRefresh(int id)
    {
        GetComponent<PrestigeSystem>().SpendPrestige(upgradeCosts[id]);
        upgradeLevels[id]++;
        upgradeCosts[id] *= levelCostIncrease[id];
        if (upgradeCosts[id] < 1000d)
        {
            upgradeCostTexts[id].text = upgradeCosts[id].ToString("F0");
        }
        else if (upgradeCosts[id] < 1000000d)
        {
            upgradeCostTexts[id].text = (upgradeCosts[id] / 1000d).ToString("F2") + "K";
        }
        else if (upgradeCosts[id] < 1000000000d)
        {
            upgradeCostTexts[id].text = (upgradeCosts[id] / 1000000d).ToString("F2") + "M";
        }
        else if (upgradeCosts[id] < 1000000000000d)
        {
            upgradeCostTexts[id].text = (upgradeCosts[id] / 1000000000d).ToString("F2") + "B";
        }
        else
        {
            upgradeCostTexts[id].text = (upgradeCosts[id] / 1000000000000d).ToString("F2") + "T";
        }
        progressSliders[id].value = (float)upgradeLevels[id] / (float)maxLevels[id];
        if (upgradeLevels[id] == maxLevels[id])
        {
            upgradesMaxed[id] = true;
            upgradeCostTexts[id].text = "Maxed";
        }
        if (upgradesMaxed[id] || upgradeCosts[id] > GetComponent<PrestigeSystem>().GetPrestigeCurrent())
        {
            upgradeButtons[id].interactable = false;
        }
    }

    public void SetUpgrade1(int level)
    {
        SetUpgradeRefresh(0, level);
        GetComponent<WeaponSelection>().ClearWeaponSelection();
        for (int i = 0; i <= level; i++)
        {
            levelButtons[i].GetComponent<Button>().interactable = true;
            levelButtons[i].GetComponent<RawImage>().texture = levelRenders[i];
        }
    }

    public void SetUpgrade2(int level)
    {
        SetUpgradeRefresh(1, level);
        moneySystem.GetComponent<BallScoring>().SetGlobalMul(level);
        if (upgradeLevels[1] == maxLevels[1])
        {
            upgradeDescriptions[1].text = "Ball cost multiplier";
        }
        else
        {
            upgradeDescriptions[1].text = "Ball cost multiplier (" + Mathf.Pow(1.5f, upgradeLevels[1]).ToString("F1") + "x -> " + Mathf.Pow(1.5f, upgradeLevels[1] + 1).ToString("F1") + "x)";
        }
    }

    public void SetUpgrade3(int level)
    {
        SetUpgradeRefresh(2, level);
        ballSystem.GetComponent<BallSystem>().SetGlobalMul(level);
        if (upgradeLevels[2] == maxLevels[2])
        {
            upgradeDescriptions[2].text = "Ball drop rate";
        }
        else
        {
            upgradeDescriptions[2].text = "Ball drop rate (" + (1 - 0.1f * upgradeLevels[2]) + "x -> " + (1 - 0.1f * (upgradeLevels[2] + 1)) + "x)";
        }
    }

    public void SetUpgrade4(int level)
    {
        SetUpgradeRefresh(3, level);
        moneySystem.GetComponent<BallScoring>().SetFinishMul(level);
        if (upgradeLevels[3] == maxLevels[3])
        {
            upgradeDescriptions[3].text = "Increase finish multiplier";
        }
        else
        {
            upgradeDescriptions[3].text = "Increase finish multiplier (" + (1 + 0.5f * upgradeLevels[3]) + "x -> " + (1 + 0.5f * (upgradeLevels[3] + 1)) + "x)";
        }
    }

    public void SetUpgrade5(int level)
    {
        SetUpgradeRefresh(4, level);
        GetComponent<OfflineIncome>().SetMaxTime(level);
        if (upgradeLevels[4] == maxLevels[4])
        {
            upgradeDescriptions[4].text = "Max offline income time";
        }
        else
        {
            upgradeDescriptions[4].text = "Max offline income time (" + (2 + upgradeLevels[4]) + "hr -> " + (2 + upgradeLevels[4] + 1) + "hr)";
        }
    }

    public void SetUpgrade6(int level)
    {
        SetUpgradeRefresh(5, level);
        ballSystem.GetComponent<BallSystem>().SetGoldenChance(level);
        if (upgradeLevels[5] == maxLevels[5])
        {
            upgradeDescriptions[5].text = "Increase golden ball chance";
        }
        else
        {
            upgradeDescriptions[5].text = "Increase golden ball chance (" + upgradeLevels[5] + "% -> " + (upgradeLevels[5] + 1) + "%)";
        }
    }

    public void SetUpgrade7(int level)
    {
        SetUpgradeRefresh(6, level);
        moneySystem.GetComponent<MoneySystem>().SetStartMoney(level);
        if (upgradeLevels[6] == maxLevels[6])
        {
            upgradeDescriptions[6].text = "More money after reset";
        }
        else
        {
            float temp = 10 * Mathf.Pow(100, upgradeLevels[6]);
            if (temp < 1000d)
            {
                upgradeDescriptions[6].text = "More money after reset (" + 10 * Mathf.Pow(100, upgradeLevels[6]) + " -> " + 10 * Mathf.Pow(100, upgradeLevels[6] + 1) + ")";
            }
            else if (temp < 1000000d)
            {
                upgradeDescriptions[6].text = "More money after reset (" + (10 * Mathf.Pow(100, upgradeLevels[6]) / 1000d).ToString("F0") + "K -> " + (10 * Mathf.Pow(100, upgradeLevels[6] + 1) / 1000d).ToString("F0") + "K)";
            }
            else if (temp < 1000000000d)
            {
                upgradeDescriptions[6].text = "More money after reset (" + (10 * Mathf.Pow(100, upgradeLevels[6]) / 1000000d).ToString("F0") + "M -> " + (10 * Mathf.Pow(100, upgradeLevels[6] + 1) / 1000000d).ToString("F0") + "M)";
            }
            else if (temp < 1000000000000d)
            {
                upgradeDescriptions[6].text = "More money after reset (" + (10 * Mathf.Pow(100, upgradeLevels[6]) / 1000000000d).ToString("F0") + "B -> " + (10 * Mathf.Pow(100, upgradeLevels[6] + 1) / 1000000000d).ToString("F0") + "B)";
            }
            else
            {
                upgradeDescriptions[6].text = "More money after reset (" + (10 * Mathf.Pow(100, upgradeLevels[6]) / 1000000000000d).ToString("F0") + "T -> " + (10 * Mathf.Pow(100, upgradeLevels[6] + 1) / 1000000000000d).ToString("F0") + "T)";
            }
        }
    }

    private void SetUpgradeRefresh(int id, int level)
    {
        upgradeLevels[id] = level;
        upgradeCosts[id] = upgradeCosts[id] * Mathf.Pow(levelCostIncrease[id], level);
        if (upgradeCosts[id] < 1000d)
        {
            upgradeCostTexts[id].text = upgradeCosts[id].ToString("F0");
        }
        else if (upgradeCosts[id] < 1000000d)
        {
            upgradeCostTexts[id].text = (upgradeCosts[id] / 1000d).ToString("F2") + "K";
        }
        else if (upgradeCosts[id] < 1000000000d)
        {
            upgradeCostTexts[id].text = (upgradeCosts[id] / 1000000d).ToString("F2") + "M";
        }
        else if (upgradeCosts[id] < 1000000000000d)
        {
            upgradeCostTexts[id].text = (upgradeCosts[id] / 1000000000d).ToString("F2") + "B";
        }
        else
        {
            upgradeCostTexts[id].text = (upgradeCosts[id] / 1000000000000d).ToString("F2") + "T";
        }
        progressSliders[id].value = (float)upgradeLevels[id] / (float)maxLevels[id];
        if (upgradeLevels[id] == maxLevels[id])
        {
            upgradesMaxed[id] = true;
            upgradeCostTexts[id].text = "Maxed";
        }
        if (upgradesMaxed[id] || upgradeCosts[id] > GetComponent<PrestigeSystem>().GetPrestigeCurrent())
        {
            upgradeButtons[id].interactable = false;
        }
    }
}