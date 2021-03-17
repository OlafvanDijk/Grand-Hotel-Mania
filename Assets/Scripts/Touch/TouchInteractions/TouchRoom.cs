using System.Collections.Generic;
using UnityEngine;

public class TouchRoom : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref Bellhop selectedBellhop)
    {
        Room room = collider.GetComponent<Room>();
        NavigationPoint navPoint = room.navigationPoint;
        if (selectedGuest)
        {
            List<Vector2> route = selectedGuest.GetRoute(selectedGuest.currentPosition, navPoint);
            if (route != null)
            {
                selectedGuest.SetRoute(route, room);
                selectedGuest.checkIn = false;
                selectedGuest.CheckIn();
                room.DoorState(false);
            }
        }
        else if (selectedBellhop)
        {
            if (room.shouldClean)
            {
                selectedBellhop.AddInteractionToQueue(room);
            }
        }

        selectedGuest = null;
    }
}
