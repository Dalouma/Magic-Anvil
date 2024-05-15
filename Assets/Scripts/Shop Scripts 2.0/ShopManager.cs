using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // List of Special Customers with news stories
    [Header("Special Customers")]
    [SerializeField] private List<CharacterData> specialCust;

    // Customer Queue
    public List<(CharacterData, CraftedItem)> npcQueue { get; private set; }
    public int npcQueueIndex;

    // Start is called before the first frame update
    void Start()
    {
        NextCustomer();
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
        npcQueue = new List<(CharacterData, CraftedItem)>
        {
            (specialCust[1], null),
            (specialCust[2], null),
            (specialCust[0], null)
        };
        npcQueueIndex = 0;
    }

    // This function makes the next customer in the queue appear
    public void NextCustomer()
    {
        Customer customer = GameObject.FindGameObjectWithTag("customer").GetComponent<Customer>();
        customer.SetCharacter(npcQueue[npcQueueIndex].Item1);
    }

}
