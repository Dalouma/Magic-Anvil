using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggger : MonoBehaviour
{
    //public CustomerData customerData;
    public CustomerManager customerManager;
    public void triggerDialogue()
    {
        if (customerManager != null)
        {
            Debug.Log("chosenWeapon: " + customerManager.chosenWeapon);
            Debug.Log(customerManager.getData().name);
            Dialogue dialogue = new Dialogue(customerManager.getData(),customerManager);
            FindObjectOfType<DialogueManager>().startDialogue(dialogue);
        }
    }

}
