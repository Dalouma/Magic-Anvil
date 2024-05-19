using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Player Resource References")]
    [SerializeField] private TMP_Text nMoneyText;

    [Header("Crafting Menu References")]
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] private TMP_Text confirmationText;

    [Header("Other References")]
    [SerializeField] private GameObject priceInputField;

    // Start is called before the first frame update
    void Start()
    {
        RefreshMoney();
    }

    public void RefreshMoney() { nMoneyText.text = ShopManager.instance.GetMoney().ToString(); }

    public void SelectItem(ItemData item) 
    {
        InventorySystem.instance.SelectItem(item);
        confirmationText.text = $"Craft a {item.ID}?"; 
    }

    public void ShowPriceInputField()
    {
        priceInputField.SetActive(true);
    }
}
