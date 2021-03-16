using UnityEngine;

public class TouchGuest : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref GameObject selectedBellhop, Navigator navigator)
    {
        selectedBellhop = null;

        Guest guest = collider.GetComponent<Guest>();
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
}