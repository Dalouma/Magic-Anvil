using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // create variables for any game specific data
    // ...
    public int currency = 0;
    public int character = 0;

    public bool pullFromSave = false;

    public void SaveGame() 
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame() 
    {
        SaveData data = SaveSystem.LoadGame();

        // set all variables back to loaded data
        // ...
        currency = data.currency;
        character = data.character;
    }

    public void FromSave() 
    {
        pullFromSave = true;
    }
}
