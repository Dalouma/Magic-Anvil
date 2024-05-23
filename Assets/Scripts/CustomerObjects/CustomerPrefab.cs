using UnityEngine;

[CreateAssetMenu(fileName = " New Customer", menuName = "Customer")]
public class CustomerPrefab : ScriptableObject
{
    public new string name;
    public string[] preferredWeapons;
    public string[] hatedWeapons;
    public float stinginess;
    public string introDialogue;
    public string secondLine;
    public string payment;
    public string preferred;
    public string unpreferred;
    public string baddeal;
    public string neutral;
    public string gooddeal;
    public string refusal;
    public string headline1;
    public string text1;
    public string headline2;
    public string text2;
    public int[] newsOutcomes;
    public Sprite sprite;
    public Animator anim;

}
