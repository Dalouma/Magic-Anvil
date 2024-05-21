using UnityEngine;

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
            DontDestroyOnLoad(gameObject);
        }
    }

    // create variables for any game specific data
    // ...
    public int currency = 450;
    public int character = 0;

    // Customer Queue save data
    public string[] customerQueue;
    public itemSaveData[] givenItems;
    public int queueIndex;
    public int dayCount;

    // Inventory save data
    public itemSaveData[] inventorySaveData;

    // Gem Inventory save data
    public (string, int)[] gemInventory;

    // Analytics save data
    public bool tracking;
    public bool consent;


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
        //currency = data.currency;
        //character = data.character;
    }

    public void FromSave()
    {
        pullFromSave = true;
    }

    

}
