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

    // Other References
    private TMP_Text nameBox;

    // Speech States
    private enum SpeechState
    {
        Intro,
        // Item responses
        Happy, Accept, Disappoint, Reject,
        // Haggling responses
        GoodDeal, NeutralDeal, BadDeal
    }

    // Speech Tracker Variables
    private TMP_Text speechBox;
    private SpeechState state;
    private int speechIndex;
    private Dictionary<SpeechState, string[]> responseDict;
    private bool talking;

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
            { SpeechState.Intro,        NPC.introText }
        };

        // Add additional responses if character has customerData
        if (NPC.customerData != null)
        {
            responseDict.Add(SpeechState.Happy, NPC.customerData.happyText);
            responseDict.Add(SpeechState.Accept, NPC.customerData.acceptText);
            responseDict.Add(SpeechState.Disappoint, NPC.customerData.disappointText);
            responseDict.Add(SpeechState.Reject, NPC.customerData.rejectText);
            responseDict.Add(SpeechState.GoodDeal, NPC.customerData.goodDeal);
            responseDict.Add(SpeechState.NeutralDeal, NPC.customerData.neutralDeal);
            responseDict.Add(SpeechState.BadDeal, NPC.customerData.badDeal);
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

    // Closes speech box; resets speech index; reactivates character button
    private void TurnOffSpeechBox()
    {
        speechBox.text = "...";
        speechIndex = 0;
        talking = false;
        GetComponent<Image>().raycastTarget = true;
        speechCanvas.enabled = false;

        // Customer leaves if state is in Reject
    }

    // Opens speech box. This is called in ReceiveItem().
    private void TurnOnSpeechBox()
    {
        ProgressText();
        talking = true;
        speechCanvas.enabled = true;
        GetComponent<Image>().raycastTarget = false;
    }

    // Detects the player dropping the item on the character
    // Only runs if the character has customer data
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("detected drop event");
        if (NPC.customerData != null && eventData.pointerDrag.CompareTag("item"))
        {
            ReceiveItem(eventData.pointerDrag.GetComponent<CounterItem>().GetItem());
        }
    }
    
    // Determines next state and response depending on which item is received
    public void ReceiveItem(CraftedItem item)
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
    

    public void CalculatePayment()
    {

    }

    
}
