using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GemSlot : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private GemData gemData;

    [Header("References")]
    [SerializeField] private Image gemImage;
    [SerializeField] private TMP_Text gemName;
    [SerializeField] private TMP_Text amountText;

    // Start is called before the first frame update
    void Start()
    {
        gemImage.sprite = gemData.gemArt;
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Refresh()
    {
        if (gemName != null)
            gemName.text = gemData.name + " Gem\t\tx" + InventorySystem.instance.GetGemAmount(gemData);
        if (amountText != null)
            amountText.text = "x" + InventorySystem.instance.GetGemAmount(gemData);
    }

}
