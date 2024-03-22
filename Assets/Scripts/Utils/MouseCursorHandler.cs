using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorHandler : MonoBehaviour
{
    private bool mouseCursorIsEnabled = true;

    void Start()
    {
        ToggleCursorVisualization();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorVisualization();
        }
    }

    private void ToggleCursorVisualization()
    {
        mouseCursorIsEnabled = !mouseCursorIsEnabled;
        Cursor.visible = mouseCursorIsEnabled;

        if (mouseCursorIsEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
