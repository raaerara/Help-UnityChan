using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchChecker : MonoBehaviour
{
    protected virtual void CheckTouch()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Vector2 point = touch.position;
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(point);

            if (Camera.main == null)
            {
                ray = Camera.current.ScreenPointToRay(point);
            }
            if (Physics.Raycast(ray, out hit))
            {
                hit.transform.gameObject.SendMessage("OnMouseDown");
                Debug.Log("オンマウスダウンを呼びました");
            }
        }
    }

    void Update()
    {
        CheckTouch();
    }
}