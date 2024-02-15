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
    [SerializeField] private Image _progressBar;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        _progressBar.fillAmount = time/30.0f;
        if (time >= 30.0f)
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
