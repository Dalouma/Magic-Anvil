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

    [Header("Settings")]
    [SerializeField] private float pointerSpeed;
    [SerializeField] private float pointsRate;
    [SerializeField] private float goodReleaseBonus;
    [SerializeField] private float greatReleaseBonus;

    private float score;
    private int scoreMax;
    private float time;
    public bool sharpening;

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

        ChangeScoreText();
    }

    void Update()
    {
        CountDown();
        SharpenItem();
    }

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
            score += greatReleaseBonus;
        // Good release timing
        else if (pointerTransform.position.x > goodThreshold.position.x)
            score += goodReleaseBonus;

        // Reset pointer position
        pointerTransform.position = pointerStartTransform.position;
        // Update score text
        ChangeScoreText();
    }

    public void ChangeScoreText()
    {
        scoreText.text = $"Score:\n{(int)score}/{scoreMax}";
    }

    // Timer countdown
    public void CountDown()
    {
        time -= Time.deltaTime;
        timeText.text = $"Time: {(int)time}";
    }
}
