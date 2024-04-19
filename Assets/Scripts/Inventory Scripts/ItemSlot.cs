using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image backgroundArt;
    [SerializeField] private Image icon;

    private CraftedItem m_item;

    // Sets the appearance of the icon
    public void Set(CraftedItem item)
    {
        if (item == null)
        {
            Debug.Log("no item found whil setting itemSlot data");
            return;
        }

        m_item = item;

        // set icon
        icon.sprite = item.data.icon;

        // set background
    }

    // The function the button calls in order to set the Item Info Window
    public void SetItemWindow()
    {
        if (m_item == null)
        {
            Debug.Log("Item Slot has no item");
            return;
        }
        transform.parent.gameObject.GetComponent<InventoryUI>().ViewItemInfo(m_item);
    }
}
