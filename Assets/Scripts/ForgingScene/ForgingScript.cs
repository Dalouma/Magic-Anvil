using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForgingScript : MonoBehaviour
{
    [SerializeField] private List<float> cursorMovementSpeed;

    [SerializeField] private TMP_Text timingText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text hitsText;

    [SerializeField] private Transform cursorTransform;
    [SerializeField] private Animator hammerAnimator;

    [SerializeField] private Transform greatLeft;
    [SerializeField] private Transform greatRight;

    [SerializeField] private Transform goodLeft;
    [SerializeField] private Transform goodRight;

    [SerializeField] private Transform badLeft;
    [SerializeField] private Transform badRight;

    [SerializeField] private Transform EndLeft;
    [SerializeField] private Transform EndRight;

    [SerializeField] private Canvas ResultsCanvas;
    [SerializeField] private Canvas FinishCanvas;
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private Button nextScene;

    public LocalizedStringDatabase table;

   // private AudioSource clangAudio;
    //private GameObject ResultCanvas;
    private int clicks;
    private int totalhits;
    private float finishtimer;
    private int forgingscore;
    private bool gameStart;
    private float currentSpeed;
    private float direction;
    private int speedLevel;
    public static int greatTiming, goodTiming, badTiming;
    private string weapon;
    private string sharpeninglevel = "SharpeningV2";
    private bool onCooldown;
    public static int score;

    private bool activeScene;
    [SerializeField] private float cooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // finishScene.gameObject.SetActive(false);
        totalhits = 5;
        FinishCanvas.GetComponent<Canvas>().enabled = false;
        activeScene = true;
        ResultsCanvas.GetComponent<Canvas>().enabled = false;
        //clangAudio = GetComponent<AudioSource>();
        clicks = 0;
        currentSpeed = 0;
        speedLevel = 0;
        direction = 1f;
        gameStart = false;
        greatTiming = 0;
        goodTiming = 0;
        badTiming = 0;
        StartGame();
        //weapon = Weapon.weapon;
        Debug.Log(weapon);
        onCooldown = false;
        nextScene.onClick.AddListener(EndGame);
        finishtimer = 2f;
        score = 0;
        // Change Music to appropriate track
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ChangeMusic("forge");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheatKeys();

        if (activeScene)
        {
            // Valid Inputs
            bool LMB = Input.GetMouseButtonDown(0);
            bool RMB = Input.GetMouseButtonDown(1);
            // Might use later ^^^^^

            float cursorPos = cursorTransform.position.x;

            cursorTransform.position += Vector3.right * currentSpeed * direction * Time.deltaTime;

            // Change Directions
            if (cursorTransform.position.x > EndRight.position.x && direction > 0)
            {
                direction *= -1;
            }
            if (cursorTransform.position.x < EndLeft.position.x && direction < 0)
            {
                direction *= -1;
            }

            // Check Click Timing
            if (!onCooldown && Input.anyKeyDown && gameStart)
            {
                onCooldown = true;
                StartCoroutine(SlowMo());
                StartCoroutine(Cooldown());
                //clangAudio.Play();
               
                
                hammerAnimator.Play("HammerSwing");
                clicks++;
                SetHitsText();


                // Check Zones
                if (cursorPos >= greatLeft.position.x && cursorPos <= greatRight.position.x)
                {
                    Debug.Log("GREAT TIMING!!");
                    greatTiming++;
                    score += 20;
                    timingText.text = table.GetLocalizedString("greatstrike");
                    // bump speed up
                    if (speedLevel < cursorMovementSpeed.Count - 1)
                    {
                        speedLevel++;
                    }
                }
                else if (cursorPos >= goodLeft.position.x && cursorPos <= goodRight.position.x)
                {
                    Debug.Log("GOOD TIMING!");
                    goodTiming++;
                    score += 10;
                    timingText.text = table.GetLocalizedString("goodstrike");
                    // bump speed down
                    if (speedLevel > 0)
                    {
                        speedLevel--;
                    }
                }
                else if (cursorPos >= badLeft.position.x && cursorPos <= badRight.position.x)
                {
                    Debug.Log("BAD TIMING");
                    badTiming++;
                    score += 5;
                    timingText.text = table.GetLocalizedString("badstrike");
                    // bump speed down
                    if (speedLevel > 0)
                    {
                        speedLevel--;
                    }
                }
                else
                {
                    Debug.Log("FAIL");
                    timingText.text = table.GetLocalizedString("misses");
                    // reset speed
                    speedLevel = 0;
                }
                
                SetScoreText();


                // end check
                if (clicks == totalhits)
                {
                    StartCoroutine(ShowFinishButton());
                    //ShowForgingResults();
                    //EndGame();
                    InventorySystem.instance.AddScore(score * 15);
                }
            }
        }
    }

    void CheatKeys()
    {
        // Press P to skip game
        if (Input.GetKeyDown(KeyCode.P))
        {
            greatTiming = 10;
            goodTiming = 0;
            badTiming = 0;
            StartCoroutine(ShowFinishButton());
        }
    }

    private IEnumerator SlowMo()
    {
        Debug.Log("SlowMo");
        float slowRate = 0.2f;
        currentSpeed *= slowRate;
        yield return new WaitForSeconds(0.3f);
        currentSpeed *= 1 / slowRate;
        currentSpeed = cursorMovementSpeed[speedLevel];
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    public void StartGame()
    {
        gameStart = true;
        currentSpeed = cursorMovementSpeed[speedLevel];
    }

    public void EndGame()
    {
        
        Debug.Log("button clicked for end game");
        gameStart = false;
        currentSpeed = 0;
        if (weapon == "Shield" || weapon == "Hammer")
        {
            AudioManager.instance.ChangeMusic("menu");
            Debug.Log("loading customer scene");
            SceneManager.LoadScene("TestScene");
        }
        if (weapon == "Dagger" || weapon == "Sword" || weapon == "Axe")
        {
            Debug.Log("loading sharpening scene");
            SceneManager.LoadScene(sharpeninglevel);
            //levelChangerScript.FadeToLevel(sharpeninglevel);
        }
    }

    public void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void SetHitsText()
    {
        hitsText.text = clicks.ToString() + "/" + totalhits;
    }

    public void ShowForgingResults()
    {
        //forgingscore = CalculateForgingScore(greatTiming, goodTiming, badTiming); 
        resultText.text = $"{table.GetLocalizedString("forging")}\n\n\n{table.GetLocalizedString("score")} {score} /100";
        ResultsCanvas.GetComponent<Canvas>().enabled = true;
        AnalyticsForging();
    }
    public void AnalyticsForging(){
        CustomEvent ForgingScore = new CustomEvent("ForgingScore")
        {
            { "score", score },
            { "great", greatTiming },
            { "good", goodTiming },
            { "bad", badTiming },
            //{ "weapon", Weapon.weapon} 
        };
        AnalyticsService.Instance.RecordEvent(ForgingScore);
        Debug.Log("ForgingScoreResults" + ForgingScore);
        foreach (KeyValuePair<string, object> kvp in ForgingScore)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        Debug.Log("ForgingScoreResults recorded");
        
    }

    IEnumerator ShowFinishButton(){
        activeScene = false;
        if (AudioManager.instance != null)
        {
                AudioManager.instance.PlaySFX("Victory");
        }
        FinishCanvas.GetComponent<Canvas>().enabled = true;
        yield return new WaitForSeconds(finishtimer);
        FinishCanvas.GetComponent<Canvas>().enabled = false;
        ShowForgingResults();
    }

}
