using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // instance reference
    public static InventorySystem instance;

    private List<CraftedItem> inventory;
    private int maxSize = 6;

    private Dictionary<string, int> gemInventory;
    [SerializeField] private List<GemData> gemTypes;

    [Header("Current item being crafted")]
    [SerializeField] private ItemData itemType;
    [SerializeField] private int itemScore;

    [Header("For Testing")]
    [SerializeField] private List<ItemData> testItems;


    // Only 1 instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize Gem Inventory
            LoadGems();
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        itemScore = 0;
        itemType = null;

        // Initialize Inventory 
        inventory = new List<CraftedItem>();
        
        

    }

    // Currently this initializes an empty gem inventory using a dictionary with (gemData, int) kvp
    // This should load gems from save data in the future
    private void LoadGems()
    {
        gemInventory = new Dictionary<string, int>();
        for (int i = 0; i < gemTypes.Count; i++)
        {
            //Debug.Log("adding " + gemTypes[i].name + " to dictionary");
            gemInventory.Add(gemTypes[i].name, 0);
        }
            
    }

    private void Update()
    {
        //TestKeys();
    }

    // Test Keys (DELETE OUT OF UPDATE FUNCTION LATER)
    private void TestKeys()
    {
        
    }

    // Generates a random item for testing purposes
    public void GenerateRandomItem()
    {
        if (inventory.Count >= maxSize)
        {
            Debug.Log("Inventory Full!!!");
            return;
        }
        itemType = testItems[Random.Range(0, testItems.Count)];
        itemScore = Random.Range(0, 3000);
        FinishCrafting();
        Debug.Log("Created " + itemType.ID + " with grade " + itemScore);
    }

    // sets chosen weapon and sets item score to 0
    public void StartCrafting(ItemData itemData)
    {
        itemScore = 0;
        itemType = itemData;
    }

    // Adds Finished item stats to inventory
    public void FinishCrafting()
    {
        inventory.Add(new CraftedItem(itemType, itemScore));
    }

    // Returns CraftedItem by index from inventory
    public CraftedItem GetItem(int index)
    {
        if (index > inventory.Count - 1)
            return null;
        return inventory[index];
    }

    // Removes Crafted Item by index from inventory
    public void RemoveItem(int index)
    {
        inventory.RemoveAt(index);
    }

    // Get number of gems currently possessed by player by accessing dictionary
    public int GetGemAmount(GemData type)
    {
        return gemInventory[type.name];
    }

    // Add or Subtract gems by type
    public void ChangeGemAmount(GemData type, int amount)
    {
        gemInventory[type.name] = gemInventory[type.name] + amount;
    }

    // Gets number of items currently in inventory
    public int GetItemCount() { return inventory.Count;}
    public bool FullInventory() { return inventory.Count >= maxSize; }
}
