using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button saveButton = GetComponent<Button>();

        saveButton.onClick.AddListener(SaveButtonClick);
    }

    private void SaveButtonClick()
    {
        GameManager.Instance.SaveGame();
    }
}
