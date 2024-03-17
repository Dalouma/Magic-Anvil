using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewspaperManager : MonoBehaviour
{
    [SerializeField] private GameObject newsBottom;
    [SerializeField] private List<GameObject> prefabList;

    [SerializeField] private List<Sprite> newsImages;

    private int newsCount;
    private int sectionHeight = 530;

    // Start is called before the first frame update
    void Start()
    {
        newsCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateNewspaper();
        } 
    }

    void GenerateNewspaper()
    {
        addSection(newsImages[Random.Range(0, 5)], "Test Headline", "some random details that don't really have any meaning");
    }

    void addSection(Sprite newsImage, string headline, string details)
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
        // increment newscount
        newsCount++;
    }
}
