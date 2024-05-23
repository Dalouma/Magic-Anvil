using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class CharacterData : ScriptableObject
{
    public new string name;
    public Sprite characterSprite;
    public CustomerData customerData;

    [TextArea(3, 5)] public string[] introText;
}
