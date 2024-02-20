using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public CustomerManager customerManager;
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI dialoguetext;
    public Queue<string> sentences;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void startDialogue(Dialogue x)
    {
        animator.SetBool("Isopen", true);
        Debug.Log("starting"+x.name);
        nametext.text = x.name;
        sentences.Clear();
        foreach (string sentence in x.sentences) 
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sent=sentences.Dequeue();
        dialoguetext.text = sent;
    }
    public void EndDialogue()
    {
        
        animator.SetBool("Isopen", false);
        if(customerManager.getState()==CustomerManager.CustomerState.NeutralDeal|| customerManager.getState() == CustomerManager.CustomerState.BadDeal|| customerManager.getState() == CustomerManager.CustomerState.GoodDeal)
        {
            customerManager.nextCustomer();
        }
        if (customerManager.data.hatedWeapons.Contains(customerManager.chosenWeapon))
        {
            customerManager.nextCustomer();
        }
        Debug.Log("end");
    }
}
