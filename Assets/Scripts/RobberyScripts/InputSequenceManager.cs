using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class InputSequenceManager : MonoBehaviour
{
    public List<InputType> actionSequence;
    private int currentAction = 0;
    private Vector2 startPosition;

    public Slider healthSlider;
    public Slider timerSlider;

    private int lives = 3;

    public float sequenceDuration = 10f;
    private float remainingTime;

    public List<Enemy> enemies;
    private int currentEnemyIndex = 0;

    public int numEnemies = 5;

    public Sprite tallGoblinSprite;
    public Sprite shortGoblinSprite;
    public Image enemyImage;

    public Vector2 tallGoblinScale = new Vector2(1.5f, 2f);
    public Vector2 shortGoblinScale = new Vector2(1f, 1f);

    public Sprite tapSprite;
    public Sprite swipeUpSprite;
    public Sprite swipeDownSprite;
    public Sprite swipeLeftSprite;
    public Sprite swipeRightSprite;

    public Image actionImageOne;
    public Image actionImageTwo;
    public Image actionImageThree;
    public Image actionImageFour;
    public Image actionImageFive;

    public Image redTintImage;

    private int totalGoldLost = 0;
    private int totalGoldGained = 0;
    private int numGemsGained = 0;

    public TMP_Text goldGained;
    public TMP_Text goldLost;
    public TMP_Text gemsGained;

    public GameObject winScreen;
    public GameObject loseScreen;

    bool endGame = false;

    public GameObject tutorialScreen;

    public bool tutorialActive = true;

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        CreateRandomSequence();
        healthSlider.maxValue = lives;
        healthSlider.value = lives;
        timerSlider.maxValue = sequenceDuration;
        timerSlider.value = sequenceDuration;
        remainingTime = sequenceDuration;
        PopulateEnemies(numEnemies);
    }

    void Awake()
    {
        // Begin playing the robbery music
        AudioManager.instance.ChangeMusic("robbery");
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame) 
        {
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                Time.timeScale = 1f;

                winScreen.SetActive(false);
                loseScreen.SetActive(false);

                // USE LEVEL CHANGER HERE TO MOVE SCENES
                GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().FadeToLevel("ShopScene");
            }
        }

        if (!tutorialScreen.activeSelf) 
        {
            tutorialActive = false;
        }

        if (!tutorialActive)
        {
            UpdateText();
            remainingTime -= Time.deltaTime;
            timerSlider.value = remainingTime;
        }

        if (remainingTime <= 0f)
        {
            Debug.Log("You were attacked!");
            CreateRandomSequence();
            ResetTimer();
            Handheld.Vibrate();
            EnemyAttack();
        }

        if (lives <= 0)
        {
            Debug.Log("Robbery Minigame Failed!");
            LoseGame();
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

    public void WinGame() 
    {
        //GiveGold();
        //GiveGems();

        winScreen.SetActive(true);
        Time.timeScale = 0f;

        endGame = true;
    }

    public void LoseGame()
    {
        //RemoveGold();

        loseScreen.SetActive(true);
        Time.timeScale = 0f;

        endGame = true;
    }

    void UpdateText() 
    {
        goldGained.text = totalGoldGained.ToString();
        goldLost.text = totalGoldLost.ToString();
        gemsGained.text = numGemsGained.ToString();
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

        UpdateEnemySprite();
    }

    void CreateRandomSequence() 
    {
        actionSequence.Clear();
        currentAction = 0;

        for (int i = 0; i < 5; i++)
        {
            actionSequence.Add((InputType)Random.Range(0, System.Enum.GetValues(typeof(InputType)).Length));
        }

        ResetActionImages();
        UpdateSequenceImages();
    }

    void CheckInput(InputType input)
    {
        if (input == actionSequence[currentAction])
        {
            ClearCurrentActionImage();
            currentAction++;
            if (currentAction >= actionSequence.Count)
            {
                Debug.Log("Sequence Completed!");

                AddGold();
                ChanceForGem();

                currentEnemyIndex++;
                if (currentEnemyIndex >= enemies.Count)
                {
                    WinGame();
                }

                CreateRandomSequence();
                ResetTimer();
                StartCoroutine(EnemyDefeated());
            }
        }
        else
        {
            Debug.Log("Sequence Failed!");
            CreateRandomSequence();
            Handheld.Vibrate();
        }
    }

    void AddGold() 
    {
        float randomValue = Random.value;

        int goldAmount = (randomValue < 0.5f) ? 5 : 10;

        totalGoldGained += goldAmount;
    }

    void GiveGold() 
    {
        ShopManager.instance.UpdateMoney(totalGoldGained);
    }

    void RemoveGold() 
    {
        int playerGold = ShopManager.instance.GetMoney();
        int goldAmount = Mathf.CeilToInt(playerGold * 0.2f);

        totalGoldLost += goldAmount;

        if (playerGold - goldAmount < 0) 
        {
            ShopManager.instance.UpdateMoney(playerGold * -1);
        }
        else 
        {
            ShopManager.instance.UpdateMoney(goldAmount * -1);
        }
    }

    void ChanceForGem() 
    {
        float randomValue = Random.value;

        if (randomValue < 0.1f)
        {
            numGemsGained += 1;
        }
    }

    void GiveGems() 
    {
        for (int i = 0; i < numGemsGained; i++) 
        {
            int randomIndex = Random.Range(0, InventorySystem.instance.gemTypes.Count);
            InventorySystem.instance.ChangeGemAmount(InventorySystem.instance.gemTypes[randomIndex], 1);
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
        UpdateSequenceImages();
    }

    void ResetTimer()
    {
        remainingTime = sequenceDuration;
    }

    Sprite GetSpriteForInputType(InputType inputType) 
    {
        switch (inputType) 
        {
            case InputType.Tap:
                return tapSprite;
            case InputType.SwipeRight:
                return swipeRightSprite;
            case InputType.SwipeDown:
                return swipeDownSprite;
            case InputType.SwipeUp:
                return swipeUpSprite;
            case InputType.SwipeLeft:
                return swipeLeftSprite;
            default:
                return null;
        }
    }

    void UpdateSequenceImages() 
    {
        if (actionSequence.Count > 0) actionImageOne.sprite = GetSpriteForInputType(actionSequence[0]);
        if (actionSequence.Count > 1) actionImageTwo.sprite = GetSpriteForInputType(actionSequence[1]);
        if (actionSequence.Count > 2) actionImageThree.sprite = GetSpriteForInputType(actionSequence[2]);
        if (actionSequence.Count > 3) actionImageFour.sprite = GetSpriteForInputType(actionSequence[3]);
        if (actionSequence.Count > 4) actionImageFive.sprite = GetSpriteForInputType(actionSequence[4]);
    }

    void ClearCurrentActionImage()
    {
        switch (currentAction)
        {
            case 0:
                SetImageAlpha(actionImageOne, 0);
                break;
            case 1:
                SetImageAlpha(actionImageTwo, 0);
                break;
            case 2:
                SetImageAlpha(actionImageThree, 0);
                break;
            case 3:
                SetImageAlpha(actionImageFour, 0);
                break;
            case 4:
                SetImageAlpha(actionImageFive, 0);
                break;
        }
    }

    void ResetActionImages()
    {
        SetImageAlpha(actionImageOne, 1);
        SetImageAlpha(actionImageTwo, 1);
        SetImageAlpha(actionImageThree, 1);
        SetImageAlpha(actionImageFour, 1);
        SetImageAlpha(actionImageFive, 1);
    }

    void SetImageAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    void UpdateEnemySprite() 
    {
        Enemy currentEnemy = enemies[currentEnemyIndex];
        if (currentEnemy.type == "tall") 
        {
            enemyImage.sprite = tallGoblinSprite;
            enemyImage.rectTransform.localScale = tallGoblinScale;
        }
        else
        {
            enemyImage.sprite = shortGoblinSprite;
            enemyImage.rectTransform.localScale = shortGoblinScale;
        }
    }

    IEnumerator EnemyDefeated() 
    {
        if (enemyImage == null)
        {
            yield break;
        }

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            Color color = enemyImage.color;
            color.a = i;
            enemyImage.color = color;
            yield return null;
        }

        if (currentEnemyIndex >= enemies.Count)
        {
            Color color = enemyImage.color;
            color.a = 0;
            enemyImage.color = color;
        }
        else
        {
            UpdateEnemySprite();

            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                Color color = enemyImage.color;
                color.a = i;
                enemyImage.color = color;
                yield return null;
            }
        }
    }

    IEnumerator TintScreenRed()
    {
        Color originalColor = redTintImage.color;
        Color targetColor = new Color(1, 0, 0, 0.5f);

        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            redTintImage.color = Color.Lerp(originalColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        redTintImage.color = targetColor;

        yield return new WaitForSeconds(0.2f);

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            redTintImage.color = Color.Lerp(targetColor, originalColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        redTintImage.color = originalColor;
    }

    void EnemyAttack() 
    {
        Enemy enemy = enemies[currentEnemyIndex];
        if (enemy != null) 
        {
            StartCoroutine(TintScreenRed());
            Debug.Log("Attacked by " + enemy.type + " goblin.");
            lives -= enemy.damage;
            healthSlider.value = lives;
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
