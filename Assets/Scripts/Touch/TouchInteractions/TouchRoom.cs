using System.Collections.Generic;
using UnityEngine;

public class TouchRoom : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref GameObject selectedBellhop, Navigator navigator)
    {
        Room room = collider.GetComponent<Room>();
        NavigationPoint navPoint = room.navigationPoint;
        if (selectedGuest)
        {
            room.SetNavigator(navigator);
            List<Vector2> route = navigator.GetRoute(selectedGuest.currentPosition, navPoint);
            if (route != null)
            {
                selectedGuest.SetRoute(route, room);
                selectedGuest.checkIn = false;
                selectedGuest.CheckIn();
                room.DoorState(false);
            }
        }
        //else if (bellhop)
        //  if (shouldClean)
        //      TODO Navigate bellhop to room

        selectedGuest = null;
    }
}
