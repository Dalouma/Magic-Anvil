using UnityEngine;
using System.IO;
using System;

public static class SaveSystem
{
    private static readonly string SavePath = Application.persistentDataPath + "/manager.json";

    public static void SaveGame(GameManager manager) 
    {
        try
        {
            SaveData data = new SaveData(manager);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(SavePath, json);
            Debug.Log("Game saved successfully to " + SavePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save game: " + e.Message);
        }
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(SavePath))
        {
            try
            {
                string json = File.ReadAllText(SavePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load game: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + SavePath);
            return null;
        }
    }
}
