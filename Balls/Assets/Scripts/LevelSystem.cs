using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    private int currentLevel = -1;

    public Camera cam;
    public GameObject selectionOutline;
    public GameObject weaponSystem;
    public GameObject UISystem;
    public GameObject[] levelButtons;
    public GameObject[] levels;

    private void Start()
    {
        ChangeLevel(0);
    }

    public void ChangeLevel(int id)
    {
        if (currentLevel != id)
        {
            UISystem.GetComponent<UpgradeSystem>().CloseUpgradeMenu();
        }
        currentLevel = id;
        cam.transform.position = new Vector3(levels[id].transform.position.x, levels[id].transform.position.y, -10f);
        selectionOutline.transform.position = levelButtons[id].transform.position;
        UISystem.GetComponent<WeaponSelection>().CheckWeaponSelection(id);
        UISystem.GetComponent<UpgradeSystem>().SetUpgradedLevel(id);        
        if (!weaponSystem.GetComponent<WeaponSystem>().GetLevelWeapon(id))
        {
            UISystem.GetComponent<UpgradeSystem>().AllowOpening(false);
        }
        else
        {
            UISystem.GetComponent<UpgradeSystem>().AllowOpening(true);
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}