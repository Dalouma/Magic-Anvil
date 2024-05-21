using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputSequenceManager : MonoBehaviour
{
    public List<InputType> actionSequence;
    private int currentAction = 0;
    private Vector2 startPosition;

    public Slider healthSlider;
    public Slider timerSlider;

    public TMP_Text sequenceText;
    public TMP_Text lifeText;
    private int lives = 3;

    public TMP_Text timerText;
    public float sequenceDuration = 10f;
    private float remainingTime;

    public List<Enemy> enemies;
    private int currentEnemyIndex = 0;

    public int numEnemies = 5;


    // Start is called before the first frame update
    void Start()
    {
        CreateRandomSequence();
        healthSlider.maxValue = lives;
        healthSlider.value = lives;
        timerSlider.maxValue = sequenceDuration;
        timerSlider.value = sequenceDuration;
        lifeText.text = "Lives: 3";
        remainingTime = sequenceDuration;
        PopulateEnemies(numEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        UpdateTimerText();
        timerSlider.value = remainingTime;

        if (remainingTime <= 0f) 
        {
            Debug.Log("You were attacked!");
            UpdateLifeText();
            CreateRandomSequence();
            ResetTimer();
            Handheld.Vibrate();
            EnemyAttack();
        }

        if (lives <= 0) 
        {
            Debug.Log("Robbery Minigame Failed!");
        }

        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) 
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    Vector2 endPosition = touch.position;
                    CheckInput(GetSwipeDirection(startPosition, endPosition));
                    break;
            }
        }
    }

    void PopulateEnemies(int numEnemies) 
    {
        enemies.Clear();

        for (int i = 0; i < numEnemies; i++)
        {
            Enemy enemy = new Enemy();
            enemy.type = Random.Range(0, 2) == 0 ? "small" : "tall";
            enemy.damage = enemy.type == "small" ? 1 : 2;
            enemies.Add(enemy);
        }
    }

    void CreateRandomSequence() 
    {
        actionSequence.Clear();
        currentAction = 0;

        for (int i = 0; i < 5; i++) 
        {
            actionSequence.Add((InputType)Random.Range(0, System.Enum.GetValues(typeof(InputType)).Length));
        }

        UpdateSequenceText();
    }

    void CheckInput(InputType input) 
    {
        if (input == actionSequence[currentAction]) 
        {
            currentAction++;
            if (currentAction >= actionSequence.Count) 
            {
                Debug.Log("Sequence Completed!");
                CreateRandomSequence();
                ResetTimer();
            }
        }
        else 
        {
            Debug.Log("Sequence Failed!");
            ResetSequence();
            Handheld.Vibrate();
        }
    }

    InputType GetSwipeDirection(Vector2 start, Vector2 end) 
    {
        float swipeMagnitude = (end - start).magnitude;
        if (swipeMagnitude < 20f) 
        {
            return InputType.Tap;
        }

        Vector2 swipeDirection = end - start;
        float angle = Vector2.SignedAngle(swipeDirection, Vector2.right);

        if (angle > -45f && angle <= 45f) 
        {
            return InputType.SwipeRight;
        }
        else if (angle > 45f && angle <= 135f) 
        {
            return InputType.SwipeDown;
        }
        else if (angle > -135f && angle <= -45f) 
        {
            return InputType.SwipeUp;
        }
        else 
        {
            return InputType.SwipeLeft;
        }
    }

    void ResetSequence() 
    {
        Debug.Log("Sequence Reset!");
        currentAction = 0;
        UpdateSequenceText();
    }

    void ResetTimer() 
    {
        remainingTime = sequenceDuration;
    }

    void UpdateSequenceText() 
    {
        string sequenceString = "Sequence: ";
        foreach (var action in actionSequence) 
        {
            sequenceString += action.ToString() + " ";
        }
        sequenceText.text = sequenceString;
    }

    void UpdateLifeText()
    {
        string lifeString = "Lives: ";
        lifeString += lives.ToString();
        lifeText.text = lifeString;
    }

    void UpdateTimerText() 
    {
        timerText.text = "Time: " + Mathf.CeilToInt(remainingTime).ToString();
    }

    void EnemyAttack() 
    {
        Enemy enemy = enemies[currentEnemyIndex];
        if (enemy != null) 
        {
            Debug.Log("Attacked by " + enemy.type + " goblin.");
            lives -= enemy.damage;
            healthSlider.value = lives;
            UpdateLifeText();
            currentEnemyIndex++;
            if (currentEnemyIndex >= enemies.Count) 
            {
                currentEnemyIndex= 0;
            }
        }
    }
}

[System.Serializable]
public class Enemy 
{
    public string type;
    public int damage;
}

public enum InputType 
{
    Tap,
    SwipeUp,
    SwipeDown,
    SwipeLeft,
    SwipeRight
}
