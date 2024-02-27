using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public DialogueTriggger dialogue;
    public CustomerManager customerManager;
    public static ShopManager instance;
    public Animator animator;
    public static int money = 0;
    public int price = 0;
    public TextMeshProUGUI moneyText;
    public TMP_InputField moneyInputField;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
  
    }
    public void Start()
    {
        moneyText.text = money.ToString();
    }
    public void ShowShop()
    {
        if (customerManager.data.hatedWeapons.Contains(customerManager.chosenWeapon))
        {
            dialogue.triggerDialogue();
            customerManager.animator.SetBool("Available", false);
        }
        else
        {
            dialogue.triggerDialogue();
            animator.SetBool("ShopShow", true);
        }
        
    }
   /* public void ShowForge()
    {
        if (forge.GetBool("forge",false)
        {
            forge.SetBool("forge", true);
        }
        else
        {
            forge.SetBool("forge", false);
        }

    }*/
    public void setPrice(string money)
    {
        price=int.Parse(money);
        if (customerManager.priceCheck(price))
        {
            UPdateMoney();
        }
    }
    public void UPdateMoney()
    {
        money += price;
        moneyText.text =money.ToString();
        animator.SetBool("ShopShow", false);

    }



}
