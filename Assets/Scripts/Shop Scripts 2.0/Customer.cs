using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This is a wrapper class for the CharacterData for each customer
public class Customer : MonoBehaviour
{
    [SerializeField] private CharacterData customerData;

    [Header("References")]
    [SerializeField] private Canvas speechCanvas;

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

    // Other Variables
    private TMP_Text nameBox;

    private void Start()
    {
        // Grab References
        speechBox = GameObject.FindGameObjectWithTag("SpeechBox").GetComponent<TMP_Text>();
        nameBox = GameObject.FindGameObjectWithTag("NameBox").GetComponent<TMP_Text>();

        // Setup specific customer
        nameBox.text = customerData.name;
        GetComponent<Image>().sprite = customerData.characterSprite;
        SetupResponses();

        // Setup Vars
        state = SpeechState.Intro;
        speechIndex = 0;
        talking = false;
    }

    // Maps enum states to string arrays according to customer data
    private void SetupResponses()
    {
        responseDict = new Dictionary<SpeechState, string[]>()
        {
            { SpeechState.Intro,        customerData.introText },
            { SpeechState.Happy,        customerData.happyText },
            { SpeechState.Accept,       customerData.acceptText },
            { SpeechState.Disappoint,   customerData.disappointText },
            { SpeechState.Reject,       customerData.rejectText },
            { SpeechState.GoodDeal,     customerData.goodDeal },
            { SpeechState.NeutralDeal,  customerData.neutralDeal },
            { SpeechState.BadDeal,      customerData.badDeal },
        };
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
        talking = false;
        speechCanvas.enabled = false;
        GetComponent<Image>().raycastTarget = true;
        speechIndex = 0;
    }

    public bool isTalking() { return talking; }
    public void SetTalking(bool state) { talking = state; }

    public void CalculatePayment()
    {

    }

}
