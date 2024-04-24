using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Dialogue
    
{
    //CustomerManager customerManager;
    public string name;
    [TextArea(3,10)]
    public string[] sentences;
    public Dialogue(string name, string[] sentences)
    {
        this.name = name;
        this.sentences = sentences;
    }
        public Dialogue(CustomerData customerData, CustomerManager customerManager)
        {
            this.name = customerData.name;
        Debug.Log(name);
        switch(CustomerManager.state)
        { 
        case CustomerManager.CustomerState.Intro:
            this.sentences = new string[]
            {
                    customerData.introDialogue,
                    customerData.secondLine
            };
            break;
        case CustomerManager.CustomerState.GivenItem:
                if (customerData != null&&customerManager!=null)
                {
                    Debug.Log("hatedWeapons: " + string.Join(", ", customerData.hatedWeapons));
                    Debug.Log("chosenWeapon: " + customerManager.getWeapon());
                    if (customerData.hatedWeapons.Contains(customerManager.chosenWeapon))
                    {
                        this.sentences = new string[]
                   {
                    customerData.unpreferred,

                   };
                    }
                    else
                    {
                        this.sentences = new string[]
              {
                    customerData.preferred,
                    customerData.payment
              };
                    }
                }
                break;
            case CustomerManager.CustomerState.Refusal:
                this.sentences = new string[]
                   {
                    customerData.refusal,
                   };
                break;
            case CustomerManager.CustomerState.BadDeal:
                this.sentences = new string[]
                   {
                    customerData.baddeal,

                   };
                break;
            case CustomerManager.CustomerState.GoodDeal:
                this.sentences = new string[]
                   {
                    customerData.gooddeal,

                   };
                break;
            case CustomerManager.CustomerState.NeutralDeal:
                this.sentences = new string[]
                   {
                    customerData.neutral,

                   };
                break;

            default:
                break;
        }
        Debug.Log(sentences[0]);
        }
    }


