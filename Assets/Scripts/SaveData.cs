using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // make any public variables needed to store data to be saved
    // ...
    public int score;

    public SaveData (GameManager manager) 
    {
        // set public variables above to data from the manager
        // ...
        score = manager.score;
    }
}
