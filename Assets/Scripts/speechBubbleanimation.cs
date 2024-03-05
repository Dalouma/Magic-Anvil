using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class speechBubbleanimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public void speechFade()
    {
        animator.SetBool("invis", true);
        StartCoroutine(Pause(4.0f, () =>
        {
            animator.SetBool("invis", false);
        }));
    }
    
    
    IEnumerator Pause(float seconds, Action callback)
    {
        float elapsedtime = 0f;
        while (elapsedtime < seconds)
        {
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        callback?.Invoke();
    }
}

