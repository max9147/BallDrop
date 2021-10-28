using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Camera cam;
    public GameObject levelSystem;
    public GameObject tutorialMenu;
    public GameObject tutorial;
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
            if (touch.phase == TouchPhase.Began && isPlaying && waitTime > 1f && !tutorialMenu.activeInHierarchy)
            {
                NextStep();
            }
        }
    }

    public void AskTutorial()
    {
        tutorialMenu.SetActive(true);
        isPlaying = true;
    }

    public void StartTutorial()
    {
        waitTime = 0;
        tutorial.SetActive(true);
        tutorialMenu.SetActive(false);
    }

    public void NextStep()
    {
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
        tutorial.SetActive(false);
        tutorialMenu.SetActive(false);
        isPlaying = false;
        levelSystem.GetComponent<LevelSystem>().ChangeLevel(0);
    }

    public bool GetStatus()
    {
        return isPlaying;
    }
}