using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForgingV2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text resultsText;
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] Canvas resultsCanvas;
    [SerializeField] private Transform leftEnd;
    [SerializeField] private Transform rightEnd;

    // Pointer Data
    private Transform pointerTransform;
    private float pointerSpeed;
    private int direction;
    public static bool inGreen, inYellow, inOrange;

    // Game Data
    private bool gameStart;
    private int score;
    private int strikes, maxStrikes;
    public static int good, great, perfect;

    private void Awake()
    {
        pointerTransform = GameObject.FindGameObjectWithTag("pointer").transform;
        inGreen = inYellow = inOrange = false;
        good = great = perfect = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        direction = 1;
        pointerSpeed = 5f;

        score = 0;
        strikes= 0;
        maxStrikes = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            PointerMovement();
            HandleInput();
        }
    }

    // Starts the game (called by button)
    public void OnBeginGame() { gameStart = true; }

    // Gives the player points depending on which zone they are in when touch input is detected
    void HandleInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            strikes++;
            if (inGreen)
            {
                score += 300;
                perfect++;
            } 
            else if (inYellow)
            {
                score += 200;
                great++;
            }
            else if (inOrange)
            {
                score += 100;
                good++;
            }

            scoreText.text = $"Score: {score}";
            if (strikes == maxStrikes)
            {
                gameStart = false;
                OnShowResults();
                InventorySystem.instance.AddScore(score);
            }
                
        }

    }

    // This function shows the result screen
    void OnShowResults()
    {
        resultsText.text =
            $"Perfect Strikes:\t{perfect}\n" +
            $"Great Strikes:\t{great}\n" +
            $"Good Strikes:\t{good}";
        finalScoreText.text = $"Score: {score}";
        resultsCanvas.enabled = true;
    }

    // This function controls the movement of the pointers
    void PointerMovement()
    {
        Vector2 target = MovementTarget();

        pointerTransform.position += Vector3.right * direction * pointerSpeed * Time.deltaTime;

        // switches directions
        float distance = (target - (Vector2)pointerTransform.position).magnitude;
        if (distance < 0.1f)
            direction *= -1;
    }

    // This returns the current movement target of the pointer depending on the current direction
    Vector2 MovementTarget()
    {
        if (direction == 1)
            return rightEnd.position;
        else return leftEnd.position;
    }
}
