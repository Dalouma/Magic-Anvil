using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togglePanels : MonoBehaviour
{
    public GameObject UIpanel;

    void Start() 
    {
        //UIpanel.SetActive(false);
    }

    public void TogglePanel() 
    {
        UIpanel.SetActive(!UIpanel.activeSelf);
        Debug.Log("Panel activated");
    }
}
