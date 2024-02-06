using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public abstract class Customer
{
    //[SerializeField] protected TMP_Text _speechText;
    [SerializeField] protected List<string> _rejectList;
    [SerializeField] protected List<(string, float)> _preferenceList;
    [SerializeField] protected string _encounterText, _responseText, _givenItem;
    [SerializeField] protected string _happyText, _baseText, _mediocreText, _rejectText;
    [SerializeField] protected int _payment;

    [SerializeField] protected float _happyMultiplier, _baseMultiplier, _mediocreMultiplier;

    // methods
    public virtual void Init()
    {
        _payment = 0;
        _rejectList = new List<string>();
        _preferenceList = new List<(string, float)>();
        _givenItem = null;
        // MODIFY ACCORDING TO DESIGN
        _happyMultiplier = 1.25f;
        _baseMultiplier = 1.0f;
        _mediocreMultiplier = 0.75f;
    }
    public virtual void GiveItem(string itemName)
    {
        _givenItem = itemName;
    }
    public virtual void CalculatePayment()
    {
        if (_givenItem == null)
        {
            Debug.Log("GIVEN ITEM == NULL");
        }
        else if (_rejectList.Contains(_givenItem)){
            _payment = 0;
            _responseText = _rejectText;
        }
        else
        {
            foreach ((string, float) preference in _preferenceList)
            {
                string itemName = preference.Item1;
                float multiplier = preference.Item2;
                if (_givenItem == itemName)
                {
                    // CALCULATE PAYMENT HERE
                    _payment = (int)(100 * multiplier);

                    if (multiplier == _happyMultiplier)
                    {
                        _responseText = _happyText;
                    }
                    if (multiplier == _baseMultiplier)
                    {
                        _responseText = _baseText;
                    }
                    if (multiplier == _mediocreMultiplier)
                    {
                        _responseText = _mediocreText;
                    }

                    break;
                }
            }
        }
    }
    public virtual void SetSpeechText(string text)
    {
        //_speechText.text = text;
    }
}

public class BasicAdventurer : Customer
{
    public override void Init()
    {
        base.Init();

        // class specifics
        // preferences
        _preferenceList.Add(("shortsword", _happyMultiplier));
        _preferenceList.Add(("dagger", _baseMultiplier));
        _preferenceList.Add(("battlehammer", _baseMultiplier));
        _preferenceList.Add(("greataxe", _baseMultiplier));
        _preferenceList.Add(("shield", _mediocreMultiplier));
        // response text
        _encounterText =
        "Hey there, I’ve recently decided that I wanna explore the world and go on adventures, " +
        "but I’ll need something to defend myself. Do you think you can make something for me?";

        _happyText = "Ah, perfect! I feel like I’ll be able to accomplish a lot with this weapon.";

        _baseText = "Wouldn’t be my first choice, but this’ll do nicely. Thanks.";

        _mediocreText = "I guess this technically helps me defend myself…";
    }
    
}