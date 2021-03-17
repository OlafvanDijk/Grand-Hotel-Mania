using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : NavigationInteraction
{
    public override void NavInteract(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
