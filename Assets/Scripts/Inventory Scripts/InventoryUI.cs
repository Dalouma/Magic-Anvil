using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ItemInfoWindow;

    private TMP_Text itemDisplayName;
    private Image borderImage;
    private Image gemEffectImage;
    private Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        //SetupIcons();
        GrabReferences();
    }

    // Grabs references for Item Information Window
    private void GrabReferences()
    {
        itemDisplayName = ItemInfoWindow.transform.GetChild(0).GetComponent<TMP_Text>();
        borderImage = ItemInfoWindow.transform.GetChild(1).GetComponent<Image>();
        gemEffectImage = ItemInfoWindow.transform.GetChild(2).GetComponent<Image>();
        itemImage = ItemInfoWindow.transform.GetChild(3).GetComponent<Image>();
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
        for(int i = 0; i < InventorySystem.instance.GetItemCount(); i++)
        {
            ItemSlot currentSlot = transform.GetChild(i + 1).gameObject.GetComponent<ItemSlot>();
            CraftedItem item = InventorySystem.instance.GetItem(i);
            currentSlot.Set(item);
        }
    }

    // Open up Item Information Window
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

        // enable info canvas
        ItemInfoWindow.GetComponent<Canvas>().enabled = true;
    }
}
