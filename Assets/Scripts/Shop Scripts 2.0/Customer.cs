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

    private void Start()
    {
        // Grab References
        speechBox = GameObject.FindGameObjectWithTag("SpeechBox").GetComponent<TMP_Text>();

        // Setup specific customer
        GetComponent<Image>().sprite = customerData.characterSprite;
        SetupResponses();

        // Setup Vars
        state = SpeechState.Intro;
        speechIndex = 0;
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

    public void ProgressText()
    {
        var currentResponseArray = responseDict[state];
        if (speechIndex < currentResponseArray.Length)
        {
            speechBox.text = currentResponseArray[speechIndex];
            speechIndex++;
        }
        else
            TurnOffSpeechBox();
    }

    private void TurnOffSpeechBox()
    {
        speechCanvas.enabled = false;
        GetComponent<Image>().raycastTarget = true;
        speechIndex = 0;
    }

    public void CalculatePayment()
    {

    }

}
