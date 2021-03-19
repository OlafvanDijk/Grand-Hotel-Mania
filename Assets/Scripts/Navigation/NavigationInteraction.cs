using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NavigationInteraction : MonoBehaviour
{
    [Tooltip("Corresponding NavigationPoint.")]
    public NavigationPoint navigationPoint;

    public abstract void NavInteract(GameObject gameObject);
}
