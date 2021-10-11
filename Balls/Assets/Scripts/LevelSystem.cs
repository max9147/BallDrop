using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int currentLevel;

    public GameObject UISystem;

    private void Start()
    {
        ChangeLevel(0);
    }

    public Camera cam;
    public GameObject[] levels;

    public void ChangeLevel(int id)
    {
        currentLevel = id;
        cam.transform.position = new Vector3(levels[id].transform.position.x, levels[id].transform.position.y, -10f);
        UISystem.GetComponent<WeaponSelection>().CheckWeaponSelection(id);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}