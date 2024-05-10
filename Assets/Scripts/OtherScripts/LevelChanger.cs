using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public int Maxdays = 8;
    public int daycount = 1;
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    public Animator animator;

    private string levelToLoad;
    private string prevLevel;

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeToLevel(string levelName)
    {
        prevLevel = levelToLoad;
        levelToLoad = levelName;
        if (prevLevel == "Newspaper Scene" && daycount < Maxdays)
        {
            daycount++;
            levelToLoad = "Testscene";
        }
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
