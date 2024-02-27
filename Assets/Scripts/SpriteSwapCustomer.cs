using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapCustomer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public static int customer = 1;
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
       spriteRenderer.sprite=spriteArray[customer];
    }
    public void changeSprite()
    {
        customer++;
        spriteRenderer.sprite = spriteArray[customer];
    }



   
}
