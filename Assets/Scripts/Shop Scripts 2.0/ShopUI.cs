using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Craftable Items")]
    [SerializeField] private List<ItemData> items;

    [Header("References")]
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] private TMP_Text confirmationText;
    

    // Variables
    private ItemData selectedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
