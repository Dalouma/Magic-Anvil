using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMatShop : MonoBehaviour
{
    public Animator animator;

        public void OpenShop()
    { 
        if (animator.GetBool("open") == false)
        {
            animator.SetBool("open", true);
        }
        else
        {
            animator.SetBool("open", false);
        }
    }

}
