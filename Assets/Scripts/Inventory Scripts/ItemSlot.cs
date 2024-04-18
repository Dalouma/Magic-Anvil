using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image backgroundArt;
    [SerializeField] private Image icon;

    public void Set(CraftedItem item)
    {
        if (item == null)
        {
            Debug.Log("no item found whil setting itemSlot data");
            return;
        }

        // set icon
        icon.sprite = item.data.icon;

        // set background
    }
}
