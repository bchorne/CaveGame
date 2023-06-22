using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public MouseLook mouseLook;

    public void OnEnable()
    {
        Time.timeScale = 0f;
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
        mouseLook.enabled = false;
    }
    public void OnDisable()
    {
        Time.timeScale = 1f;
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.enabled = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
