using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GemSlot : MonoBehaviour
{
    [SerializeField] private GemData gemData;
    [SerializeField] private bool draggable;

    [Header("References")]
    [SerializeField] private Image gemImage;
    [SerializeField] private TMP_Text gemName;

    // Start is called before the first frame update
    void Start()
    {
        gemImage.sprite = gemData.gemArt;
        gemName.text = gemData.name + " Gem\t\tx" + InventorySystem.instance.GetGemAmount(gemData);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
