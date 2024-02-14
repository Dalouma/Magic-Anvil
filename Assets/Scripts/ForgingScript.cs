using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimingScript : MonoBehaviour
{
    [SerializeField] private List<float> cursorMovementSpeed;

    [SerializeField] private TMP_Text timingText;
    [SerializeField] private Transform cursorTransform;

    [SerializeField] private Transform greatLeft;
    [SerializeField] private Transform greatRight;

    [SerializeField] private Transform goodLeft;
    [SerializeField] private Transform goodRight;

    [SerializeField] private Transform badLeft;
    [SerializeField] private Transform badRight;

    [SerializeField] private Transform EndLeft;
    [SerializeField] private Transform EndRight;

    private AudioSource clangAudio;

    private int clicks;
    private bool gameStart;
    private float currentSpeed;
    private float direction;
    private int speedLevel;

    // Start is called before the first frame update
    void Start()
    {
        clangAudio = GetComponent<AudioSource>();
        clicks = 0;
        currentSpeed = 0;
        speedLevel= 0;
        direction = 1f;
        gameStart = false;

        StartGame();
    }

    // Update is called once per frame
    void Update()
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
        if (Input.anyKeyDown && gameStart)
        {
            StartCoroutine(SlowMo());
            clangAudio.Play();
            clicks++;
            // Check Zones
            if (cursorPos >= greatLeft.position.x && cursorPos <= greatRight.position.x)
            {
                Debug.Log("GREAT TIMING!!");
                timingText.text = "GREAT!!";
                // bump speed up
                if (speedLevel < cursorMovementSpeed.Count - 1)
                {
                    speedLevel++;
                }
            }
            else if (cursorPos >= goodLeft.position.x && cursorPos <= goodRight.position.x)
            {
                Debug.Log("GOOD TIMING!");
                timingText.text = "GOOD!!";
                // bump speed down
                if (speedLevel > 0)
                {
                    speedLevel--;
                }
            }
            else if (cursorPos >= badLeft.position.x && cursorPos <= badRight.position.x)
            {
                Debug.Log("BAD TIMING");
                timingText.text = "BAD";
                // bump speed down
                if (speedLevel > 0)
                {
                    speedLevel--;
                }
            }
            else
            {
                Debug.Log("FAIL");
                timingText.text = "FAIL";
                // reset speed
                speedLevel= 0;
            }
            

            // end check
            if (clicks == 10)
            {
                EndGame();
            }
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

    public void StartGame()
    {
        gameStart = true;
        currentSpeed = cursorMovementSpeed[speedLevel];
    }

    public void EndGame()
    {
        gameStart = false;
        currentSpeed = 0;

        Debug.Log("loading customer scene");
        SceneManager.LoadScene(0);

    }

}
