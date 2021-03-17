using UnityEngine;

public class TouchBellhop : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref Bellhop selectedBellhop)
    {
        selectedGuest = null;
        selectedBellhop = collider.GetComponent<Bellhop>();
    }
}
