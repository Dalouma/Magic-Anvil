using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setActive2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject One;
    public GameObject Two;
    public float time = 0.0f;
    public float totalTime;
    [SerializeField] private Image _progressBar;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        _progressBar.fillAmount = time/totalTime;
        if (time >= totalTime)
        {
            timerEnded();
        }

    }
    void timerEnded()
    {
        One.SetActive(false);
        Two.SetActive(true);
    }
}
