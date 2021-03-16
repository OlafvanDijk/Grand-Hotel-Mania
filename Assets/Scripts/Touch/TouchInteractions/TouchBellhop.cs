using UnityEngine;

public class TouchBellhop : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref GameObject selectedBellhop, Navigator navigator)
    {
        selectedGuest = null;
        selectedBellhop = collider.gameObject;
    }
}
