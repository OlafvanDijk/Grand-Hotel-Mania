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

    public Navigator navigator { get; private set; }
   
    private Collider2D collider;
    private NavigationInteraction exit;
    private GuestSpawner guestSpawner;
    private SpriteRenderer spriteRenderer;

    #region Unity Methods
    /// <summary>
    /// Set Sprite of the Guest.
    /// </summary>
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        SetSprite();
    }

    /// <summary>
    /// Inform the guest spawner that this guest has left the hotel.
    /// </summary>
    private void OnDestroy()
    {
        guestSpawner.GuestLeft(this);
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Set some variable references that the guest needs at the beginning of spawning.
    /// This method is called by the spawner.
    /// </summary>
    /// <param name="navigator">Reference to the navigator script.</param>
    /// <param name="exit">NavigationInteraction where the guest should exit.</param>
    /// <param name="guestSpawner">Reference to the spawner of the guests.</param>
    public void InitializedGuest(Navigator navigator, NavigationInteraction exit, GuestSpawner guestSpawner)
    {
        this.navigator = navigator;
        this.exit = exit;
        this.guestSpawner = guestSpawner;
    }

    /// <summary>
    /// Disable and Enable the Collider and CheckInBubble.
    /// </summary>
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

    /// <summary>
    /// Disable and Enable the Collider and CheckOutBubble.
    /// Also sends the guest to the exit after having checked out.
    /// </summary>
    public void CheckOut()
    {
        if (checkOut)
        {
            SetColliderAndBubble(true, checkOutBubble, true);
        }
        else
        {
            SetColliderAndBubble(false, checkOutBubble, false);
            List<Vector2> route = navigator.GetRoute(currentPosition, exit.navigationPoint);
            SetRoute(route, exit);
        }
    }

    /// <summary>
    /// Method to let the TouchInteraction check if there is a route.
    /// </summary>
    /// <param name="currentPosition">Current Position of the guest.</param>
    /// <param name="navigationPoint">NavigationPoint at the end of the route.</param>
    /// <returns></returns>
    public List<Vector2> GetRoute(Vector2 currentPosition, NavigationPoint navigationPoint)
    {
        return navigator.GetRoute(currentPosition, navigationPoint);
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Set the sprite of the guest.
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

    /// <summary>
    /// Enable or disable the collider and bubble based on the given values.
    /// </summary>
    /// <param name="enableCollider">True if the collider should be enabled.</param>
    /// <param name="bubble">GameObject of the bubble.</param>
    /// <param name="bubbleEnabled">True if the bubble should be enabled.</param>
    private void SetColliderAndBubble(bool enableCollider, GameObject bubble, bool bubbleEnabled)
    {
        collider.enabled = enableCollider;
        bubble.SetActive(bubbleEnabled);
    }
    #endregion
}
