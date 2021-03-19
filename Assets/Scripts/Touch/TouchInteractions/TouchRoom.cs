using System.Collections.Generic;
using UnityEngine;

public class TouchRoom : TouchInteraction
{
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
