using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    public string chosenWeapon = "";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public void GoToForging()
    {
        Debug.Log("loading forging scene");
        SceneManager.LoadScene(1);
    }

    public void ChooseWeapon(string weapon)
    {
        chosenWeapon = weapon;
    }
}
