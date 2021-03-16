using System.Collections.Generic;
using UnityEngine;

public class TouchRoom : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref GameObject selectedBellhop, Navigator navigator)
    {
        Room navInteraction = collider.GetComponent<Room>();
        NavigationPoint navPoint = navInteraction.navigationPoint;
        if (selectedGuest)
        {
            List<Vector2> route = navigator.GetRoute(selectedGuest.currentPosition, navPoint);
            if (route != null)
            {
                selectedGuest.SetRoute(route, navInteraction);
                selectedGuest.CheckIn = false;
                navInteraction.DoorState(false);
            }
        }
        //else if (bellhop)
        //  if (shouldClean)
        //      TODO Navigate bellhop to room

        selectedGuest = null;
    }
}
