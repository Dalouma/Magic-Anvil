using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToShopScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void FullScreen(bool state)
    {

        if (state)
        {
            //Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.SetResolution(1920, 1080, state);
        }
        else
        {
            //Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.SetResolution(1280, 720, state);
        }
        //Screen.SetResolution(1920, 1080, state);
    }
}
