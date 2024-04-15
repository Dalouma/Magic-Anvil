using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class SharpeningV2 : MonoBehaviour
{
    // References
    private GameObject meterObj;
    private Transform pointerStartTransform;
    private Transform goodThreshold;
    private Transform greatThreshold;
    private Transform failThreshold;
    private Transform pointerTransform;

    private GameObject itemObj;
    private GameObject grindstoneObj;
    

    [Header("References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject startWindow;
    [SerializeField] private GameObject resultsWindow;

    [Header("Settings")]
    [SerializeField] private float pointerSpeed;
    [SerializeField] private float pointsRate;
    [SerializeField] private float goodReleaseBonus;
    [SerializeField] private float greatReleaseBonus;

    private float score;
    private int scoreMax;
    private float time;
    private bool sharpening;
    private int goodReleases;
    private int greatReleases;

    private bool gameActive;

    void Start()
    {
        // Get References
        meterObj = GameObject.FindGameObjectWithTag("meter");
        pointerStartTransform = meterObj.transform.GetChild(0);
        goodThreshold = meterObj.transform.GetChild(1);
        greatThreshold = meterObj.transform.GetChild(2);
        failThreshold = meterObj.transform.GetChild(3);
        pointerTransform = GameObject.FindGameObjectWithTag("pointer").transform;

        itemObj = GameObject.FindGameObjectWithTag("item");
        grindstoneObj = GameObject.FindGameObjectWithTag("grindstone");
        
        // Set Variables
        score = 0;
        scoreMax= 1000;
        time = 30f;
        goodReleases = 0;
        greatReleases = 0;
        gameActive = false;

        ChangeScoreText();

        // Pull up tutorial window
        startWindow.GetComponent<Canvas>().enabled = true;
    }

    void Update()
    {
        if (gameActive)
        {
            CountDown();
            SharpenItem();
        }
    }

    public void StartGame()
    {
        gameActive = true;
    }

    public void SetSharpening(bool state)
    {
        sharpening = state;
    }

    // Accumulates points and checks for red zone
    public void SharpenItem()
    {
        if (sharpening)
        {
            score += pointsRate * Time.deltaTime;
            ChangeScoreText();

            pointerTransform.position += Vector3.right * Time.deltaTime * pointerSpeed;

            // Red zone check
            if (pointerTransform.position.x > failThreshold.position.x)
            {
                // Reset item pos
                itemObj.GetComponent<TouchAndDrag>().ResetPosition();

                // STUN
            }
        }
        
    }

    // Logic for when the item leaves the grindstone hitbox
    public void ReleaseSharpening()
    {
        // Red zone
        if (pointerTransform.position.x > failThreshold.position.x)
        {
            pointerTransform.position = pointerStartTransform.position;
            return;
        }
            
        // Great release timing
        if (pointerTransform.position.x > greatThreshold.position.x)
        {
            score += greatReleaseBonus;
            greatReleases++;
        }
        // Good release timing
        else if (pointerTransform.position.x > goodThreshold.position.x)
        {
            score += goodReleaseBonus;
            goodReleases++;
        }

        // Reset pointer position
        pointerTransform.position = pointerStartTransform.position;
        // Update score text
        ChangeScoreText();
    }

    // Updates the score text
    public void ChangeScoreText()
    {
        scoreText.text = $"Score:\n{(int)score}/{scoreMax}";
    }

    // Timer countdown
    public void CountDown()
    {
        time -= Time.deltaTime;
        timeText.text = $"Time: {(int)time}";

        if (time < 0)
            EndGame();
    }

    // Ends the game and pulls up the results window
    public void EndGame()
    {
        gameActive = false;

        resultsWindow.GetComponentInChildren<TMP_Text>().text =
            "Final Score\n" +
            $"{(int)score} pts\n\n" +
            $"Good Releases: {goodReleases}\n" +
            $"Great Releases: {greatReleases}";

        resultsWindow.GetComponent<Canvas>().enabled = true;
    }
}
