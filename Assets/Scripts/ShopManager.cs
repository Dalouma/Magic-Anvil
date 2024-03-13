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
    public static int money = 450;
    public int price = 0;
    public TextMeshProUGUI moneyText;
    public TMP_InputField moneyInputField;
    private GameManager manager = GameManager.Instance;
    public static int[] materials = { 0, 0 };
    public TextMeshProUGUI metalAMT;
    public TextMeshProUGUI woodAMT;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
  
    }
    public void Start()
    {
        if (manager != null && manager.pullFromSave == true)
        {
            money = manager.currency;
            Debug.Log(money);
        }

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
            customerManager.animator.SetBool("Available", false);
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
        if (money.Length > 8)
        {
            customerManager.priceCheck(99999);
        }
        else
        {
            price = int.Parse(money);
            if (customerManager.priceCheck(price))
            {
                UPdateMoney(price);
            }
        }
    }
    public void addMetal()
    {
        if (money - 200 >= 0)
        {
            materials[0]++;
            metalAMT.text = materials[0].ToString();
            money -= 200;
            moneyText.text = money.ToString();
        }
    }
    public void addWood()
    {
        if (money - 200 >= 0)
        {
          materials[1]++;
        woodAMT.text = materials[1].ToString();
        money -= 200;
        moneyText.text = money.ToString();
        }
    }
    public void loseMetal()
    {
        materials[0]--;
        metalAMT.text = materials[0].ToString();
    }
    public void loseWood()
    {
        materials[1]--;
        woodAMT.text = materials[1].ToString();
    }
    public void UPdateMoney(int price)
    {
        money += price;
        moneyText.text =money.ToString();
        animator.SetBool("ShopShow", false);

        if (manager != null)
        {
            manager.currency = money;
        }

    }



}
