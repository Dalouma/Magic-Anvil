using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image backgroundArt;
    [SerializeField] private Image icon;
    [SerializeField] private int index;

    private CraftedItem m_item;

    // Sets the appearance of the icon
    public void Set(CraftedItem item)
    {
        if (item == null)
        {
            Debug.Log("no item found while setting itemSlot data");
            m_item = null;
            icon.sprite = null;
            return;
        }

        m_item = item;
        icon.sprite = item.data.icon;

        // set background
    }

    // The function the button calls in order to set the Item Info Window
    public void SetItemWindow()
    {
        if (m_item == null)
        {
            transform.parent.gameObject.GetComponent<InventoryUI>().ResetDisplay();
            Debug.Log("Item Slot has no item");
            return;
        }
        transform.parent.gameObject.GetComponent<InventoryUI>().ViewItemInfo(m_item);
        transform.parent.gameObject.GetComponent<InventoryUI>().currentItemIndex = index;
    }

    // Removes the item that is held in this slot
    public void RemoveItem()
    {
        InventorySystem.instance.RemoveItem(index);
    }
}
