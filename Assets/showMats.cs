using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showMats : MonoBehaviour
{
    public Animator animator;

    public void OpenShop()
    {
        animator.SetBool("matshow", true);
    }
    public void CloseShop()
    {
        animator.SetBool("matshow", false);
    }
}
