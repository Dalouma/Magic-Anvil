using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/CraftableItem")]
public class ItemData : ScriptableObject
{
    public string ID;
    public Sprite fullArt;
    public Sprite icon;
    public GameObject prefab;
    public string[] itemGrades;
    public List<string> materials;
    public List<int> materialAmount;
}