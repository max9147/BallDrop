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
    private int[] maxLevels = new int[] { 17, 10, 10, 10, 10, 10, 10 };

    public Button[] upgradeButtons;
    public GameObject[] levelButtons;
    public RenderTexture[] levelRenders;
    public Slider[] progressSliders;
    public TextMeshProUGUI[] upgradeCostTexts;

    public void InitializeValues()
    {
        for (int i = 0; i < 7; i++)
        {
            upgradesMaxed[i] = false;
            upgradeLevels[i] = 0;
            upgradeCosts[i] = 1;
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

    public int GetFirstUpgradeLevel()
    {
        return upgradeLevels[0];
    }

    public void BuyUpgrade1()
    {
        GetComponent<PrestigeSystem>().SpendPrestige(upgradeCosts[0]);
        upgradeLevels[0]++;
        GetComponent<WeaponSelection>().ClearWeaponSelection();
        levelButtons[upgradeLevels[0]].GetComponent<Button>().interactable = true;
        levelButtons[upgradeLevels[0]].GetComponent<RawImage>().texture = levelRenders[upgradeLevels[0]];
        upgradeCosts[0] *= 6;
        upgradeCostTexts[0].text = upgradeCosts[0].ToString();
        progressSliders[0].value = (float)upgradeLevels[0] / (float)maxLevels[0];
        if (upgradeLevels[0] == maxLevels[0])
        {
            upgradesMaxed[0] = true;
        }
        if (upgradesMaxed[0] || upgradeCosts[0] > GetComponent<PrestigeSystem>().GetPrestigeCurrent())
        {
            upgradeButtons[0].interactable = false;
        }
    }

    public void SetUpgrade1(int level)
    {
        upgradeLevels[0] = level;
        GetComponent<WeaponSelection>().ClearWeaponSelection();
        for (int i = 0; i <= level; i++)
        {
            levelButtons[i].GetComponent<Button>().interactable = true;
            levelButtons[i].GetComponent<RawImage>().texture = levelRenders[i];
        }
        upgradeCosts[0] = Mathf.Pow(6, level);
        upgradeCostTexts[0].text = upgradeCosts[0].ToString();
        progressSliders[0].value = (float)upgradeLevels[0] / (float)maxLevels[0];
        if (upgradeLevels[0] == maxLevels[0])
        {
            upgradesMaxed[0] = true;
        }
        if (upgradesMaxed[0] || upgradeCosts[0] > GetComponent<PrestigeSystem>().GetPrestigeCurrent())
        {
            upgradeButtons[0].interactable = false;
        }
    }
}