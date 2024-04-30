using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    public Animator animator;

    private string levelToLoad;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(string levelName) 
    {
        levelToLoad = levelName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() 
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
