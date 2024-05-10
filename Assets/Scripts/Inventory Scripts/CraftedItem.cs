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

    public void Socket(GemData gem)
    {
        gData = gem;
    }

}
