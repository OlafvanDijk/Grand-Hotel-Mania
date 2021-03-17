using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : NavigationInteraction
{
    public override void NavInteract(GameObject gameObject)
    {
        Guest guest = gameObject.GetComponent<Guest>();
        if (guest.checkIn)
        {
            guest.CheckIn();
        }
        else if (guest.checkOut)
        {
            guest.CheckOut();
        }
    }
}
