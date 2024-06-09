using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    public GameObject tutorialScreen;

    // Start is called before the first frame update
    void Start()
    {
        ShowTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialScreen.activeInHierarchy && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) 
        {
            AudioManager.Click();
            HideTutorial();
        }
    }

    public void ShowTutorial() 
    {
        tutorialScreen.SetActive(true);
    }

    public void HideTutorial() 
    {
        tutorialScreen.SetActive(false);
    }
}
