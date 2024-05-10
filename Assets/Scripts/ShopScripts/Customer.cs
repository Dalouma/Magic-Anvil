using System.Collections.Generic;
using UnityEngine;

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
        else if (_rejectList.Contains(_givenItem))
        {
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
        "\"Hey there, I’ve recently decided that I wanna explore the world and go on adventures, " +
        "but I’ll need something to defend myself. Do you think you can make something for me?\"";

        _happyText = "\"Ah, perfect! I feel like I’ll be able to accomplish a lot with this weapon.\"";

        _baseText = "\"Wouldn’t be my first choice, but this’ll do nicely. Thanks.\"";

        _mediocreText = "\"I guess this technically helps me defend myself…\"";
    }

}

public class Bandit : Customer
{
    public override void Init()
    {
        base.Init();

        // class specifics
        // preferences
        _preferenceList.Add(("dagger", _happyMultiplier));

        _preferenceList.Add(("shortsword", _baseMultiplier));
        _preferenceList.Add(("battlehammer", _baseMultiplier));
        _preferenceList.Add(("greataxe", _baseMultiplier));

        _rejectList.Add("shield");


        // response text
        _encounterText =
        "\"Hey, uh… I need a weapon. " +
        "I’m not very strong so it needs to be something I can use very easily and it would be better if you didn’t ask any questions…\"";

        _happyText = "\"Oh yes… hehehe… this’ll do.\" They hand you a hefty sum before leaving*";

        _baseText = "\"Not what I had in mind, but maybe this might work…\"";

        _mediocreText = "\"Not what I had in mind, but maybe this might work…\"";

        _rejectText = "\"Are you kidding me? This won’t help me ki– I mean… Ugh!\"";
    }

}

public class Paladin : Customer
{
    public override void Init()
    {
        base.Init();

        // class specifics
        // preferences
        _preferenceList.Add(("shield", _happyMultiplier));
        _preferenceList.Add(("battlehammer", _happyMultiplier));

        _preferenceList.Add(("shortsword", _baseMultiplier));

        _preferenceList.Add(("greataxe", _mediocreMultiplier));

        _rejectList.Add("dagger");


        // response text
        _encounterText =
        "\"Greetings, good smith. I aim to be sworn before the goddess of protection at the holy temple, " +
        "but I need a piece of equipment to bind my oath. Can you make me something to protect the people?\"";

        _happyText = "\"This is perfect! The goddess will be pleased.";

        _baseText = "\"I’m not too confident with what you presented me, but I’ll pay you anyway\"";

        _mediocreText = "\"I’m not too confident with what you presented me, but I’ll pay you anyway\"";

        _rejectText = "\"What mockery is this? You’ve presented me with a coward’s weapon. The holy temple will hear of this.\"";
    }

}