using UnityEngine;

public class setActive : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject One;
    public GameObject Two;
    public float time = 60.0f;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0.0f)
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
