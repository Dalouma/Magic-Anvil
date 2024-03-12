using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpriteSwapCustomer : MonoBehaviour
{
    //public SpriteRenderer spriteRenderer;
    public Button currCustomer;
    public Sprite[] spriteArray;
    public Animator animator;
    private Animation anim;
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite=spriteArray[customer];
        anim = GetComponent<Animation>();

    }
    public void changeSprite(int customer)
    {
             if (customer >= spriteArray.Length)
             {
                 customer = 0;
             }

             Image custimage = currCustomer.image;
             custimage.sprite = spriteArray[customer];

    }
    public void changeSpriteAnim(int customer)
    {
        animator.SetBool("ChangeCustomer", true);
        animator.SetBool("invis", true);
        StartCoroutine(Pause(4.0f, () =>
        {
            if (customer >= spriteArray.Length)
            {
                customer = 0;
            }

            Image custimage = currCustomer.image;
            custimage.sprite = spriteArray[customer];
            animator.SetBool("ChangeCustomer", false);
            animator.SetBool("invis", false);
        }));




    }

    IEnumerator Pause(float seconds, Action callback)
    {
        float elapsedtime = 0f;
        while(elapsedtime<seconds)
        {
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        callback?.Invoke();
    }
}



   
