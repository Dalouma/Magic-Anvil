using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    // instance reference
    public static ShopManager instance;

    // Player Resources
    [Header("Status")]
    [SerializeField] private int money;
    [SerializeField] public double reputationValue;
    [SerializeField] public Dictionary<string, int> materialsInventory = new Dictionary<string, int>();
    [SerializeField] public ItemData HoveredItem;

    // List of Special Customers with news stories
    [Header("Special Customers")]
    [SerializeField] private List<CharacterData> specialCust;

    // Customer Queue
    public List<CharacterData> npcQueue { get; private set; }
    private int npcQueueIndex;

    // Record of Items sold
    public List<CraftedItem> itemsSold { get; private set; }

    //materials data
    public List<string> materialsNames;
    public List<int> materialsCostInOrder;
    public Dictionary<string, int> MaterialsData = new Dictionary<string, int>();

    // UI objects
    public UnityEngine.UI.Slider bar;
    public TextMeshProUGUI moneyText;

    // Only one instance
    private void Awake()
    {
        //moneyText = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
        if (instance == null)
        {
            instance = this;
            //LoadCustomers();
            BasicQueue();
            LoadCurrency();
            for(int i=0;i<materialsNames.Count;i++)
            {
                MaterialsData.Add(materialsNames[i], materialsCostInOrder[i]);
                
            }
            SetRep(reputationValue);
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        //DontDestroyOnLoad(this);
    }

    public int GetMoney() { return money; }
    public void UpdateMoney(int nmoney) 
    { 
        money += nmoney;
       if (moneyText != null)
        {
            moneyText.text = money.ToString();
        }
    }

    // This function should load the currency from save data.
    // For now it will set them to 0
    private void LoadCurrency()
    {
        money = 30;
        reputationValue = 50;
    }

    // This function should load the queue of customers from save data.
    // For now it will load the 3 current customers
    private void LoadCustomers()
    {
        
    }

    // This function will record the item sold to the current customer and stores the information in the queue
    public void RecordItem(CraftedItem item) 
    { 
        if (item != null)
        {
            itemsSold.Add(new CraftedItem(item));
        }
        else { itemsSold.Add(null); }
        
    }

    public void ResetQueue() { npcQueue.Clear(); }
    public void ResetRecords() { itemsSold.Clear(); }
    public void BasicQueue()
    {
        npcQueue = new List<CharacterData>
        {
            specialCust[0],
            specialCust[1],
            specialCust[2]
        };
        itemsSold = new List<CraftedItem>();
        npcQueueIndex = 0;
    }

    public void RobberyCheck()
    {
        if(Random.Range(0,10) <= 10)
        {
            GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().FadeToLevel("RobberyScene");
        }
    }

    public CharacterData GetCurrentCharacter() { return npcQueue[npcQueueIndex]; }

    // This function makes the next customer in the queue appear
    public void NextCustomer()
    {
        // Move to newspaper scene if queue is ended
        if (npcQueueIndex == npcQueue.Count - 1)
        {
            GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().FadeToLevel("NewspaperScene");
            return;
        }

        npcQueueIndex++;
        Customer customer = GameObject.FindGameObjectWithTag("customer").GetComponent<Customer>();
        customer.SetCharacter(npcQueue[npcQueueIndex]);
    }
    public void SetRep(double rep)
    {
        reputationValue = rep;
    }
    public void changeRep(double rep)
    {
        reputationValue += +rep;
    }
    public void incrementMaterials(string materialName)
    {
       if (money >= MaterialsData[materialName]) { 
        if (!materialsInventory.ContainsKey(materialName))
        {
            materialsInventory.Add(materialName, 0);
        }

        materialsInventory[materialName]++;
        UpdateMoney(-(MaterialsData[materialName]));
    }
    }
    public void decrementMaterials(string materialName)
    {
        materialsInventory[materialName]--;
        //inventoryTexts[materialName].text = materialsInventory[materialName].ToString();
    }
    public void makeItem()
    {
        for (int i = 0; i < HoveredItem.materials.Count; i++)
        {
            materialsInventory[HoveredItem.materials[i]] -= HoveredItem.materialAmount[i];
        }

    }
    public void ResetInventory()
    {

        List<string> keys = new List<string>(materialsInventory.Keys);

        foreach (string name in keys)
            {
                UpdateMoney(materialsInventory[name] * MaterialsData[name]);
                materialsInventory[name] = 0;
                //inventoryTexts[name].text = materialsInventory[name].ToString();
            }
        
    }

}
