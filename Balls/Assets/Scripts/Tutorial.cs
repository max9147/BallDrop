using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Camera cam;
    public GameObject levelSystem;
    public GameObject tutorialMenu;
    public GameObject tutorial;
    public GameObject soundSystem;
    public GameObject weaponSystem;
    public GameObject[] tutorialParts;

    private bool isPlaying = false;
    private int curStep = 0;
    private float waitTime = 0;
    private Touch touch;

    private void Update()
    {
        if (isPlaying)
        {
            waitTime += Time.deltaTime;
        }
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && isPlaying && waitTime > 0.5f && !tutorialMenu.activeInHierarchy)
            {
                NextStep();
            }
        }
    }

    public void AskTutorial()
    {
        tutorialMenu.SetActive(true);        
    }

    public void StartTutorial()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        waitTime = 0;
        isPlaying = true;
        tutorial.SetActive(true);
        tutorialMenu.SetActive(false);
        tutorialParts[0].SetActive(true);
    }

    public void NextStep()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        waitTime = 0;
        curStep++;
        if (curStep == 3)
        {
            EndTutorial();
        }
        else
        {
            tutorialParts[curStep - 1].SetActive(false);
            tutorialParts[curStep].SetActive(true);
        }
    }

    public void EndTutorial()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        tutorial.SetActive(false);
        tutorialMenu.SetActive(false);
        tutorialParts[2].SetActive(false);
        curStep = 0;
        isPlaying = false;
        if (!weaponSystem.GetComponent<WeaponSystem>().GetLevelWeapon(0))
        {
            levelSystem.GetComponent<LevelSystem>().ChangeLevel(0);
        }
    }

    public bool GetStatus()
    {
        return isPlaying;
    }
}