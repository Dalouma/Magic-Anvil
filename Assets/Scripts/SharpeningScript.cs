using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SharpeningScript : MonoBehaviour
{
    [SerializeField] private GameObject spacebar;
    [SerializeField] private Transform cursorTransform;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform zoneBoundTop;
    [SerializeField] private Transform zoneBoundBottom;
    [SerializeField] private Animator grindstoneAnimator;
    [SerializeField] private Animator itemAnimator;
    [SerializeField] private ParticleSystem sparks;

    [SerializeField] private List<Transform> keyTransforms;
    [SerializeField] private List<Transform> randomTransforms;

    [SerializeField] private float cursorSpeed;
    [SerializeField] private float maxSharpeningTime;
    [SerializeField] private List<float> zoneMultipliers;
    private string shopLevel = "ResultsScene";
    
    [SerializeField] private TextMeshProUGUI wordCorrectText;
    [SerializeField] private TextMeshProUGUI sharpeningTimerText;


    private bool spinning, sharpening;
    private float spinTime;
    private int currentKeyIndex;
    private float score;

    static public int correctWordInputs;
    private float sharpeningTimer;
    
   // private float sharpeningMaxTime;  
    

    // Start is called before the first frame update
    void Start()
    {
        spinning = false;
        sharpening = false;
        spinTime = 0f;
        currentKeyIndex = 0;
        correctWordInputs = 0;
        sharpeningTimer = 30;
        SetCorrectWordInputsText();
        //sharpeningMaxTime = 20;

        SetRandomKeyOrder();
    }

    // Update is called once per frame
    void Update()
    {
        sharpeningTimer -= Time.deltaTime;
        SetsharpeningTimerText();
        HandleWheelSpin();
        HandleBarTrace();
        
        CheatKeys();
    }

    void CheatKeys()
    {
        // PRESS T TO INCREASE TIME BY 10 SECONDS
        if (Input.GetKeyDown(KeyCode.T))
        {
            sharpeningTimer += 10;
        }
        // PRESS P TO INCREASE SCORE BY 100
        if (Input.GetKeyDown(KeyCode.P))
        {
            score += 100;
            SetCorrectWordInputsText();
        }
    }

    void HandleBarTrace()
    {
        // Get Player Input
        if (spinning)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                cursorTransform.position += Vector3.up * cursorSpeed * Time.deltaTime;
            }
            else if (cursorTransform.position.y > startTransform.position.y)
            {
                cursorTransform.position += Vector3.down * cursorSpeed * Time.deltaTime;
            }
            
        }

        // check in zone
        if (cursorTransform.position.y < zoneBoundTop.position.y && cursorTransform.position.y > zoneBoundBottom.position.y)
        {
            score += zoneMultipliers[0] * Time.deltaTime;
            SetCorrectWordInputsText();
            if (!sharpening)
            {
                sharpening= true;
                itemAnimator.Play("move");
                sparks.Play();
            }
        }
        else if (sharpening)
        {
            sharpening = false;
            itemAnimator.Play("idle");
            sparks.Stop();
        }

        if (!spinning)
        {
            cursorTransform.position = startTransform.position;
        }

    }

    void HandleWheelSpin()
    {
        // Check input
        if (Input.anyKeyDown)
        {
            GameObject currentKey = keyTransforms[currentKeyIndex].gameObject;
            if (Input.GetKeyDown(KeyCode.W))
            {
                CheckKey(currentKey, "W");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                CheckKey(currentKey, "A");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                CheckKey(currentKey, "S");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                CheckKey(currentKey, "D");
            }

            // Check finish correct input
            if (currentKeyIndex >= keyTransforms.Count)
            {
                currentKeyIndex = 0;
                spinning = true;
                spinTime = maxSharpeningTime;
                grindstoneAnimator.Play("spin");
                spacebar.SetActive(true);
                setKeyVisibility(false);
            }
        }

        // Handle Spin Time
        if (spinning)
        {
            spinTime -= Time.deltaTime;

            if (spinTime < 0f)
            {
                spinning = false;
                grindstoneAnimator.Play("idle");
                spacebar.SetActive(false);
                SetRandomKeyOrder();
            }
        }
    }
    
    void CheckKey(GameObject currentKey, string correctKey){
        if (currentKey.name == correctKey)
        {
            //currentKey.SetActive(false);
            currentKey.transform.position += Vector3.down * 0.5f;
            currentKeyIndex++;
        }
        else
        {
            Debug.Log("FAIL!!!");
            SetRandomKeyOrder();
            currentKeyIndex = 0;
        }
    }

    void setKeyVisibility(bool keystate){
        for (int i = 0; i < keyTransforms.Count; i++)
        {
            keyTransforms[i].position = randomTransforms[i].position;
            keyTransforms[i].gameObject.SetActive(keystate);
        }
    }

    void SetRandomKeyOrder()
    {
        Shuffle(keyTransforms);
        setKeyVisibility(true);
    }

    void Shuffle<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }

    void SetCorrectWordInputsText(){
        //wordCorrectText.text = "Hits:  " + correctWordInputs;
        wordCorrectText.text = "Score: " + (int)score;
    }

    void SetsharpeningTimerText(){
       sharpeningTimerText.text = "Timer:  " + (int)sharpeningTimer;
       if((int)sharpeningTimer <= 0){
            GoToShop();
       }
    }

    public void GoToShop()
    {
        Debug.Log("loading forging scene");
        SceneManager.LoadScene(shopLevel);
    }

}
