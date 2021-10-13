using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    private int currentLevel;

    public Camera cam;
    public GameObject selectionOutline;
    public GameObject UISystem;
    public GameObject[] levelButtons;
    public GameObject[] levels;

    private void Start()
    {
        ChangeLevel(0);
    }

    public void ChangeLevel(int id)
    {
        currentLevel = id;
        cam.transform.position = new Vector3(levels[id].transform.position.x, levels[id].transform.position.y, -10f);
        selectionOutline.transform.position = levelButtons[id].transform.position;
        UISystem.GetComponent<WeaponSelection>().CheckWeaponSelection(id);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}