using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEditor.UIElements;
using System.ComponentModel;

public class ShopUI : MonoBehaviour
{
    [Header("Player Resource References")]
    [SerializeField] private TMP_Text nMoneyText;


    [Header("Crafting Menu References")]
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] private TMP_Text confirmationText;
    [SerializeField] private TMP_Text ItemMaterialsCost;

    [Header("Other References")]
    [SerializeField] private GameObject priceInputField;
    [SerializeField] private Button yesButtonChecker;

    //shop inventory data
    public List<TextMeshProUGUI> InventoryTextsInorder;
    public Dictionary<string, TextMeshProUGUI> inventoryTexts = new Dictionary<string, TextMeshProUGUI>();



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ShopManager.instance.materialsNames.Count; i++) 
        {
            inventoryTexts.Add(ShopManager.instance.materialsNames[i], InventoryTextsInorder[i]);
            if (ShopManager.instance.materialsInventory.ContainsKey(ShopManager.instance.materialsNames[i]))
            {
                inventoryTexts[ShopManager.instance.materialsNames[i]].text = ShopManager.instance.materialsInventory[ShopManager.instance.materialsNames[i]].ToString();
            }

              }
        RefreshMoney();
    }

    public void RefreshMoney() { nMoneyText.text = ShopManager.instance.GetMoney().ToString(); }

    public void SelectItem(ItemData item) 
    {
        ShopManager.instance.HoveredItem = item;
        string tempText = "";
        bool sellable = true;
        InventorySystem.instance.SelectItem(item);
        for(int i = 0;i<item.materials.Count;i++)
        {
            tempText = tempText + item.materials[i]+" x" + item.materialAmount[i].ToString()+", ";
            if (!ShopManager.instance.materialsInventory.ContainsKey(item.materials[i]) || ShopManager.instance.materialsInventory[item.materials[i]] < item.materialAmount[i])
            {
                sellable = false;
            }
        }
        ItemMaterialsCost.text = tempText;
        confirmationText.text = $"Craft a {item.ID}?"; 
        if(!sellable)
        {
            yesButtonChecker.interactable = false;
        }
        else
        {
            yesButtonChecker.interactable = true;
        }


    }
    public void incrementMaterials(string materialName)
    {
            ShopManager.instance.incrementMaterials(materialName);
        inventoryTexts[materialName].text = ShopManager.instance.materialsInventory[materialName].ToString();
        RefreshMoney();
    }
    public void ResetInventory()
    {
        ShopManager.instance.ResetInventory();
        foreach (string name in ShopManager.instance.materialsInventory.Keys)
        {
            inventoryTexts[name].text = ShopManager.instance.materialsInventory[name].ToString();
        }
        RefreshMoney();
    }
    public void makeItem()
    {
        ShopManager.instance.makeItem();

    }

    public void ShowPriceInputField()
    {
        priceInputField.SetActive(true);
    }
}
