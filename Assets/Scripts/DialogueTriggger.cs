using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggger : MonoBehaviour
{
    public Dialogue dialogue;
    public void triggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }

}
