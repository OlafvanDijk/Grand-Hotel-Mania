using System.Collections.Generic;
using UnityEngine;

public class TouchSupply : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref Bellhop selectedBellhop)
    {
        if (selectedBellhop)
        {
            NavigationInteraction supply = collider.GetComponent<NavigationInteraction>();
            selectedBellhop.AddInteractionToQueue(supply);
        }
    }
}
