using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsMenuToggle : MonoBehaviour
{
    public Animator anim;
    public void Open()
    {
        anim.SetBool("open", true);
    }
    public void close() 
    {
        anim.SetBool("open", false);
    }

}
