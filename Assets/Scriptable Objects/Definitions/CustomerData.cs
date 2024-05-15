using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "ScriptableObjects/Customer")]
public class CustomerData : ScriptableObject
{
    public ItemData[] preferredItems;
    public ItemData[] rejectedItems;

    // Speech Text
    [Header("Item responses")]
    [TextArea(3, 5)] public string[] happyText;
    [TextArea(3, 5)] public string[] acceptText;
    [TextArea(3, 5)] public string[] disappointText;
    [TextArea(3, 5)] public string[] rejectText;

    [Header("Haggling response")]
    [TextArea(3, 5)] public string[] goodDeal;
    [TextArea(3, 5)] public string[] neutralDeal;
    [TextArea(3, 5)] public string[] badDeal;

    [Header("Newspaper outcomes")]
    public string[] headlines;
    [TextArea(3, 5)] public string[] storyText;
}