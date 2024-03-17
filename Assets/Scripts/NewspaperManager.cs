using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperManager : MonoBehaviour
{
    [SerializeField] private GameObject newsBottom;
    [SerializeField] private List<GameObject> prefabList;

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
            addSection();
        } 
    }

    void GenerateNewspaper()
    {

    }

    void addSection()
    {
        GameObject newSection = Instantiate(prefabList[newsCount % 2], this.transform);
        newSection.GetComponent<RectTransform>().anchoredPosition = Vector2.down * (sectionHeight * (newsCount+1) + 90);
        newsCount++;

        newsBottom.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -sectionHeight);
    }
}
