using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] CharacterData customerData;

    private void Start()
    {
        GetComponent<Image>().sprite = customerData.characterSprite;
    }

    public void CalculatePayment()
    {
       
    }

}
