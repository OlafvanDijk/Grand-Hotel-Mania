using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPoint : MonoBehaviour
{
    public List<NavigationPoint> nodes;

    [HideInInspector]
    public Vector2 position;

    private void Awake()
    {
        position = transform.position;
    }
}
