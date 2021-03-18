using UnityEngine;

public abstract class TouchInteraction : MonoBehaviour
{
    public abstract void TouchInteract(Collider2D collider, MoneyHandler moneyHandler, ObjectiveHandler objectiveHandler, ref Guest selectedGuest, ref Bellhop bellhop);
}