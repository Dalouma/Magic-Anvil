using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // instance reference
    public static InventorySystem instance;

    private List<CraftedItem> inventory;
    private int maxSize = 10;

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

    private void Update()
    {
        TestKeys();
    }

    // Test Keys (DELETE OUT OF UPDATE FUNCTION LATER)
    private void TestKeys()
    {
        // Crafts a random item with random grade
        if (Input.GetKeyDown(KeyCode.V))
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
    }

    // sets chosen weapon and sets item score to 0
    public void StartCrafting(ItemData itemData)
    {
        itemScore = 0;
        itemType = itemData;

        // scene switches to forging scene afterwards
    }

    // Adds Finished item stats to inventory
    public void FinishCrafting()
    {
        inventory.Add(new CraftedItem(itemType, itemScore));

        // scene switch to shop
    }

    // Returns CraftedItem by index from inventory
    public CraftedItem GetItem(int index)
    {
        if (index > inventory.Count-1)
            return null;
        return inventory[index];
    }

    // Gets number of items currently in inventory
    public int GetItemCount() { return inventory.Count;}
    public bool FullInventory() { return inventory.Count >= maxSize; }
}
