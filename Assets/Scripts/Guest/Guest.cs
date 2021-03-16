using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Guest : NavigationObject
{
    [Header("Guest")]
    [Tooltip("Bubble showing the guest wants to check in")]
    [SerializeField] private GameObject checkInBubble;
    [Tooltip("Bubble showing the guest wants to check out")]
    [SerializeField] private GameObject checkOutBubble;
    [Tooltip("Different colors for the guests.")]
    [SerializeField] private List<Color> colors;

    [HideInInspector]
    public bool checkIn = true;
    [HideInInspector]
    public bool checkOut = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    /// <summary>
    /// Set Sprite of the Guest
    /// </summary>
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        SetSprite();
    }

    public void CheckIn()
    {
        if (checkIn)
        {
            SetColliderAndBubble(true, checkInBubble, true);
        }
        else
        {
            SetColliderAndBubble(false, checkInBubble, false);
        }
    }

    public void CheckOut()
    {
        if (checkOut)
        {
            SetColliderAndBubble(true, checkOutBubble, true);
        }
        else
        {
            SetColliderAndBubble(false, checkOutBubble, false);
            //TODO SEND TO EXIT
        }
    }    

    public void SetCollider(bool enable)
    {
        collider.enabled = enable;
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

    private void SetColliderAndBubble(bool collider, GameObject bubble, bool bubbleEnabled)
    {
        SetCollider(collider);
        bubble.SetActive(bubbleEnabled);
    }
}
