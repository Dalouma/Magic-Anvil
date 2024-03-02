using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapCustomer : MonoBehaviour
{
    //public SpriteRenderer spriteRenderer;
    public Button currCustomer;
    public Sprite[] spriteArray;
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
       //spriteRenderer.sprite=spriteArray[customer];
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



   
}
