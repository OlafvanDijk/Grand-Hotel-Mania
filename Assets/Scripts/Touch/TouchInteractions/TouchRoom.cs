using System.Collections.Generic;
using UnityEngine;

public class TouchRoom : TouchInteraction
{
    /// <summary>
    /// If Room is touched while a guest is selected and the room is available then the guest will be sent to the room and will be checked in.
    /// Else if the Bellhop is not null and the room should be cleaned then the room wil not be available to guests and the bellhop is sent over to clean it.
    /// Selected guest is always set to null if one was selected.
    /// </summary>
    /// <param name="collider">Collider of touched GameObject.</param>
    /// <param name="moneyHandler">Reference to the moneyHandler.</param>
    /// <param name="objectiveHandler">Reference to the objectiveHandler.</param>
    /// <param name="selectedGuest">Actual Reference to the selectedGuest in the TouchInput Script.</param>
    /// <param name="bellhop">Actual Reference to the bellhop in the TouchInput Script.</param>
    public override void TouchInteract(Collider2D collider, MoneyHandler moneyHandler, ObjectiveHandler objectiveHandler, ref Guest selectedGuest, ref Bellhop bellhop)
    {
        Room room = collider.GetComponent<Room>();
        NavigationPoint navPoint = room.navigationPoint;
        if (selectedGuest & room.availableToGuests)
        {
            List<Vector2> route = selectedGuest.GetRoute(selectedGuest.currentPosition, navPoint);
            if (route != null)
            {
                selectedGuest.navigator.HighlightRooms(false);
                selectedGuest.SetRoute(route, room);
                selectedGuest.checkIn = false;
                selectedGuest.CheckIn();
                moneyHandler.CheckIn();
                room.DoorState(false);
            }
        }
        else if (bellhop)
        {
            if (room.shouldClean)
            {
                room.availableToGuests = false;
                bellhop.AddInteractionToQueue(room);
            }
        }

        if (selectedGuest)
        {
            selectedGuest.navigator.HighlightRooms(false);
            selectedGuest = null;
        }
    }
}
