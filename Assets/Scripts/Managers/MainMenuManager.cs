using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Canvas startMenu;
    
    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.ChangeMusic("menu");
    }

    public void ToggleNewGamePopup()
    {
        startMenu.gameObject.SetActive(true);
    }

     public void ExitNewGamePopup()
    {
        startMenu.gameObject.SetActive(false);
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToShopScene()
    {
        //musicSource.Stop();
        //SceneManager.LoadScene("TestScene");
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
            Screen.SetResolution(1920, 1080, state);
        }
        else
        {
            Screen.SetResolution(1280, 720, state);
        }
    }*/
}
