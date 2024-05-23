using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    public GameObject tutorialScreen;

    private bool gamePaused = true;

    // Start is called before the first frame update
    void Start()
    {
        ShowTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePaused && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) 
        {
            HideTutorial();
            ResumeGame();
        }
    }

    public void ShowTutorial() 
    {
        tutorialScreen.SetActive(true);
        PauseGame();
    }

    public void HideTutorial() 
    {
        tutorialScreen.SetActive(false);
    }

    private void PauseGame() 
    {
        Time.timeScale = 0f;
        gamePaused = true;
    }

    private void ResumeGame() 
    {
        Time.timeScale = 1f;
        gamePaused = false;
    }
}
