using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBalls : MonoBehaviour
{
    private RaycastHit2D touchHit;
    private Touch touch;
    private Vector3 touchPos;

    public Camera cam;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (GetComponent<UpgradeSystem>().CheckOpened() || GetComponent<Settings>().GetSettingsStatus() || GetComponent<OfflineIncome>().GetOfflineMenuStatus())
            {
                return;
            }
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPos = cam.ScreenToWorldPoint(touch.position);
                touchHit = Physics2D.Raycast(touchPos, Vector2.zero);
                if (touchHit && touchHit.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
                {
                    touchHit.collider.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
                }
            }
        }
    }
}