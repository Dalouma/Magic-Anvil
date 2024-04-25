using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class NewspaperManager : MonoBehaviour
{
    [SerializeField] private float scrollSensitivity;
    [SerializeField] private GameObject newsBottom;
    [SerializeField] private List<GameObject> prefabList;
    [SerializeField] private List<Sprite> newsImages;

    private CustomerData customerData;
    private int newsCount;
    private int sectionHeight = 530;
    private float yMin = 190;
    private float yMax = 520;

    Locale currentLocale;

    // Start is called before the first frame update
    void Start()
    {
        newsCount = 0;
        currentLocale = LocalizationSettings.SelectedLocale;
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

    // Generates the newspaper according to player's choices
    void GenerateNewspaper()
    {
        foreach (KeyValuePair<string, string> kvp in Weapon.chosenWeapons)
        {
            string customerName = kvp.Key;
            string chosenWeapon = kvp.Value;

            if (currentLocale.Identifier == "en")
            {
                if (customerName.Substring(customerName.Length - 2) != "Ch")
                {
                    LoadCustomer(customerName);

                    if (customerData.hatedWeapons.Any(item => item == chosenWeapon))
                    {
                        // Doesn't add section if customer left
                        continue;
                    }
                    if (customerData.preferredWeapons.Any(item => item == chosenWeapon))
                    {
                        // Customer Good Ending
                        AddSection(newsImages[customerData.newsOutcomes[0]], customerData.headline1, customerData.text1);
                    }
                    else
                    {
                        // Customer Bad Ending
                        AddSection(newsImages[customerData.newsOutcomes[1]], customerData.headline2, customerData.text2);
                    }
                }
            }
            else 
            {
                if (customerName.Substring(customerName.Length - 2) == "Ch")
                {
                    LoadCustomer(customerName);

                    if (customerData.hatedWeapons.Any(item => item == chosenWeapon))
                    {
                        // Doesn't add section if customer left
                        continue;
                    }
                    if (customerData.preferredWeapons.Any(item => item == chosenWeapon))
                    {
                        // Customer Good Ending
                        AddSection(newsImages[customerData.newsOutcomes[0]], customerData.headline1, customerData.text1);
                    }
                    else
                    {
                        // Customer Bad Ending
                        AddSection(newsImages[customerData.newsOutcomes[1]], customerData.headline2, customerData.text2);
                    }
                }
            }
        }

    }

    // Adds in a section to the newspaper
    void AddSection(Sprite newsImage, string headline, string details)
    {
        // Position of the section
        GameObject newSection = Instantiate(prefabList[newsCount % 2], this.transform);
        newSection.GetComponent<RectTransform>().anchoredPosition = Vector2.down * (sectionHeight * (newsCount+1) + 90);
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

    // Loads customer data in customerData var
    public void LoadCustomer(string customerName)
    {
        //string content;
        string filePath = Path.Combine(Application.streamingAssetsPath, "DialogueData/" + customerName + ".json");
        WWW www = new WWW(filePath);
        while (!www.isDone) { }
        //content = www.text;
        customerData = JsonUtility.FromJson<CustomerData>( www.text);
    }

    private void CanvasScrolling()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            print(touch.deltaPosition.y);
            if(touch.phase == TouchPhase.Moved)
            {
                float scrollValue = touch.deltaPosition.y;
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
}
