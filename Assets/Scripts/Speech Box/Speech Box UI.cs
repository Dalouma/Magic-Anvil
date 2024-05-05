using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBoxUI : MonoBehaviour
{
    [Header("Refernces")]
    public TMP_Text speechBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProgressText()
    {
        // Replace with whatever
        speechBox.text = "I just changed the text. Woooooo";
    }
}
