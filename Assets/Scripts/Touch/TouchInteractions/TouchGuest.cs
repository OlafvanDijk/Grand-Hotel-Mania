using UnityEngine;

public class TouchGuest : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref Bellhop bellhop)
    {
        Guest guest = collider.GetComponent<Guest>();
        if (guest.checkIn)
        {
            //TODO Show available rooms

            selectedGuest = guest;
            return;
        }
        else if (guest.checkOut)
        {
            //TODO Collect cash
            guest.checkOut = false;
            guest.CheckOut();
            selectedGuest = null;
            return;
        }
    }
}