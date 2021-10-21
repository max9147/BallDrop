using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeUpgrades : MonoBehaviour
{
    private bool[] upgradesMaxed = new bool[7];
    private double[] upgradeCosts = new double[7];
    private int[] upgradeLevels = new int[7];
    private int[] maxLevels = new int[] { 17, 20, 5, 10, 10, 10, 10 };
    private int[] levelCostIncrease = new int[] { 5, 4, 150, 1, 1, 1, 1 };

    public Button[] upgradeButtons;
    public GameObject ballSystem;
    public GameObject moneySystem;
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
            if (i == 2)
            {
                upgradeCosts[i] = 50;
            }
            else
            {
                upgradeCosts[i] = 1;
            }            
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
        UpgradeRefresh(0);
        GetComponent<WeaponSelection>().ClearWeaponSelection();
        levelButtons[upgradeLevels[0]].GetComponent<Button>().interactable = true;
        levelButtons[upgradeLevels[0]].GetComponent<RawImage>().texture = levelRenders[upgradeLevels[0]];
    }

    public void BuyUpgrade2()
    {
        UpgradeRefresh(1);
        moneySystem.GetComponent<BallScoring>().UpgradeGlobalMul();
        if (upgradeLevels[1] == maxLevels[1])
        {
            upgradeDescriptions[1].text = "Ball cost multiplier";
        }
        else
        {
            float mult = Mathf.Pow(2, upgradeLevels[1]);
            if (mult < 1000d)
            {
                upgradeDescriptions[1].text = "Ball cost multiplier (" + Mathf.Pow(2, upgradeLevels[1]) + "x -> " + Mathf.Pow(2, upgradeLevels[1] + 1) + "x)";
            }
            else if (mult < 1000000d)
            {
                upgradeDescriptions[1].text = "Ball cost multiplier (" + (Mathf.Pow(2, upgradeLevels[1]) / 1000d).ToString("F0") + "Kx -> " + (Mathf.Pow(2, upgradeLevels[1] + 1) / 1000d).ToString("F0") + "Kx)";
            }
            else if (mult < 1000000000d)
            {
                upgradeDescriptions[1].text = "Ball cost multiplier (" + (Mathf.Pow(2, upgradeLevels[1]) / 1000000d).ToString("F0") + "Mx -> " + (Mathf.Pow(2, upgradeLevels[1] + 1) / 1000000d).ToString("F0") + "Mx)";
            }
        }
    }

    public void BuyUpgrade3()
    {
        UpgradeRefresh(2);
        ballSystem.GetComponent<BallSystem>().UpgradeGlobalMul();
        if (upgradeLevels[2] == maxLevels[2])
        {
            upgradeDescriptions[2].text = "Global ball drop rate";
        }
        else
        {
            upgradeDescriptions[2].text = "Global ball drop rate (" + (1 - 0.1f * upgradeLevels[2]) + "x -> " + (1 - 0.1f * (upgradeLevels[2] + 1)) + "x)";
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
            float mult = Mathf.Pow(2, upgradeLevels[1]);
            if (mult < 1000d)
            {
                upgradeDescriptions[1].text = "Ball cost multiplier (" + Mathf.Pow(2, upgradeLevels[1]) + "x -> " + Mathf.Pow(2, upgradeLevels[1] + 1) + "x)";
            }
            else if (mult < 1000000d)
            {
                upgradeDescriptions[1].text = "Ball cost multiplier (" + (Mathf.Pow(2, upgradeLevels[1]) / 1000d).ToString("F0") + "Kx -> " + (Mathf.Pow(2, upgradeLevels[1] + 1) / 1000d).ToString("F0") + "Kx)";
            }
            else if (mult < 1000000000d)
            {
                upgradeDescriptions[1].text = "Ball cost multiplier (" + (Mathf.Pow(2, upgradeLevels[1]) / 1000000d).ToString("F0") + "Mx -> " + (Mathf.Pow(2, upgradeLevels[1] + 1) / 1000000d).ToString("F0") + "Mx)";
            }
        }
    }

    public void SetUpgrade3(int level)
    {
        SetUpgradeRefresh(2, level);
        ballSystem.GetComponent<BallSystem>().SetGlobalMul(level);
        if (upgradeLevels[2] == maxLevels[2])
        {
            upgradeDescriptions[2].text = "Global ball drop rate";
        }
        else
        {
            upgradeDescriptions[2].text = "Global ball drop rate (" + (1 - 0.1f * upgradeLevels[2]) + "x -> " + (1 - 0.1f * (upgradeLevels[2] + 1)) + "x)";
        }
    }

    private void SetUpgradeRefresh(int id, int level)
    {
        upgradeLevels[id] = level;
        upgradeCosts[id] = Mathf.Pow(levelCostIncrease[id], level);
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