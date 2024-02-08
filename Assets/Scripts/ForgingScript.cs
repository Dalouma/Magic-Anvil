using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimingScript : MonoBehaviour
{
    [SerializeField] private float cursorMovementSpeed;

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

    // Start is called before the first frame update
    void Start()
    {
        clangAudio = GetComponent<AudioSource>();
        clicks = 0;
        currentSpeed = 0;
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

        cursorTransform.position += Vector3.right * currentSpeed * Time.deltaTime;

        // Change Directions
        if (cursorTransform.position.x > EndRight.position.x || cursorTransform.position.x < EndLeft.position.x)
        {
            currentSpeed *= -1;
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
            }
            else if (cursorPos >= goodLeft.position.x && cursorPos <= goodRight.position.x)
            {
                Debug.Log("GOOD TIMING!");
                timingText.text = "GOOD!!";
            }
            else if (cursorPos >= badLeft.position.x && cursorPos <= badRight.position.x)
            {
                Debug.Log("BAD TIMING");
                timingText.text = "BAD";
            }
            else
            {
                Debug.Log("FAIL");
                timingText.text = "FAIL";
            }

            if (clicks == 5)
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
    }

    public void StartGame()
    {
        gameStart = true;
        currentSpeed = cursorMovementSpeed;
    }

    public void EndGame()
    {
        gameStart = false;
        currentSpeed = 0;

        Debug.Log("loading customer scene");
        SceneManager.LoadScene(0);

    }

}
