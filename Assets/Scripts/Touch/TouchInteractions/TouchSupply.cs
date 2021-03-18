using System.Collections.Generic;
using UnityEngine;

public class TouchSupply : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, MoneyHandler moneyHandler, ObjectiveHandler objectiveHandler, ref Guest selectedGuest, ref Bellhop bellhop)
    {
        if (bellhop)
        {
            NavigationInteraction supply = collider.GetComponent<NavigationInteraction>();
            bellhop.AddInteractionToQueue(supply);
        }
    }
}
