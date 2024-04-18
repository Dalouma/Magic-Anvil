using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftedItem
{
    public ItemData data { get; private set; }
    public int scoreVal { get; private set; }


    
    public CraftedItem(ItemData source, int score)
    {
        data = source;
        scoreVal = score;
    }

}
