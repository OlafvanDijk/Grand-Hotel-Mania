using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPoint : MonoBehaviour
{
    [Tooltip("Neighbouring NavigationPoints")]
    public List<NavigationPoint> neighbours;

    [HideInInspector]
    public Vector2 position { get; private set; }

    #region Unity Methods
    /// <summary>
    /// Set position based on the transform.
    /// </summary>
    private void Awake()
    {
        position = transform.position;
    }
    #endregion
}
