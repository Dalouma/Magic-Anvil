using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Analytics;
using Unity.Services.Core.Environments;				

public class MainMenuManager : MonoBehaviour
{
    public Canvas startMenu;
    public Canvas consentMenu;
    public AudioSource musicSource;
    private bool isInitialized = false;
    void Start()
    {
        StartAnalytics();
        AudioManager.instance.ChangeMusic("menu");
    }

    async void StartAnalytics(){
        var options = new InitializationOptions();
        options.SetEnvironmentName("testing");
        await UnityServices.InitializeAsync(options);
        Debug.Log("UnityServicesState:   " +UnityServices.State);
        AnalyticsService.Instance.Flush();
        isInitialized = true;
    }
    public void ConsentGiven()
	{
        if (!isInitialized)
        {
            Debug.LogWarning("Initialization is not complete. Please wait.");
            return;
        }
		AnalyticsService.Instance.StartDataCollection();
        consentMenu.gameObject.SetActive(false);
        ToggleNewGamePopup();
	}
    public void ConsentNotGiven()
	{
        if (!isInitialized)
        {
            Debug.LogWarning("Initialization is not complete. Please wait.");
            return;
        }
		//AnalyticsService.Instance.StopDataCollection();
        consentMenu.gameObject.SetActive(false);
        ToggleNewGamePopup();
	}

    public void DeleteData(){
        AnalyticsService.Instance.StopDataCollection();
        AnalyticsService.Instance.RequestDataDeletion();
    }

    public void ToggleNewGamePopup()
    {
        startMenu.gameObject.SetActive(true);
    }
    public void ExitNewGamePopup()
    {
        startMenu.gameObject.SetActive(false);
    }
}
