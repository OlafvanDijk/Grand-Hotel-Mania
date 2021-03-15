using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Guest : NavigationObject
{
    [Header("Guest")]
    [Tooltip("Different colors for the guests.")]
    [SerializeField] private List<Color> colors;

    [HideInInspector]
    public bool CheckIn = true;
    [HideInInspector]
    public bool CheckOut = false;

    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Set Sprite of the Guest
    /// </summary>
    private void Start()
    {
        SetSprite();
    }

    /// <summary>
    /// Set the sprite of the guest
    /// </summary>
    private void SetSprite()
    {
        if (colors.Count > 0)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            int index = Random.Range(0, colors.Count);
            spriteRenderer.color = colors[index];
        }
    }
}
