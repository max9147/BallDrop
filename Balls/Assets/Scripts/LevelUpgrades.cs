using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpgrades : MonoBehaviour
{
    private int[] upgrade1Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade2Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade3Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public Button[] upgrade1Buttons;
    public Button[] upgrade2Buttons;
    public Button[] upgrade3Buttons;
    public Slider[] upgrade1Sliders;
    public Slider[] upgrade2Sliders;
    public Slider[] upgrade3Sliders;
    public TextMeshProUGUI[] upgrade1Costs;
    public TextMeshProUGUI[] upgrade2Costs;
    public TextMeshProUGUI[] upgrade3Costs;

    public void BuyUpgrade1(int level)
    {
        upgrade1Levels[level]++;
    }

    public void SetUpgrade1(int[] levels)
    {
        upgrade1Levels = levels;
    }

    public int[] GetUpgrade1()
    {
        return upgrade1Levels;
    }    

    public void BuyUpgrade2(int level)
    {
        upgrade2Levels[level]++;
    }

    public void SetUpgrade2(int[] levels)
    {
        upgrade2Levels = levels;
    }

    public int[] GetUpgrade2()
    {
        return upgrade2Levels;
    }

    public void BuyUpgrade3(int level)
    {
        upgrade3Levels[level]++;
    }

    public void SetUpgrade3(int[] levels)
    {
        upgrade3Levels = levels;
    }

    public int[] GetUpgrade3()
    {
        return upgrade3Levels;
    }

    public void ResetUpgrades()
    {
        for (int i = 0; i < upgrade1Levels.Length; i++)
        {
            upgrade1Levels[i] = 0;
            upgrade2Levels[i] = 0;
            upgrade3Levels[i] = 0;
        }
    }
}
