using UnityEngine;

public class TouchSupply : TouchInteraction
{
    public override void TouchInteract(Collider2D collider, ref Guest selectedGuest, ref GameObject selectedBellhop, Navigator navigator)
    {
        //Collider = supply
        //if (selectedBellhop)
        //{
        //    NavigationPoint supply = collider.GetComponent<NavigationPoint>();
        //    Vector2 position = selectedBellhop.position;
        //    List<Vector2> navigator.GetRoute(position, supply);
        //    selectedBellhop.SetRoute(route);
        //}
        throw new System.NotImplementedException();
    }
}
