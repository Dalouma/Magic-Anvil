using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Display References")]
    [SerializeField] private TMP_Text itemDisplayName;
    [SerializeField] private Image borderImage;
    [SerializeField] private Image gemEffectImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image gemSocketIndicator;
    [SerializeField] private GameObject gemPanel;

    [Header("Button References")]
    [SerializeField] private GameObject socketButton;
    [SerializeField] private GameObject sellButton;

    [Header("Empty Gem Effect Background")]
    [SerializeField] private Sprite emptyGemEffect;

    [Header("Status")]
    public int currentItemIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentItemIndex = -1;
        ResetDisplay();
        RefreshIcons();
    }

    // Update is called once per frame
    void Update()
    {
        TestingKeys();
    }

    // Testing keys (REMOVE FROM UPDATE FUNCTION LATER)
    private void TestingKeys()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InventorySystem.instance.GenerateFullSet();
            RefreshIcons();
        }

        // Crafts a random item with random grade
        if (Input.GetKeyDown(KeyCode.V))
        {
            InventorySystem.instance.GenerateRandomItem();
            RefreshIcons();
        }
    }

    // Refreshes the gem amounts in the Gem Selection Panel
    private void RefreshGems()
    {
        for (int i = 0; i < gemPanel.transform.childCount; i++)
        {
            GemSlot gem = gemPanel.transform.GetChild(i).gameObject.GetComponent<GemSlot>();
            if (gem != null)
                gem.Refresh();
        }
    }

    // Gives each Item Slot the item data from the inventory
    private void RefreshIcons()
    {
        for (int i = 0; i < 6; i++)
        {
            ItemSlot currentSlot = transform.GetChild(i).gameObject.GetComponent<ItemSlot>();

            CraftedItem item;
            if (i < InventorySystem.instance.GetItemCount())
                item = InventorySystem.instance.GetItemAt(i);
            else
                item = null;
            currentSlot.Set(item);
        }
    }

    // Display Item Information on the right
    public void ViewItemInfo(CraftedItem item)
    {
        // determine item grade prefix (weak, strong)
        string itemPrefix = "";
        if (item.scoreVal >= 2000)
            itemPrefix = item.data.itemGrades[1];
        if (item.scoreVal < 1000)
            itemPrefix = item.data.itemGrades[0];

        itemDisplayName.text = $"{itemPrefix} {item.data.ID}";

        // set item gem affix text
        if (item.gData != null)
            itemDisplayName.text += $" {item.gData.affixText}";

        // change background art
        if (item.gData != null)
        {
            gemEffectImage.sprite = item.gData.backgroundArt;
            gemSocketIndicator.enabled = true;
        }
        else
        {
            gemEffectImage.sprite = emptyGemEffect;
            gemSocketIndicator.enabled = false;
        }

        // change item image
        itemImage.sprite = item.data.fullArt;
        itemImage.enabled = true;

        // turn on buttons
        socketButton.SetActive(true);
        sellButton.SetActive(true);
    }

    // Resets display 
    public void ResetDisplay()
    {
        itemDisplayName.text = "";
        gemEffectImage.sprite = emptyGemEffect;
        itemImage.enabled = false;
        gemSocketIndicator.enabled = false;

        socketButton.SetActive(false);
        sellButton.SetActive(false);
    }

    // Place item on counter
    // Called when player presses Sell button in inventory
    public void PlaceItem()
    {
        CraftedItem toPlace = InventorySystem.instance.GetItemAt(currentItemIndex);
        CounterItem itemOnCounter = GameObject.FindGameObjectWithTag("item").GetComponent<CounterItem>();
        if (itemOnCounter == null)
        {
            Debug.Log("item on counter not found");
            return;
        }
        itemOnCounter.SetItem(toPlace);
    }

    // Removes item from inventory and refreshes display
    public void SellItem()
    {
        InventorySystem.instance.RemoveItem(currentItemIndex);
        RefreshIcons();
        ResetDisplay();
    }

    // Attaches currently selected item with gem that was dragged in.
    public void SocketItem()
    {
        GemData gem = InventorySystem.instance.selectedGem;
        CraftedItem item = InventorySystem.instance.GetItemAt(currentItemIndex);
        item.Socket(gem);

        InventorySystem.instance.ChangeGemAmount(gem, -1);

        ViewItemInfo(item);
        RefreshIcons();
        RefreshGems();
    }
}
