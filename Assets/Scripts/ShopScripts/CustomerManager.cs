using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CustomerData
{
    public string name;
    public List<string> preferredWeapons = new List<string>();
    public List<string> hatedWeapons = new List<string>();
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

public class GenericCustomerData
{
    public List<string> preferredWeapons = new List<string>();
    public List<string> hatedWeapons = new List<string>();
    public List<string> introDialogue = new List<string>();
    public List<string> secondLine = new List<string>();
    public List<string> payment = new List<string>();
    public List<string> preferred = new List<string>();
    public List<string> unpreferred = new List<string>();
    public List<string> baddeal = new List<string>();
    public List<string> neutral = new List<string>();
    public List<string> gooddeal = new List<string>();
    public List<string> refusal = new List<string>();
    
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
    public static double currRep = 20.0;
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
    private static int day = 0;
    private static CustomerData tempData;
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
        Debug.Log(Math.Ceiling(namedCustomers.Length * (currRep / 100)));

        //maybe get more customers based on reputation?
        for (int i = 0; i < Math.Ceiling(namedCustomers.Length * (currRep / 100)); i++)
        {
            Debug.Log(namedCustomers[rnd.Next(0, namedCustomers.Length)]);

            customerfileList.Add(namedCustomers[rnd.Next(0, namedCustomers.Length)]);
        }
        for (int i = 0; i < Math.Floor(namedCustomers.Length * (currRep / 100)) + day; i++)
        {
            customerfileList.Insert(rnd.Next(0, customerfileList.Count), "Customer");
        }


    }
    public CustomerData GenGenericCustomer()
    {
        string content;
        string filePath = Path.Combine(Application.streamingAssetsPath, "DialogueData/GenericCustomerData.json");
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        www.SendWebRequest();

        while (!www.isDone) { }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load file: " + www.error);
            return null;
        }

        content = www.downloadHandler.text;
        Debug.Log(content);
        GenericCustomer = JsonUtility.FromJson<GenericCustomerData>(content);
        Debug.Log("Generic Customer:" + GenericCustomer.preferredWeapons[1]);


        int customergenint = rnd.Next(0, 3);
        CustomerData Gencust = new CustomerData();
        Gencust.name = "Customer";
        Debug.Log("CustomerNAme:" + Gencust.name);
        Gencust.stinginess = (float)(rnd.Next(7, 10)) / 10;
        if (customergenint == 0)
        {
            Gencust.preferredWeapons.Add(GenericCustomer.preferredWeapons[customergenint]);
            Gencust.hatedWeapons.Add(GenericCustomer.hatedWeapons[customergenint + 1]);
            Gencust.introDialogue = GenericCustomer.introDialogue[customergenint];
            Gencust.secondLine = GenericCustomer.secondLine[customergenint];

        }
        if (customergenint == 1)
        {
            Gencust.preferredWeapons.Add(GenericCustomer.preferredWeapons[customergenint]);
            Gencust.preferredWeapons.Add(GenericCustomer.preferredWeapons[4]);
            Gencust.hatedWeapons.Add(GenericCustomer.hatedWeapons[2]);
            Gencust.introDialogue = GenericCustomer.introDialogue[customergenint];
            Gencust.secondLine = GenericCustomer.secondLine[customergenint];
        }
        if (customergenint == 2)
        {
            Gencust.preferredWeapons.Add(GenericCustomer.preferredWeapons[2]);
            Gencust.preferredWeapons.Add(GenericCustomer.preferredWeapons[4]);
            Gencust.hatedWeapons.Add(GenericCustomer.hatedWeapons[0]);
            Gencust.introDialogue = GenericCustomer.introDialogue[customergenint];
            Gencust.secondLine = GenericCustomer.secondLine[customergenint];
        }
        Gencust.payment = GenericCustomer.payment[rnd.Next(0, 3)];
        Gencust.preferred = GenericCustomer.preferred[rnd.Next(0, 3)];
        Gencust.unpreferred = GenericCustomer.unpreferred[rnd.Next(0, 3)];
        Gencust.refusal = GenericCustomer.refusal[rnd.Next(0, 3)];
        Gencust.baddeal = GenericCustomer.baddeal[rnd.Next(0, 3)];
        Gencust.gooddeal = GenericCustomer.gooddeal[rnd.Next(0, 3)];
        Gencust.neutral = GenericCustomer.neutral[rnd.Next(0, 3)];
        //generic customers don't show up in newspaper
        Gencust.newsOutcomes = Array.Empty<int>();
        return Gencust;
    }


    private GameManager manager = GameManager.Instance;
    Locale oldLocale;
    Locale currentLocale;

    void Update()
    {
        
    }

    public static CustomerState state = CustomerState.Intro;
    void Start()
    {


        levelChangerScript = levelChanger.GetComponent<LevelChanger>();
        //GameManager manager = GameManager.Instance;
        Debug.Log(levelChangerScript.daycount);
        if (levelChangerScript.daycount > day)
        {
            day++;
            GenDailyCustomers();
            loadCustomer();

        }
        data = tempData;

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
        cust.changeSprite(customerfileList[customer]);
        SetRep(currRep);
        //Debug.Log(getData().name);
        chosenWeapon = Weapon.weapon;

        if (chosenWeapon != null)
        {
            switch (chosenWeapon)
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
        Debug.Log(customerfileList[1]);
        Debug.Log(customerfileList.Count);
        if (customerfileList[customer] == "Customer")
        {
            tempData = GenGenericCustomer();
            
        }
        else
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "DialogueData/GenericCustomerData.json");
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            www.SendWebRequest();

            while (!www.isDone) { }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load file: " + www.error);
            }

            content = www.downloadHandler.text;           
            data = JsonUtility.FromJson<CustomerData>(content);
         
        }
        cnum = customer;


    }
    public void nextCustomer()
    {
        if (customer < customerfileList.Count)
        {
            customer++;

            if (manager != null)
            {
                manager.character++;
            }

            cnum = customer;
            loadCustomer();
            data = tempData;
            cust.changeSpriteAnim(customerfileList[customer]);
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

        if (price > (1.4 * basePrice * data.stinginess + (basePrice * (currRep / 100)) + (basePrice * (score / 50))) || price < 0)
        {
            state = CustomerState.Refusal;
            return false;
        }
        else if (price > (1.15 * basePrice * data.stinginess + (basePrice * (currRep / 100)) + (basePrice * (score / 50))))
        {
            state = CustomerState.BadDeal;
            currRep -= 10;
            SetRep(currRep);
            return true;
        }
        else if (price > (1 * basePrice * data.stinginess + (basePrice * (currRep / 100)) + (basePrice * (score / 50))))
        {
            state = CustomerState.NeutralDeal;
            return true;

        }
        else
        {
            if (price == 0)
            {
                currRep += 99;
                SetRep(currRep);
                state = CustomerState.GoodDeal;
                return true;
            }
            //Debug.Log(Math.Floor((400 * data.stinginess + (400 * (currRep / 100))))/price);
            currRep += Convert.ToInt32(Math.Floor((basePrice * data.stinginess + (basePrice * (currRep / 100)) + (basePrice * (score / 50))) / price));
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
        Weapon.weapon = weapon;

       /* if (currentLocale.Identifier == "en")
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
        }*/
    }
    public string getWeapon()
    {
        return chosenWeapon;
    }
    public void SetRep(double rep)
    {
        bar.value = (int)rep;
    }
}
