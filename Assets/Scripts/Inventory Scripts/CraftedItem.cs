using System;

[Serializable]
public class CraftedItem
{
    public ItemData data { get; private set; }
    public GemData gData { get; private set; }
    public int scoreVal { get; private set; }

    public CraftedItem(ItemData source, int score)
    {
        data = source;
        scoreVal = score;
        gData = null;
    }

    public CraftedItem(ItemData source, int score, GemData gem)
    {
        data = source;
        scoreVal = score;
        gData = gem;
    }

    public CraftedItem(CraftedItem original)
    {
        data = original.data;
        scoreVal = original.scoreVal;
        gData = original.gData;
    }

    public void Socket(GemData gem)
    {
        gData = gem;
    }

    public itemSaveData GetSaveData()
    {
        itemSaveData saveData = new itemSaveData();

        saveData.itemName = data.ID;
        saveData.gemName = gData.name;
        saveData.itemGrade = scoreVal;

        return saveData;
    }

}

[System.Serializable]
public struct itemSaveData
{
    public string itemName;
    public string gemName;
    public int itemGrade;
}