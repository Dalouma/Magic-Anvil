using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Craftable Items")]
    [SerializeField] private List<ItemData> items;

    [Header("Player Resource References")]
    [SerializeField] private TMP_Text nMoneyText;

    [Header("Crafting Menu References")]
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] private TMP_Text confirmationText;

    [Header("Other References")]
    [SerializeField] private GameObject priceInputField;

    // Variables
    private ItemData selectedItem;

    // Start is called before the first frame update
    void Start()
    {
        RefreshMoney();
    }

    public void RefreshMoney() { nMoneyText.text = ShopManager.instance.GetMoney().ToString(); }

    public void SelectItem(int itemIndex)
    {
        selectedItem = items[itemIndex];
        confirmationText.text = $"Craft a {selectedItem.ID}?";
    }

    public void StartCrafting()
    {
        InventorySystem.instance.StartCrafting(selectedItem);
        SceneManager.LoadScene("ForgingScene");
    }

    public void ShowPriceInputField()
    {
        priceInputField.SetActive(true);
    }
}
