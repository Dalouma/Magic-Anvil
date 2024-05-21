using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public Animator animator;

    public void setTrue()
    {
        animator.SetBool("Isopen", true);
    }
    public void setFalse()
    {
        animator.SetBool("Isopen", false);
    }
}
