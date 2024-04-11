using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    // Only 1 instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
