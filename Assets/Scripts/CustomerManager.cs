using System.Collections;
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
    public Animator animator;
    //public Image customerImage;
    public ShopManager shopManager;
    public int customer = 0;
    public static int cnum;
    public static CustomerManager Instance;
    public CustomerData data;
    public string chosenWeapon;
    public string[] customerfileList = { "Paladin", "Rogue", "Barbarian" };
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

    public static CustomerState state = CustomerState.Intro;
    void Start()
    {
        customer = cnum;
        loadCustomer();
        Debug.Log(getData().name);
        chosenWeapon = Weapon.weapon;
        if(chosenWeapon != null)
        {
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
        Debug.Log("loading forging scene");
        Debug.Log("chosenWeapon: " + chosenWeapon);
        state = CustomerState.GivenItem;
        SceneManager.LoadScene(1);
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
            cnum = customer;
            loadCustomer();
            Weapon.weapon = null;
            chosenWeapon = null;
            state = CustomerState.Intro;
            animator.SetBool("Available", false);
            //customerImage.ChangeImage(customerfileList[customer]);
        }
    }
    public bool priceCheck(int price)
    {
        
        if(price>(1.4*400*data.stinginess)||price<0)
        {
            state = CustomerState.Refusal;
            return false;
        }
        else if(price > (1.15 * 400 * data.stinginess))
         {
            state = CustomerState.BadDeal;
            return true;
        }
        else if(price > (1 * 400 * data.stinginess))
            {
            state = CustomerState.NeutralDeal;
            return true;

        }
        else
        {
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
}
