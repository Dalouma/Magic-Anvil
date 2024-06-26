using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

// THIS CODE WAS COPIED AND MODIFIED FROM
// https://github.com/Maraakis/ChristinaCreatesGames/tree/mains
// THE LICENSE IS STORED UNDER THE ASSETS FOLDER

public class TypewriterEffect : MonoBehaviour
{
    private TMP_Text _textBox;

    // Basic Typewriter Functionality
    private int _iCharVisible;
    private Coroutine _typewriterCoroutine;
    private bool _readyForNewText = true;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _punctuationDelay;

    [Header("Typewriter Settings")]
    [SerializeField] private float charsPerSecond = 20f;
    [SerializeField] private float punctuationDelay = 0.5f;
    [SerializeField] private char[] punctuations;

    // Skipping Functionality 
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip Options")]
    [SerializeField] private bool quickSkip;
    [SerializeField]
    [Min(1)] private int skipSpeedup = 5;

    // Event Functionality
    private WaitForSeconds _textboxFullEventDelay;
    [Header("Event Options")]
    [SerializeField]
    [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;

    // Character reference
    private Customer customer;

    private void Awake()
    {
        _textBox = GameObject.FindGameObjectWithTag("SpeechBox").GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1 / charsPerSecond);
        _punctuationDelay = new WaitForSeconds(punctuationDelay);

        _skipDelay = new WaitForSeconds(1 / (charsPerSecond * skipSpeedup));
        _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);

        // Grab customer reference
        customer = GameObject.FindGameObjectWithTag("customer").GetComponent<Customer>();
    }

    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText);
    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareForNewText);
    }

    private void Update()
    {
        if (customer.isTalking() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (_readyForNewText)
            {
                _textBox.maxVisibleCharacters = 0;
                _iCharVisible = 0;
                CurrentlySkipping = false;
                GameObject.FindGameObjectWithTag("customer").GetComponent<Customer>().ProgressText();
                return;
            }
            if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
            {
                Skip();
            }
        }
    }

    public void PrepareForNewText(UnityEngine.Object obj)
    {
        if (obj != _textBox || !_readyForNewText || CurrentlySkipping)
        {
            return;
        }

        CurrentlySkipping = false;
        _readyForNewText = false;

        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _textBox.maxVisibleCharacters = 0;
        _iCharVisible = 0;

        _typewriterCoroutine = StartCoroutine(routine: Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo;

        while (_iCharVisible < textInfo.characterCount + 1)
        {
            var lastCharIndex = textInfo.characterCount - 1;

            if (_iCharVisible >= lastCharIndex)
            {
                _textBox.maxVisibleCharacters++;
                yield return _textboxFullEventDelay;
                CompleteTextRevealed?.Invoke();
                _readyForNewText = true;
                yield break;
            }

            char character = textInfo.characterInfo[_iCharVisible].character;

            _textBox.maxVisibleCharacters++;

            if (!CurrentlySkipping && punctuations.Contains(character))
            {
                yield return _punctuationDelay;
            }
            else
            {
                yield return CurrentlySkipping ? _skipDelay : _simpleDelay;
            }

            CharacterRevealed?.Invoke(character);
            _iCharVisible++;
        }
    }

    void Skip()
    {
        if (CurrentlySkipping)
        {
            return;
        }

        CurrentlySkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(routine: SkipSpeedupReset());
            return;
        }

        StopCoroutine(_typewriterCoroutine);
        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
        _readyForNewText = true;
        CompleteTextRevealed?.Invoke();
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters == _textBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }

    public bool IsReadyToChange()
    {
        return _readyForNewText;
    }
}
