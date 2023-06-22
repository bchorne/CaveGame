using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public MouseLook mouseLook;

    public void OnEnable()
    {
        Time.timeScale = 0f;
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
        if(mouseLook != null)
        {
            mouseLook.enabled = false;
        }

    }
    public void OnDisable()
    {
        Time.timeScale = 1f;
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
        if(mouseLook != null)
        {
            mouseLook.enabled = true;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
