using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class NewspaperManager : MonoBehaviour
{
    [SerializeField] private GameObject newsBottom;
    [SerializeField] private List<GameObject> prefabList;
    [SerializeField] private List<Sprite> newsImages;

    //private CustomerData customerData;
    private int scrollSensitivity = -20;
    private int newsCount;
    private int sectionHeight = 530;
    private float yMin = 120;
    private float yMax = 520;

    //Locale currentLocale;

    // Start is called before the first frame update
    void Start()
    {
        newsCount = 0;
        //currentLocale = LocalizationSettings.SelectedLocale;
        GenerateNewspaper();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Touch Phase: " + touch.phase);
        }
        CheatKeys();
        CanvasScrolling();
    }

    void CheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GenerateNewspaper();
            AddSection(newsImages[0], "Test Headline", "Some more random text for testing the details section of the newspaper");
        }
    }

    // Generates a newspaper according to the list of customers
    void GenerateNewspaper()
    {
        for (int i = 0; i < ShopManager.instance.npcQueue.Count; i++)
        {
            CharacterData npc = ShopManager.instance.npcQueue[i];
            CraftedItem item = ShopManager.instance.itemsSold[i];

            if (item == null) { continue; }

            bool generatedSection = false;
            CustomerData customer = npc.customerData;

            foreach (ItemData preferredItem in npc.customerData.preferredItems)
            {
                if (item.data.ID == preferredItem.ID)
                {
                    AddSection(customer.storyGraphics[0], customer.headlines[0], customer.storyText[0]);
                    generatedSection = true;
                    break;
                }
            }
            if (generatedSection) { continue; }
            // Section if item sold was not preferred
            AddSection(customer.storyGraphics[1], customer.headlines[1], customer.storyText[1]);

        }

    }

    // Adds in a section to the newspaper
    void AddSection(Sprite newsImage, string headline, string details)
    {
        // Position of the section
        GameObject newSection = Instantiate(prefabList[newsCount % 2], this.transform);
        newSection.GetComponent<RectTransform>().anchoredPosition = Vector2.down * (sectionHeight * (newsCount + 1) + 90);
        // Image
        newSection.transform.GetChild(0).GetComponent<Image>().sprite = newsImage;
        // Headline
        newSection.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = headline;
        // Details
        newSection.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = details;
        // Shifts the bottom of the scroll to make space for new section
        newsBottom.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -sectionHeight);

        newsCount++;
        yMax += sectionHeight;
    }

    private void CanvasScrolling()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            print(touch.deltaPosition.y);
            if (touch.phase == TouchPhase.Moved)
            {
                float scrollValue = touch.deltaPosition.y * (scrollSensitivity);
                if (scrollValue != 0)
                {
                    float height = rectTransform.anchoredPosition.y;
                    float dHeight = height - scrollValue;
                    float newHeight = Mathf.Clamp(dHeight, yMin, yMax);

                    rectTransform.anchoredPosition = new Vector2(0, newHeight);
                }
            }
        }
    }

    public void OnEndDay()
    {
        ShopManager.instance.ResetQueue();
        ShopManager.instance.ResetRecords();
        ShopManager.instance.BasicQueue();
    }
}
