using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEditor.UIElements;

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



    // Start is called before the first frame update
    void Start()
    {
        RefreshMoney();
    }

    public void RefreshMoney() { nMoneyText.text = ShopManager.instance.GetMoney().ToString(); }

    public void SelectItem(ItemData item) 
    {
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

    public void ShowPriceInputField()
    {
        priceInputField.SetActive(true);
    }
}
