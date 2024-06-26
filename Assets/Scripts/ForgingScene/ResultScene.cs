using TMPro;
using UnityEngine;

public class ResultScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI resultText;
    private float score;
    private int sharpeningHits = SharpeningScript.correctWordInputs;
    private int greatTiming = ForgingScript.greatTiming;
    private int goodTiming = ForgingScript.goodTiming;
    private int badTiming = ForgingScript.badTiming;
    void Start()
    {
        //string weaponText = "Weapon: " + Weapon.weapon;
        string weaponText = "Weapon: EMPTY (REFACTOR)";
        string sharpeningText = "\nSharpening Hits: " + sharpeningHits;
        string forgingText = "\nGreat Timing: " + greatTiming + "\nGood Timing: "
         + goodTiming + "\nBad Timing: " + badTiming;
        calculateScore();
        resultText.text = weaponText + forgingText + sharpeningText + "\n Score: " + score;
    }

    void calculateScore()
    {
        score = (((greatTiming * 2) + goodTiming + badTiming / 2) / 20 + sharpeningHits / 4) * 100;

    }
}
