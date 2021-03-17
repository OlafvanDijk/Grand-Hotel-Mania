using UnityEngine;

public abstract class TouchInteraction : MonoBehaviour
{
    public abstract void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref Bellhop bellhop);
}