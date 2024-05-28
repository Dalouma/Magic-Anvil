using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenShot : MonoBehaviour
{
    public static ScreenShot instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenCapture.CaptureScreenshot("Assets/Screenshots/" + SceneManager.GetActiveScene().name + ".png");
        } 
    }
}
