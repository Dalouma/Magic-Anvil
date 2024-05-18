using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapCustomer : MonoBehaviour
{
    //public SpriteRenderer spriteRenderer;
    public Button currCustomer;
    public Sprite[] spriteArray;
    public Dictionary<string, Sprite> spriteMap = new Dictionary<string, Sprite>();
    public List<string> spriteNames = new List<string>();
    public List<Sprite> sprites = new List<Sprite>();
    public Animator animator;
    private Animation anim;
    void Start()
    {
        
        anim = GetComponent<Animation>();
        for (int i = 0; i < spriteNames.Count; i++)
        {
            if (!spriteMap.ContainsKey(spriteNames[i]))
            {
                spriteMap.Add(spriteNames[i], sprites[i]);
            }
        }

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
    public void changeSpriteAnim(string customer)
    {
        animator.SetBool("ChangeCustomer", true);
        animator.SetBool("invis", true);
        StartCoroutine(Pause(3.6f, () =>
        {
    
            Image custimage = currCustomer.image;
            custimage.sprite=spriteMap[customer];
            animator.SetBool("ChangeCustomer", false);
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




