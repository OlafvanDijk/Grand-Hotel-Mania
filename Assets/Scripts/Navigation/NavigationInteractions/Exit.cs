using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : NavigationInteraction
{
    /// <summary>
    /// Destroys the given GameObject.
    /// </summary>
    /// <param name="gameObject">GameObject Interacting with the NavigationInteraction.</param>
    public override void NavInteract(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
