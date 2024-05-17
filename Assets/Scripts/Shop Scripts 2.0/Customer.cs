using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// This is a wrapper class for the CharacterData for each customer
public class Customer : MonoBehaviour, IDropHandler
{
    [SerializeField] private CharacterData NPC;

    [Header("References")]
    [SerializeField] private Canvas speechCanvas;
    [SerializeField] private ShopUI shopUI;

    // Other References
    private TMP_Text nameBox;

    // Speech States
    private enum SpeechState
    {
        Intro,
        // Item responses
        Happy, Accept, Disappoint, Reject,
        // Haggling responses
        TooExpensive, HighPrice, MidPrice, LowPrice
    }

    // Speech Tracker Variables
    private TMP_Text speechBox;
    private SpeechState state;
    private int speechIndex;
    private Dictionary<SpeechState, string[]> responseDict;
    private bool talking;

    // Payment tracker
    private CraftedItem presentedItem;
    private int goodPrice, idealPrice, maxPrice;
    private int offeredPrice;

    private void Awake()
    {
        // Grab References
        speechBox = GameObject.FindGameObjectWithTag("SpeechBox").GetComponent<TMP_Text>();
        nameBox = GameObject.FindGameObjectWithTag("NameBox").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        // Setup Vars
        state = SpeechState.Intro;
        speechIndex = 0;
        talking = false;
    }

    public bool isTalking() { return talking; }
    public void SetTalking(bool state) { talking = state; }
    public void SetCharacter(CharacterData data)
    {
        NPC = data;
        nameBox.text = data.name;
        GetComponent<Image>().sprite = data.characterSprite;
        SetupResponses();
    }

    // Maps enum states to string arrays according to customer data
    private void SetupResponses()
    {
        responseDict = new Dictionary<SpeechState, string[]>()
        {
            { SpeechState.Intro, NPC.introText }
        };

        // Add additional responses if character has customerData
        if (NPC.customerData != null)
        {
            // Item responses
            responseDict.Add(SpeechState.Happy,         NPC.customerData.happyText);
            responseDict.Add(SpeechState.Accept,        NPC.customerData.acceptText);
            responseDict.Add(SpeechState.Disappoint,    NPC.customerData.disappointText);
            responseDict.Add(SpeechState.Reject,        NPC.customerData.rejectText);
            // Price responses
            responseDict.Add(SpeechState.TooExpensive,  NPC.customerData.tooExpensive);
            responseDict.Add(SpeechState.HighPrice,     NPC.customerData.highPrice);
            responseDict.Add(SpeechState.MidPrice,      NPC.customerData.midPrice);
            responseDict.Add(SpeechState.LowPrice,      NPC.customerData.lowPrice);
        }
    }

    // Progresses active speech box to display next set of sentences
    public void ProgressText()
    {
        //Debug.Log("progressing text at index " + speechIndex);
        var currentResponseArray = responseDict[state];
        if (speechIndex < currentResponseArray.Length)
        {
            speechBox.text = currentResponseArray[speechIndex];
            speechIndex++;
        }
        else
            TurnOffSpeechBox();
    }

    // Opens speech box. This is called in ReceiveItem().
    private void TurnOnSpeechBox()
    {
        ProgressText();
        talking = true;
        speechCanvas.enabled = true;
        GetComponent<Image>().raycastTarget = false;
    }

    // Closes speech box; resets speech index; reactivates character button
    private void TurnOffSpeechBox()
    {
        speechBox.text = "...";
        speechIndex = 0;
        talking = false;
        GetComponent<Image>().raycastTarget = true;
        speechCanvas.enabled = false;

        
        // Customer leaves if state is in Reject
        if (state == SpeechState.Reject)
        {
            ShopManager.instance.NextCustomer();
        }
        // Let player offer a price
        else if (state == SpeechState.Happy || state == SpeechState.Accept || state == SpeechState.Disappoint)
        {
            shopUI.ShowPriceInputField();
        }
        // Will give player another opportunity to offer price unless too annoyed
        else if (state == SpeechState.TooExpensive)
        {
            shopUI.ShowPriceInputField();
            // implement incrementing annoyance value later
        }
        // Pays player; records item sold; removes item from inventory; customer leaves
        else if (state == SpeechState.HighPrice || state == SpeechState.MidPrice || state == SpeechState.LowPrice)
        {
            // pay player
            ShopManager.instance.AddMoney(offeredPrice);
            shopUI.RefreshMoney();
            // record item
            ShopManager.instance.RecordItem(presentedItem);
            // remove item from inventory
            GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<InventoryUI>().SellItem();
            GameObject.FindGameObjectWithTag("item").GetComponent<Image>().enabled = false;
            // customer leave
            ShopManager.instance.NextCustomer();
        }

    }

    // Detects the player dropping the item on the character
    // Only runs if the character has customer data
    public void OnDrop(PointerEventData eventData)
    {
        if (NPC.customerData != null && eventData.pointerDrag.CompareTag("item"))
        {
            presentedItem = eventData.pointerDrag.GetComponent<CounterItem>().GetItem();
            ReceiveItem(presentedItem);
            CalculatePriceThreshold();
        }
    }
    
    // Determines next state and response depending on which item is received
    private void ReceiveItem(CraftedItem item)
    {
        // Rejected Items
        foreach (ItemData rejectedItem in NPC.customerData.rejectedItems)
        {
            if (item.data.ID == rejectedItem.ID)
            {
                state = SpeechState.Reject;
                TurnOnSpeechBox();
                return;
            }
        }
        // Preferred Items
        foreach (ItemData preferredItem in NPC.customerData.preferredItems)
        {
            if (item.data.ID == preferredItem.ID)
            {
                state = SpeechState.Happy;
                TurnOnSpeechBox();
                return;
            }
        }
        // Accepted Items (default)
        state = SpeechState.Accept;
        TurnOnSpeechBox();
    }

    // This function calculates the pricing thresholds that the customer tolerates
    // It is currently set to arbitrary numbers for testing
    // This should take into account customer's stinginess
    private void CalculatePriceThreshold()
    {
        maxPrice = 60;
        idealPrice = 40;
        goodPrice = 20;
    }

    // This function is called after the player offers a price by finishing the input field
    public void OfferPrice(string input)
    {
        if (input == null) { return; }

        offeredPrice = int.Parse(input);

        if (offeredPrice > maxPrice)
            state = SpeechState.TooExpensive;
        else if (offeredPrice > idealPrice)
            state = SpeechState.HighPrice;
        else if (offeredPrice > goodPrice)
            state = SpeechState.MidPrice;
        else
            state = SpeechState.LowPrice;

        TurnOnSpeechBox();
    }

    
}
