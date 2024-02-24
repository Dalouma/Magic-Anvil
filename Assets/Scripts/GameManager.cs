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
            LoadGame();
        }
    }

    // create variables for any game specific data
    // ...
    public int score = 0;

    public void SaveGame() 
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame() 
    {
        SaveData data = SaveSystem.LoadGame();

        // set all variables back to loaded data
        // ...
        score = data.score;
    }

    public void AddScore() { score++; }

    public void RemoveScore() { score--; }

    public TMP_Text scoreText;

    void Update() { scoreText.SetText("Score: " + score); }
}
