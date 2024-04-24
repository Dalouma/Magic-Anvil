using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    public GameObject menuPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (menuPanel != null) 
            {
                menuPanel.SetActive(!menuPanel.activeSelf);

                if (menuPanel.activeSelf ) 
                {
                    PauseGame();
                }
                else 
                {
                    UnpauseGame();
                }
            }
        }
    }

    public void QuitGame() 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void PauseGame() 
    {
        Time.timeScale = 0;
    }

    void UnpauseGame() 
    {
        Time.timeScale = 1f;
    }
}
