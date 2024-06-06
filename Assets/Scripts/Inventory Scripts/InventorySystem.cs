using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySystem : MonoBehaviour
{
    // instance reference
    public static InventorySystem instance;

    private List<CraftedItem> inventory;
    private int maxSize = 6;

    private Dictionary<string, int> gemInventory;
    public List<GemData> gemTypes;

    [Header("Current item being crafted")]
    [SerializeField] private ItemData itemType;
    [SerializeField] private int itemScore;

    [Header("Selected Gem for Socket")]
    public GemData selectedGem;

    [Header("For Testing")]
    [SerializeField] private List<ItemData> testItems;


    // Only 1 instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize Item Inventory
            LoadInventory();

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
        itemType = testItems[0];

    }

    // Currently initializes an empty item inventory as an empty list of type CraftedItem
    // This should load items from save data in the future
    private void LoadInventory()
    {
        inventory = new List<CraftedItem>();
    }

    // Currently this initializes an empty gem inventory using a dictionary with (gemData, int) kvp
    // This should load gems from save data in the future
    private void LoadGems()
    {
        gemInventory = new Dictionary<string, int>();
        for (int i = 0; i < gemTypes.Count; i++)
        {
            gemInventory.Add(gemTypes[i].name, 2);
        }

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

    public void GenerateFullSet()
    {
        if (inventory.Count > 1)
        {
            Debug.Log("Too many items in inventory!!!");
            return;
        }
        foreach (ItemData item in testItems)
        {
            itemType = item;
            itemScore = 3000;
            FinishCrafting();
        }
        Debug.Log("generated perfect set of items");
    }

    public void SelectItem(ItemData item) { itemType = item; }
    public void AddScore(int score) { itemScore += score; }

    // Returns the itemData of the item currently being crafted
    public ItemData GetCurrentItem()
    {
        return itemType;
    }

    // Adds Finished item stats to inventory
    public void FinishCrafting()
    {
        inventory.Add(new CraftedItem(itemType, itemScore));
    }

    // Returns CraftedItem by index from inventory
    public CraftedItem GetItemAt(int index) { return inventory[index]; }

    // Removes Crafted Item by index from inventory
    public void RemoveItem(int index) { inventory.RemoveAt(index); }

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

    // Returns list of all crafted items in inventory
    public List<CraftedItem> GetInventory() { return inventory; }
    // Return true if the inventory is full
    public bool FullInventory() { return inventory.Count >= maxSize; }
    // Gets number of items currently in inventory
    public int GetItemCount() { return inventory.Count; }
}
