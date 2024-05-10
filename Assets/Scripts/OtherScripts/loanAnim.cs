using UnityEngine;

public class loanAnim : MonoBehaviour
{
    public ShopManager shopManager;
    public CustomerManager customerManager;
    public Animator animator;
    public void LoanMoney()
    {
        shopManager.UPdateMoney(400);
        customerManager.SetRep(CustomerManager.currRep - 10);
    }

    private void Update()
    {
        if (ShopManager.money < 200 && (ShopManager.materials[0] < 1 || ShopManager.materials[1] < 1))
        {
            animator.SetBool("showloan", true);
        }
        else
        {
            animator.SetBool("showloan", false);
        }
    }
}
