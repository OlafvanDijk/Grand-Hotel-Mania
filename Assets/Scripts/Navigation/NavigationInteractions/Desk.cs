using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : NavigationInteraction
{
    /// <summary>
    /// Checks the guest in and out if possible.
    /// </summary>
    /// <param name="gameObject">GameObject Interacting with the NavigationInteraction.</param>
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
