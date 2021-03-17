using System.Collections.Generic;
using UnityEngine;

public class TouchSupply : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref Bellhop bellhop)
    {
        if (bellhop)
        {
            Debug.Log("Touched");
            NavigationInteraction supply = collider.GetComponent<NavigationInteraction>();
            bellhop.AddInteractionToQueue(supply);
        }
    }
}
