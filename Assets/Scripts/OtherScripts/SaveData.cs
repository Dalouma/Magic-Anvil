[System.Serializable]
public class SaveData
{
    // make any public variables needed to store data to be saved
    // ...
    public int currency;
    //public int character;
    public itemSaveData[] inventorySaveData;

    // Customer queue data
    public string[] customerQueue;
    public itemSaveData[] givenItems;
    public int queueIndex;


    public SaveData(GameManager manager)
    {
        // set public variables above to data from the manager
        // ...
        currency = manager.currency;
        //character = manager.character;
    }
}
