using System.Collections;
using System.Collections.Generic;
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

    [Header("Button References")]
    [SerializeField] private GameObject socketButton;
    [SerializeField] private GameObject sellButton;

    [Header("Variables")]
    public int currentItemIndex;

    // Start is called before the first frame update
    void Start()
    {
        //SetupIcons();
        currentItemIndex = -1;
        ResetDisplay();
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
            Debug.Log("Space pressed");
            RefreshIcons();
        }
    }

    // Gives each Item Slot the item data from the inventory
    private void RefreshIcons()
    {
        for(int i = 0; i < 6; i++)
        {
            ItemSlot currentSlot = transform.GetChild(i + 1).gameObject.GetComponent<ItemSlot>();

            CraftedItem item;
            if (i < InventorySystem.instance.GetItemCount())
                item = InventorySystem.instance.GetItem(i);
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
            itemPrefix = item.data.itemGrades[1] + " ";
        if (item.scoreVal < 1000)
            itemPrefix = item.data.itemGrades[0] + " ";
        
        itemDisplayName.text = itemPrefix + item.data.ID;

        // change item image
        itemImage.sprite = item.data.fullArt;

        // turn on buttons
        socketButton.SetActive(true);
        sellButton.SetActive(true);
    }

    public void ResetDisplay()
    {
        itemDisplayName.text = "";
        itemImage.sprite = null;
        
        socketButton.SetActive(false);
        sellButton.SetActive(false);
    }

    public void SellItem()
    {
        InventorySystem.instance.RemoveItem(currentItemIndex);
        RefreshIcons();
        ResetDisplay();
    }
}
