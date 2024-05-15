using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CounterItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image thisImage;
    [SerializeField]
    private Vector2 startPos;
    private CraftedItem item;

    // Start is called before the first frame update
    void Start()
    {
        thisImage = GetComponent<Image>();
        startPos = GetComponent<RectTransform>().anchoredPosition;
    }

    public CraftedItem GetItem() { return item; }
    public void SetItem(CraftedItem item)
    {
        this.item = item;
        thisImage.sprite = item.data.icon;
        thisImage.enabled = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        thisImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition = startPos;
        thisImage.raycastTarget = true;
    }
}
