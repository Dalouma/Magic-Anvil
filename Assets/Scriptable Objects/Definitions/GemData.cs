using UnityEngine;

[CreateAssetMenu(fileName = "Gem", menuName = "ScriptableObjects/Gem")]
public class GemData : ScriptableObject
{
    public new string name;
    public string affixText;
    public Sprite gemArt;
    public Sprite backgroundArt;
}
