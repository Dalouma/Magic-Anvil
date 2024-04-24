using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(GameManager manager) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/manager.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadGame() 
    {
        string path = Application.persistentDataPath + "/manager.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
