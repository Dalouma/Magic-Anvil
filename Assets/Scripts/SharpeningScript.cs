using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpeningScript : MonoBehaviour
{
    [SerializeField] private Transform cursorTransform;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform zoneBoundTop;
    [SerializeField] private Transform zoneBoundBottom;

    [SerializeField] private List<Transform> keyTransforms;
    [SerializeField] private List<Transform> randomTransforms;

    [SerializeField] private float arrowSpeed;
    [SerializeField] private float maxSharpeningTime;

    private bool spinning;
    private float spinTime;

    private int currentKeyIndex; 

    // Start is called before the first frame update
    void Start()
    {
        spinning = false;
        spinTime = 0f;

        currentKeyIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HandleWheelSpin();
        HandleSharpening();
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetRandomKeyOrder();
            currentKeyIndex = 0;
        }
    }

    void HandleWheelSpin()
    {
        // Get Player Input
        if (Input.GetKey(KeyCode.Space))
        {
            cursorTransform.position += Vector3.up * arrowSpeed * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // check in zone
            if (cursorTransform.position.y < zoneBoundTop.position.y && cursorTransform.position.y > zoneBoundBottom.position.y)
            {
                spinning = true;
                spinTime = maxSharpeningTime;
                SetRandomKeyOrder();
            }
            cursorTransform.position = startTransform.position;
        }
        //Debug.Log(spinning);

        // Handle Spin Time
        if (spinning && spinTime > 0f)
        {
            spinTime -= Time.deltaTime;
        }
        else
        {
            spinning = false;
        }
    }

    void HandleSharpening()
    {
        if (spinning && Input.anyKeyDown)
        {
            // Check input
            GameObject currentKey = keyTransforms[currentKeyIndex].gameObject;
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (currentKey.name == "W")
                {
                    currentKey.SetActive(false);
                    currentKeyIndex++;
                }
                else
                {
                    Debug.Log("FAIL!!!");
                    SetRandomKeyOrder();
                    currentKeyIndex = 0;
                }
                
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentKey.name == "A")
                {
                    currentKey.SetActive(false);
                    currentKeyIndex++;
                }
                else
                {
                    Debug.Log("FAIL!!!");
                    SetRandomKeyOrder();
                    currentKeyIndex = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (currentKey.name == "S")
                {
                    currentKey.SetActive(false);
                    currentKeyIndex++;
                }
                else
                {
                    Debug.Log("FAIL!!!");
                    SetRandomKeyOrder();
                    currentKeyIndex = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentKey.name == "D")
                {
                    currentKey.SetActive(false);
                    currentKeyIndex++;
                }
                else
                {
                    Debug.Log("FAIL!!!");
                    SetRandomKeyOrder();
                    currentKeyIndex = 0;
                }
            }

            // Check finish
            if (currentKeyIndex >= keyTransforms.Count)
            {
                Debug.Log("SUCCESS!!!");
                SetRandomKeyOrder();
                currentKeyIndex = 0;
            }
        }
    }

    void SetRandomKeyOrder()
    {
        Shuffle(keyTransforms);
        for (int i = 0; i < keyTransforms.Count; i++)
        {
            keyTransforms[i].position = randomTransforms[i].position;
            keyTransforms[i].gameObject.SetActive(true);
        }

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

}
