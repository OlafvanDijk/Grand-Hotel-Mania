using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    [SerializeField] private Navigator navigator; 
    private Guest selectedGuest;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = GetTouch();
        if (hit.collider)
        {
            switch (hit.collider.tag)
            {
                case "Guest":
                    InteractWithGuest(hit.collider);
                    break;
                case "Room":
                    InteractWithRoom(hit.collider);
                    break;
                case "Bellhop":
                    //select bellhop
                    break;
                case "Supply":
                    InteractWithSupply(hit.collider);
                    break;
            }
        }
    }

    private RaycastHit2D GetTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
                return Physics2D.Raycast(position, Vector2.zero);
            }
        }
        return new RaycastHit2D();
    }

    private void InteractWithGuest(Collider2D guestObject)
    {
        Guest guest = guestObject.GetComponent<Guest>();
        if (guest.CheckIn)
        {
            //TODO Show available rooms

            selectedGuest = guest;
            return;
        }
        else if (guest.CheckOut)
        {
            //TODO Collect cash
            //TODO Send to exit to despawn
            selectedGuest = null;
            return;
        }
    }

    private void InteractWithRoom(Collider2D roomObject)
    {
        NavigationInteraction navInteraction = roomObject.GetComponent<NavigationInteraction>(); //Getcomponent
        NavigationPoint navPoint = navInteraction.navigationPoint;
        if (selectedGuest)
        {
            List<Vector2> route = navigator.GetRoute(selectedGuest.currentPosition, navPoint);
            if (route != null)
            {
                selectedGuest.SetRoute(route);
                selectedGuest.CheckIn = false;
                //TODO Close room -> not available
            }
        }
        //else if (bellhop)
        //  if (shouldClean)
        //      TODO Navigate bellhop to room

        selectedGuest = null;
    }

    private void InteractWithSupply(Collider2D supplyObject)
    {
        //  if(bellhop)
        //      Navigate bellhop to supply
    }
}
