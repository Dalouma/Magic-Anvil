using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popupObj;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("PopupShown"))
        {
            popupObj.SetActive(true);

            PlayerPrefs.SetInt("PopupShown", 1);
        }
        else
        {
            popupObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
