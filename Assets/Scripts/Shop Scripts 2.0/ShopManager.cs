using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    // instance reference
    public static ShopManager instance;

    // Only one instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadCustomers();
            LoadCurrency();
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    // Player Resources
    [Header("Status")]
    [SerializeField] private int money;
    [SerializeField] private int reputationValue;
    [SerializeField] private int wood;
    [SerializeField] private int iron;

    public int GetMoney() { return money; }
    public void AddMoney(int money) { this.money += money; }

    // List of Special Customers with news stories
    [Header("Special Customers")]
    [SerializeField] private List<CharacterData> specialCust;

    // Customer Queue
    public List<CharacterData> npcQueue { get; private set; }
    private int npcQueueIndex;

    // Sold Items
    public List<CraftedItem> itemsSold { get; private set; }
    
    //UI objects
    public UnityEngine.UI.Slider bar;
    public TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {

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
        npcQueue = new List<CharacterData>
        {
            specialCust[1],
            specialCust[2],
            specialCust[0]
        };
        itemsSold = new List<CraftedItem>();
        npcQueueIndex = 0;
        Customer customer = GameObject.FindGameObjectWithTag("customer").GetComponent<Customer>();
        customer.SetCharacter(npcQueue[npcQueueIndex]);
    }

    // This function will record the item sold to the current customer and stores the information in the queue
    public void RecordItem(CraftedItem item)
    {
        itemsSold.Add(new CraftedItem(item));
    }

    // This function makes the next customer in the queue appear
    public void NextCustomer()
    {
        npcQueueIndex++;
        // Move to newspaper scene
        if (npcQueueIndex == npcQueue.Count) { return; }

        Customer customer = GameObject.FindGameObjectWithTag("customer").GetComponent<Customer>();
        customer.SetCharacter(npcQueue[npcQueueIndex]);
    }
    public void SetRep(double rep)
    {
        bar.value = (int)rep;
    }
    public void UpdateMoney(int value)
    {
        money+= value;

    }

}
