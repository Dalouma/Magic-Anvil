using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.UI;

[System.Serializable]
public class CustomerData
{
    public string name;
    public string[] preferredWeapons;
    public string[] hatedWeapons;
    public float stinginess;
    public string introDialogue;
    public string secondLine;
    public string payment;
    public string preferred;
    public string unpreferred;
    public string baddeal;
    public string neutral;
    public string gooddeal;
    public string refusal;

}
public class Weapon
{
    public static string weapon;
}



[System.Serializable]
public class CustomerManager : MonoBehaviour
{
    public GameObject levelChanger;
    private LevelChanger levelChangerScript;
    public Animator animator;
    //public Image customerImage;
    public ShopManager shopManager;
    public int customer = 20;
    public static int cnum;
    public static int currRep=20;
    public static CustomerManager Instance;
    public CustomerData data;
    public string chosenWeapon;
    public string[] customerfileList = { "Paladin", "Rogue", "Barbarian" };
    public SpriteSwapCustomer cust;
    public SpriteSwapCustomer weap;
    public speechBubbleanimation speechbubble;
    public UnityEngine.UI.Slider bar;
    public int score;
    private int basePrice = 300;
    public enum CustomerState
    {
        Intro,
        GivenItem,
        BadDeal,
        GoodDeal,
        NeutralDeal,
        Refusal
    }
    public CustomerData getData()
    {
        return data;
    }

    private GameManager manager = GameManager.Instance;

    public static CustomerState state = CustomerState.Intro;
    void Start()
    {
        levelChangerScript = levelChanger.GetComponent<LevelChanger>();
        //GameManager manager = GameManager.Instance;

        if (manager != null && manager.pullFromSave == true) 
        {
            customer = manager.character;
            Debug.Log(customer);
        }
        else 
        {
            customer = cnum;
        }
        if (ForgingScript.score != null)
        {
            score = ForgingScript.score + (int)SharpeningScript.score;
        }
        //customer = cnum;
        cust.changeSprite(customer);
        loadCustomer();
        SetRep(currRep);
        Debug.Log(getData().name);
        chosenWeapon = Weapon.weapon;
        
        if(chosenWeapon != null)
        {
            switch(chosenWeapon)
            {
                case "Shield":
                    weap.changeSprite(0);
                    break;
                case "Sword":
                    weap.changeSprite(1);
                    break;
                case "Dagger":
                    weap.changeSprite(2);
                    break;
                case "Hammer":
                    weap.changeSprite(3);
                    break;
                case "Axe":
                    weap.changeSprite(4);
                    break;
                default:
                    break;
            }
            animator.SetBool("Available", true);
        }
        else
        {
            animator.SetBool("Available", false);
        }
  

    }
   /* private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        //Instance = this;
        //DontDestroyOnLoad(gameObject);
    }*/

    public void GoToForging()
    {
        if (ShopManager.materials[0] > 0 && ShopManager.materials[1] > 0)
        {
            shopManager.loseMetal();
            shopManager.loseWood();
            state = CustomerState.GivenItem;
            levelChangerScript.FadeToLevel("ForgingScene");
        }
    }
    public void loadCustomer()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "DialogueData/" + customerfileList[customer] + ".json");
        string content = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<CustomerData>(content);
        cnum = customer;
        

    }
    public void nextCustomer()
    {
        if (customer < 2)
        {
            customer++;

            if (manager != null)
            {
                manager.character++;
            }

            cnum = customer;
            loadCustomer();
            cust.changeSpriteAnim(customer);
            speechbubble.speechFade();
            Weapon.weapon = null;
            chosenWeapon = null;
            state = CustomerState.Intro;
            animator.SetBool("Available", false);
            
            //customerImage.ChangeImage(customerfileList[customer]);
        }
        else
        {
            SceneManager.LoadScene("NewspaperScene");
        }
    }
    public bool priceCheck(int price)
    {
        
        if(price>(1.4*basePrice*data.stinginess+(basePrice*(currRep/100))+ (basePrice * (score / 100))) ||price<0)
        {
            state = CustomerState.Refusal;
            return false;
        }
        else if(price > (1.15 * basePrice * data.stinginess + (basePrice * (currRep / 100)) + (basePrice * (score / 100))))
         {
            state = CustomerState.BadDeal;
            currRep -= 10;
            SetRep(currRep);
            return true;
        }
        else if(price > (1 * basePrice * data.stinginess + (basePrice * (currRep / 100)) + (basePrice * (score / 100))))
            {
            state = CustomerState.NeutralDeal;
            return true;

        }
        else
        {
            if(price==0)
            {
                currRep += 99;
                SetRep(currRep);
                state = CustomerState.GoodDeal;
                return true;
            }
            //Debug.Log(Math.Floor((400 * data.stinginess + (400 * (currRep / 100))))/price);
            currRep += Convert.ToInt32(Math.Floor((basePrice * data.stinginess + (basePrice * (currRep / 100)) + (basePrice * (score / 100))) /price));
            SetRep(currRep);
            state = CustomerState.GoodDeal;
            return true;

        }
    }
    public CustomerState getState()
    {
        return state;
    }


    public void ChooseWeapon(string weapon)
    {
        chosenWeapon = weapon;
        Weapon.weapon=weapon;
    }
    public string getWeapon()
    {
        return chosenWeapon;
    }
    public void SetRep(int rep)
    {
        bar.value = rep;
    }
}
