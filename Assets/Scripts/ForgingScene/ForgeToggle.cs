using UnityEngine;

public class ForgeToggle : MonoBehaviour
{
    public Animator forge;

    public void ShowForge()
    {
        if (forge.GetBool("forge") == false)
        {
            forge.SetBool("forge", true);
        }
        else
        {
            forge.SetBool("forge", false);
        }

    }

}
