using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeDetection : MonoBehaviour
{
    public Camera cam;
    public GameObject levelSystem;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private void Update()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 pos = cam.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(touch.position.x, touch.position.y);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(touch.position.x, touch.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                if (currentSwipe.y < 100 && currentSwipe.y > -100)
                    return;

                currentSwipe.Normalize();

                if (GetComponent<UpgradeSystem>().CheckOpened() || GetComponent<Settings>().GetSettingsStatus() || GetComponent<OfflineIncome>().GetOfflineMenuStatus() || GetComponent<Tutorial>().GetStatus())
                {
                    return;
                }

                if (currentSwipe.y > 0 && currentSwipe.x > -0.9f && currentSwipe.x < 0.9f && levelSystem.GetComponent<LevelSystem>().GetCurrentLevel() < 17)
                {
                    if (levelSystem.GetComponent<LevelSystem>().levelButtons[levelSystem.GetComponent<LevelSystem>().GetCurrentLevel() + 1].GetComponent<Button>().interactable)
                    {
                        levelSystem.GetComponent<LevelSystem>().ChangeLevel(levelSystem.GetComponent<LevelSystem>().GetCurrentLevel() + 1);
                    }
                }

                if (currentSwipe.y < 0 && currentSwipe.x > -0.9f && currentSwipe.x < 0.9f && levelSystem.GetComponent<LevelSystem>().GetCurrentLevel() > 0)
                {
                    if (levelSystem.GetComponent<LevelSystem>().levelButtons[levelSystem.GetComponent<LevelSystem>().GetCurrentLevel() - 1].GetComponent<Button>().interactable)
                    {
                        levelSystem.GetComponent<LevelSystem>().ChangeLevel(levelSystem.GetComponent<LevelSystem>().GetCurrentLevel() - 1);
                    }
                }
            }
        }
    }
}