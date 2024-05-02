using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

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
    public string headline1;
    public string text1;
    public string headline2;
    public string text2;
    public int[] newsOutcomes;

}
public class Weapon
{
    public static string weapon;
    public static Dictionary<string, string> chosenWeapons = new Dictionary<string, string>();
}



[System.Serializable]
public class CustomerManager : MonoBehaviour
{
    public GameObject triggerDialogueObj;
    public GameObject levelChanger;
    public GenericCustomerData GenericCustomer;
    private LevelChanger levelChangerScript;
    public Animator animator;
    //public Image customerImage;
    public ShopManager shopManager;
    public int customer = 0;
    public static int cnum;
    public static int currRep=20;
    public static CustomerManager Instance;
    public CustomerData data;
    public string chosenWeapon;
    public static List<string> customerfileList = new List<string>();
    public string[] namedCustomers = { "Adventurer", "Rogue", "Paladin" };
    public SpriteSwapCustomer cust;
    public SpriteSwapCustomer weap;
    public speechBubbleanimation speechbubble;
    public UnityEngine.UI.Slider bar;
    public int score;
    private int day;
    private int basePrice = 300;
    System.Random rnd = new System.Random();
    public enum CustomerState
    {
        Intro,
        GivenItem,
        BadDeal,
        GoodDeal,
        NeutralDeal,
        Refusal
    }
    ;
    public CustomerData getData()
    {
        return data;
    }
    private void GenDailyCustomers()
    {
        
        //maybe get more customers based on reputation?
        for (int i=0;i<Math.Ceiling(namedCustomers.Length*(double)(currRep/100));i++)
        {

            customerfileList.Add(namedCustomers[rnd.Next(0, namedCustomers.Length - 1)]);
        }
        for (int i = 0; i < Math.Floor(namedCustomers.Length * (double)(currRep / 100)); i++)
        {
            customerfileList.Insert(rnd.Next(0, namedCustomers.Length - 1), "Customer");
        }


    }
    private CustomerData GenGenericCustomer()
    {
        CustomerData Gencust = new CustomerData();
        Gencust.name = "Customer";
        Gencust.stinginess=rnd.Next(7, 10)/10;

        //generic customers don't show up in newspaper
        Gencust.newsOutcomes= Array.Empty<int>();
    }

    private GameManager manager = GameManager.Instance;
    Locale oldLocale;
    Locale currentLocale;

    void Update() 
    {
        currentLocale = LocalizationSettings.SelectedLocale;
        if (oldLocale.Identifier != currentLocale.Identifier) 
        {
            oldLocale = LocalizationSettings.SelectedLocale;
            DialogueTriggger triggerScript = triggerDialogueObj.GetComponent<DialogueTriggger>();

            if (currentLocale.Identifier == "zh-Hans") 
            {
                customerfileList[0] = "AdventurerCh";
                customerfileList[1] = "RogueCh";
                customerfileList[2] = "PaladinCh";
            }
            else 
            {
                customerfileList[0] = "Adventurer";
                customerfileList[1] = "Rogue";
                customerfileList[2] = "Paladin";
            }

            loadCustomer();
            triggerScript.triggerDialogue();
        }
    }

    public static CustomerState state = CustomerState.Intro;
    void Start()
    {
        oldLocale = LocalizationSettings.SelectedLocale;
        currentLocale = LocalizationSettings.SelectedLocale;
/*        if (currentLocale.Identifier == "en") 
        {
            customerfileList[0] = "Adventurer";
            customerfileList[1] = "Rogue";
            customerfileList[2] = "Paladin";
        }
        else 
        {
            customerfileList[0] = "AdventurerCh";
            customerfileList[1] = "RogueCh";
            customerfileList[2] = "PaladinCh";
        }*/
    

        levelChangerScript = levelChanger.GetComponent<LevelChanger>();
        //GameManager manager = GameManager.Instance;
        if(levelChangerScript.daycount>day)
        {
            this.GenDailyCustomers();
        }
        day = levelChangerScript.daycount;

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
        //Debug.Log(getData().name);
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
        string content;
        Debug.Log(customerfileList[2]);
        Debug.Log(customer);
        if(customerfileList[customer]=="Customer")
        {
            data = GenGenericCustomer();
        }
        else {
            string filePath = Path.Combine(Application.streamingAssetsPath, "DialogueData/" + customerfileList[customer] + ".json");
            WWW www = new WWW(filePath);
            while (!www.isDone) { }
            content = www.text;
            data = JsonUtility.FromJson<CustomerData>(content);
        }
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

        if (currentLocale.Identifier == "en") 
        {
            string customerName = customerfileList[cnum];
            Weapon.chosenWeapons.Add(customerName, weapon);
            Weapon.chosenWeapons.Add(customerName + "Ch", weapon);
        }
        else 
        {
            string customerName = customerfileList[cnum];
            Weapon.chosenWeapons.Add(customerName, weapon);
            Weapon.chosenWeapons.Add(customerName.Substring(0, customerName.Length - 2), weapon);
        }
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
