using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GemDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private string acceptedObj;
    [SerializeField] private GameObject confirmationWindow;

    private InventorySystem inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<InventorySystem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Detected Drop");
        Debug.Log(eventData.pointerDrag.transform.name);

        SocketGem draggedGem = eventData.pointerDrag.GetComponent<SocketGem>();
        if (draggedGem != null)
        {
            inventory.selectedGem = draggedGem.GetGemData();
            Debug.Log("Opening confirmation window");
            confirmationWindow.GetComponentInChildren<TMP_Text>().text = $"Socket {inventory.selectedGem.name} Gem?";
            confirmationWindow.SetActive(true);
        }
    }
}
