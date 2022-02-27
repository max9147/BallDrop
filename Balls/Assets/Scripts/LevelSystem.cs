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
    public GameObject soundSystem;
    public GameObject dimScreen;
    public GameObject[] levelButtons;
    public GameObject[] levels;

    public void ChangeLevel(int id)
    {
        if (currentLevel != -1 && currentLevel != id)
        {
            soundSystem.GetComponent<SoundSystem>().PlayClick();
            dimScreen.GetComponent<Animation>().Play();
        }
        StartCoroutine(ChangeWait(id));
    }

    private void ExecuteChange(int id)
    {
        if (!weaponSystem.GetComponent<WeaponSystem>().GetLevelWeapon(id) && UISystem.GetComponent<UpgradeSystem>().GetCurOpen() != 2)
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
            UISystem.GetComponent<UpgradeSystem>().SetUpgradedWeapon(weaponSystem.GetComponent<WeaponSystem>().GetLevelWeaponID(id));
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private IEnumerator ChangeWait(int id)
    {
        yield return new WaitForSeconds(0.04f);
        ExecuteChange(id);
    }
}