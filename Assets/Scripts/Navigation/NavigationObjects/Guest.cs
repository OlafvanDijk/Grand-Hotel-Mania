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
    private NavigationInteraction exit;
    private Navigator navigator;
    
    /// <summary>
    /// Set Sprite of the Guest
    /// </summary>
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        SetSprite();
    }

    public void InitializedGuest(Navigator navigator, NavigationInteraction exit)
    {
        this.navigator = navigator;
        this.exit = exit;
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
            List<Vector2> route = navigator.GetRoute(currentPosition, exit.navigationPoint);
            SetRoute(route, exit);
        }
    }

    public void SetCollider(bool enable)
    {
        collider.enabled = enable;
    }

    public List<Vector2> GetRoute(Vector2 currentPosition, NavigationPoint navigationPoint)
    {
        return navigator.GetRoute(currentPosition, navigationPoint);
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

    private void SetColliderAndBubble(bool enableCollider, GameObject bubble, bool bubbleEnabled)
    {
        SetCollider(enableCollider);
        bubble.SetActive(bubbleEnabled);
    }
}
