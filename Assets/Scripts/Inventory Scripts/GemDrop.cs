using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GemDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private string acceptedObj;
    [SerializeField] private GameObject confirmationWindow;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Detected Drop");
        //Debug.Log(eventData.pointerDrag.transform.name);

        SocketGem draggedGem = eventData.pointerDrag.GetComponent<SocketGem>();
        if (draggedGem != null)
        {
            InventorySystem.instance.selectedGem = draggedGem.GetGemData();
            //Debug.Log("Opening confirmation window");
            confirmationWindow.GetComponentInChildren<TMP_Text>().text = $"Socket {InventorySystem.instance.selectedGem.name} Gem?";
            confirmationWindow.SetActive(true);
        }
    }
}
