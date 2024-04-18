using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // instance reference
    public static InventorySystem instance;


    private List<CraftedItem> inventory;
    private int maxSize = 10;

    private ItemData itemType;
    private int itemScore;


    // Only 1 instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory= new List<CraftedItem>();
        itemScore= 0;
        itemType = null;
    }

    public void StartCrafting(ItemData itemData)
    {
        itemScore = 0;
        itemType = itemData;

        // scene switch to forging scene
    }

    public void FinishCrafting()
    {
        inventory.Add(new CraftedItem(itemType, itemScore));

        // scene switch to shop
    }
}
