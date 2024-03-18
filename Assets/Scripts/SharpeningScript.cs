using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
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
    [SerializeField] private Transform greenThreshold;
    [SerializeField] private Transform yellowThreshold;
    [SerializeField] private Animator grindstoneAnimator;
    [SerializeField] private Animator itemAnimator;
    [SerializeField] private AudioSource grindstoneSFX;
    [SerializeField] private AudioSource sharpeningSFX;
    [SerializeField] private SpriteRenderer itemSprite;
    [SerializeField] private ParticleSystem sparks;

    [SerializeField] private List<Transform> keyTransforms;
    [SerializeField] private List<Transform> randomTransforms;
    [SerializeField] private Transform arrowIndicatorTransform;

    [SerializeField] private float cursorSpeed;
    [SerializeField] private float maxSharpeningTime;
    [SerializeField] private List<float> zoneMultipliers;

    [SerializeField] private Sprite[] spriteArray;
    //private string shopLevel = "ResultsScene";
    
    [SerializeField] private TextMeshProUGUI wordCorrectText;
    [SerializeField] private TextMeshProUGUI sharpeningTimerText;
    [SerializeField] private TextMeshProUGUI spinTimeText;
    [SerializeField] private Animator wKey;


    private bool spinning, sharpening;
    private float spinTime;
    private int currentKeyIndex;
    public static float score;

    static public int correctWordInputs;
    [SerializeField] private float sharpeningTimer;
    
   // private float sharpeningMaxTime;  
    

    // Start is called before the first frame update
    void Start()
    {
        spinning = false;
        sharpening = false;
        spinTime = 0f;
        currentKeyIndex = 0;
        correctWordInputs = 0;
        SetCorrectWordInputsText();
        //sharpeningMaxTime = 20;

        SetRandomKeyOrder();
        SetSprite(Weapon.weapon);
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
        // PRESS P TO SKIP GAME
        if (Input.GetKeyDown(KeyCode.P))
        {
            score = 300;
            SetCorrectWordInputsText();
            sharpeningTimer = 0f;
        }
    }

    void SetSprite(string weaponName)
    {
        switch (weaponName)
        {
            case "Sword":
                itemSprite.sprite = spriteArray[0];
                break;
            case "Dagger":
                itemSprite.sprite = spriteArray[1];
                break;
            case "Axe":
                itemSprite.sprite = spriteArray[2];
                break;
            default:
                itemSprite.sprite = spriteArray[0];
                break;
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
            
            // Reset hold animation
            if (!spacebar.activeSelf && cursorTransform.position.y <= startTransform.position.y)
            {
                spacebar.SetActive(true);
            }
        }

        // check in zone
        if (cursorTransform.position.y < zoneBoundTop.position.y && cursorTransform.position.y > zoneBoundBottom.position.y)
        {
            // Green Zone
            if (cursorTransform.position.y > greenThreshold.position.y)
            {
                score += zoneMultipliers[1] * Time.deltaTime;
            }
            // Yellow Zone
            else if (cursorTransform.position.y > yellowThreshold.position.y)
            {
                score += zoneMultipliers[0] * Time.deltaTime;
            }
            
            SetCorrectWordInputsText();

            if (spacebar.activeSelf)
            {
                spacebar.SetActive(false);
            }

            if (!sharpening)
            {
                sharpening= true;
                itemAnimator.Play("move");
                sharpeningSFX.Play();
                sparks.Play();
            }
        }
        else if (sharpening)
        {
            sharpening = false;
            itemAnimator.Play("idle");
            sharpeningSFX.Stop();
            sparks.Stop();
        }

        // check RED ZONE
        if (cursorTransform.position.y > zoneBoundTop.position.y)
        {
            spinTime = 0.0f;
            spinTimeText.text = ((int)spinTime).ToString();
            GetComponent<Animator>().Play("Shake");

        }

        if (!spinning)
        {
            cursorTransform.position = startTransform.position;
        }

    }

    void HandleWheelSpin()
    {
        // Check input
        if (!spinning && Input.anyKeyDown)
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
                arrowIndicatorTransform.position = currentKey.transform.position + Vector3.up * 0.5f;
            }
        }

        // Handle Spin Time
        if (spinning)
        {
            spinTime -= Time.deltaTime;
            spinTimeText.text = ((int)spinTime).ToString();

            if (!grindstoneSFX.isPlaying)
            {
                grindstoneSFX.Play();
            }

            if (spinTime < 0f)
            {
                spinning = false;
                grindstoneAnimator.Play("idle");
                grindstoneSFX.Stop();
                spacebar.SetActive(false);
                SetRandomKeyOrder();
            }
        }
    }
    
    void CheckKey(GameObject currentKey, string correctKey){
        if (currentKey.name == correctKey)
        {
            //currentKey.transform.position += Vector3.down * 0.05f;
            // CHANGE SPRITE TO DOWN POSITION
            currentKey.GetComponent<Animator>().Play("down");

            currentKeyIndex++;

            // Change arrow indicator position to above next key to be pressed
            if (currentKeyIndex < keyTransforms.Count)
            {
                GameObject nextKey = keyTransforms[currentKeyIndex].gameObject;
                arrowIndicatorTransform.position = nextKey.transform.position + Vector3.up * 1.2f;
            }
            
        }
        else
        {
            Debug.Log("FAIL!!!");
            currentKeyIndex = 0;
            SetRandomKeyOrder();
        }
    }

    void setKeyVisibility(bool keystate){
        for (int i = 0; i < keyTransforms.Count; i++)
        {
            keyTransforms[i].position = randomTransforms[i].position;
            keyTransforms[i].gameObject.SetActive(keystate);

            // RESET SPRITE TO UP STATE
            keyTransforms[i].gameObject.GetComponent<Animator>().Play("up");
        }
        arrowIndicatorTransform.gameObject.SetActive(keystate);
    }

    void SetRandomKeyOrder()
    {
        Shuffle(keyTransforms);
        setKeyVisibility(true);

        // Reset arrow indicator position to first key
        arrowIndicatorTransform.position = keyTransforms[currentKeyIndex].gameObject.transform.position + Vector3.up * 1.2f;
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
        wordCorrectText.text = ((int)score).ToString();
    }

    void SetsharpeningTimerText(){
       sharpeningTimerText.text = ((int)sharpeningTimer).ToString();
       if((int)sharpeningTimer <= 0){
            GoToShop();
       }
    }

    public void GoToShop()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ChangeMusic("menu");
        }
        
        Debug.Log("loading forging scene");
        SceneManager.LoadScene("TestScene");
    }

}
