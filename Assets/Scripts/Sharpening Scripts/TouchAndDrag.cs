using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAndDrag : MonoBehaviour
{
    [SerializeField] private Transform startingPos;
    private bool dragging;

    private void Start()
    {
        dragging = false;
    }

    void Update()
    {
        if (Input.touchCount> 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            // On Finger Down, check to see if finger is over item
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
                if (hit && hit.transform.gameObject == gameObject)
                {
                    Debug.Log("Clicked on Item");
                    dragging = true;
                }
            }
            // On Finger Drag, move item to finger position
            if (dragging && touch.phase == TouchPhase.Moved)
            {
                touchPos.z = 0f;
                transform.parent.position = touchPos;
            }
            // On Finger release, snap item back to starting position
            if (touch.phase == TouchPhase.Ended)
            {
                dragging = false;
                transform.parent.position = startingPos.position;
            }
        }
    }
}
