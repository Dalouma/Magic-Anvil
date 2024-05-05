using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SocketGem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private GemData gemData;
    [SerializeField] private GameObject movableGem;
    private Image gemImage;

    // Start is called before the first frame update
    void Start()
    {
        gemImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        gemImage.raycastTarget = false;
        movableGem.GetComponent<Image>().sprite = gemData.gemArt;
        movableGem.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        movableGem.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gemImage.raycastTarget = true;
        movableGem.SetActive(false);
    }
}
