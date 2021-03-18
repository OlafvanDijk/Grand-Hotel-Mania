using UnityEngine;

public class TouchGuest : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, MoneyHandler moneyHandler, ObjectiveHandler objectiveHandler, ref Guest selectedGuest, ref Bellhop bellhop)
    {
        Guest guest = collider.GetComponent<Guest>();
        if (guest.checkIn)
        {
            selectedGuest = guest;
            selectedGuest.navigator.HighlightRooms(true);
            return;
        }
        else if (guest.checkOut)
        {
            moneyHandler.CheckOut();
            objectiveHandler.AddObjectiveAmount(1, ObjectiveType.Keys);
            guest.checkOut = false;
            guest.CheckOut();
            selectedGuest = null;
            return;
        }
    }
}