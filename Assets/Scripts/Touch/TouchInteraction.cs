using UnityEngine;

public abstract class TouchInteraction : MonoBehaviour
{
    /// <summary>
    /// Interaction method when touching something.
    /// </summary>
    /// <param name="collider">Collider of touched GameObject.</param>
    /// <param name="moneyHandler">Reference to the moneyHandler.</param>
    /// <param name="objectiveHandler">Reference to the objectiveHandler.</param>
    /// <param name="selectedGuest">Actual Reference to the selectedGuest in the TouchInput Script.</param>
    /// <param name="bellhop">Actual Reference to the bellhop in the TouchInput Script.</param>
    public abstract void TouchInteract(Collider2D collider, MoneyHandler moneyHandler, ObjectiveHandler objectiveHandler, ref Guest selectedGuest, ref Bellhop bellhop);
}