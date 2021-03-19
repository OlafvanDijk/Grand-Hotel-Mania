using System.Collections.Generic;
using UnityEngine;

public class TouchSupply : TouchInteraction
{
    /// <summary>
    /// Bellhop gets the supply NavigationInteraction added to it's queue.
    /// Selected guest is set to null and rooms highlights will disappear.
    /// </summary>
    /// <param name="collider">Collider of touched GameObject.</param>
    /// <param name="moneyHandler">Reference to the moneyHandler.</param>
    /// <param name="objectiveHandler">Reference to the objectiveHandler.</param>
    /// <param name="selectedGuest">Actual Reference to the selectedGuest in the TouchInput Script.</param>
    /// <param name="bellhop">Actual Reference to the bellhop in the TouchInput Script.</param>
    public override void TouchInteract(Collider2D collider, MoneyHandler moneyHandler, ObjectiveHandler objectiveHandler, ref Guest selectedGuest, ref Bellhop bellhop)
    {
        if (bellhop)
        {
            NavigationInteraction supply = collider.GetComponent<NavigationInteraction>();
            bellhop.AddInteractionToQueue(supply);

            if (selectedGuest)
            {
                selectedGuest.navigator.HighlightRooms(false);
                selectedGuest = null;
            }
        }
    }
}
