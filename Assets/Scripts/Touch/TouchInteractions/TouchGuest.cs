using UnityEngine;

public class TouchGuest : TouchInteraction
{
    /// <summary>
    /// If Guest is touched during checking in then the rooms wil be highlighted and the guest will be selected.
    /// If Guest is touched uring checking out then the guest will be checked out, money will be added by the moneyhandler and objectivehandler and the selectedGuest will be set to null.
    /// </summary>
    /// <param name="collider">Collider of touched GameObject.</param>
    /// <param name="moneyHandler">Reference to the moneyHandler.</param>
    /// <param name="objectiveHandler">Reference to the objectiveHandler.</param>
    /// <param name="selectedGuest">Actual Reference to the selectedGuest in the TouchInput Script.</param>
    /// <param name="bellhop">Actual Reference to the bellhop in the TouchInput Script.</param>
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