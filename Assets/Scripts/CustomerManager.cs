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
    //to make chosenWeopon reset after returning to shop (doesnt work yet)
    /*private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
     private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "ShopCounterScene"){
            chosenWeapon = "";
        }
    }*/
    public void GoToForging(string weapon)
    {
        ChooseWeapon(weapon);
        Debug.Log("loading forging scene");
        SceneManager.LoadScene(1);
    }

    public void ChooseWeapon(string weapon)
    {
        chosenWeapon = weapon;
    }
}
