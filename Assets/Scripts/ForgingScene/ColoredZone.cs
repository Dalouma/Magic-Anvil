using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredZone : MonoBehaviour
{
    [SerializeField] bool green;
    [SerializeField] bool yellow;
    [SerializeField] bool orange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pointer"))
        {
            if (green)
                ForgingV2.inGreen = true;
            else if (yellow)
                ForgingV2.inYellow = true;
            else if (orange)
                ForgingV2.inOrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("pointer"))
        {
            if (green)
                ForgingV2.inGreen = false;
            else if (yellow)
                ForgingV2.inYellow = false;
            else if (orange)
                ForgingV2.inOrange = false;
        }
    }
}
