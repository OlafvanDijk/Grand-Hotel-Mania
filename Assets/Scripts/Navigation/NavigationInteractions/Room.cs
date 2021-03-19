using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : NavigationInteraction
{
    [Header("RoomState Gameobjects")]
    [Tooltip("GameObject to enable when closing the door.")]
    [SerializeField] private GameObject closedDoor;
    [Tooltip("GameObject to enable when the room needs cleaning.")]
    [SerializeField] private GameObject cleaningSign;
    [Tooltip("GameObject to enable when the room is highlighted.")]
    [SerializeField] private GameObject highlightObject;

    [Header("Navigation")]
    [Tooltip("NavigationInteraction to send the guest to after having rested in the room.")]
    [SerializeField] private NavigationInteraction desk;

    [HideInInspector]
    public bool availableToGuests = true;
    [HideInInspector]
    public bool shouldClean = false;

    private GameObject currentGuest;

    #region Public Methods
    /// <summary>
    /// Checks what kind of interaction is taking place.
    /// </summary>
    /// <param name="gameObject">GameObject Interacting with the NavigationInteraction.</param>
    public override void NavInteract(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "Guest":
                InteractWithGuest(gameObject);
                break;
            case "Bellhop":
                InteractWithBellhop(gameObject);
                break;
        }
    }

    #region Room State Methods
    /// <summary>
    /// Opens or closes the door based on the given value.
    /// </summary>
    /// <param name="open">True if the door should be open.</param>
    public void DoorState(bool open)
    {
        if (open)
        {
            closedDoor.SetActive(false);
            availableToGuests = true;
        }
        else
        {
            closedDoor.SetActive(true);
            availableToGuests = false;
        }
    }

    /// <summary>
    /// Set shouldclean and cleaningsign based on the given value.
    /// </summary>
    /// <param name="shouldClean">True if the room should be cleaned.</param>
    public void ShouldClean(bool shouldClean)
    {
        this.shouldClean = shouldClean;
        cleaningSign.SetActive(shouldClean);
    }

    /// <summary>
    /// Highlights the room based on the given value and based on if the room is available to guests.
    /// </summary>
    /// <param name="highlight">True if the room should be highlighted.</param>
    public void HighlightRoom(bool highlight)
    {
        if (highlight)
        {
            highlight = availableToGuests;
        }
        highlightObject.SetActive(highlight);
    }
    #endregion
    #endregion

    #region Private Methods
    /// <summary>
    /// Let the guest rest in the room.
    /// </summary>
    /// <param name="gameObject">Guests' GameObject</param>
    private void InteractWithGuest(GameObject gameObject)
    {
        currentGuest = gameObject;
        currentGuest.SetActive(false);
        StartCoroutine(RestingGuest());
    }

    /// <summary>
    /// Interacts with Bellhop. If the Bellhop has cleaning supplies and the room has to be cleaned then the Coroutine CleanRoom is called.
    /// </summary>
    /// <param name="gameObject">Bellhop's GameObject.</param>
    private void InteractWithBellhop(GameObject gameObject)
    {
        Bellhop bellhop = gameObject.GetComponent<Bellhop>();
        if (bellhop.itemManager.HasItem(ItemType.CleaningSupplies) && shouldClean)
        {
            StartCoroutine(CleanRoom(bellhop));
        }
        else
        {
            bellhop.Interacted.Invoke();
        }
    }

    /// <summary>
    /// Sends guests back to the desk.
    /// </summary>
    private void SendGuestToDesk()
    {
        currentGuest.SetActive(true);
        Guest guest = currentGuest.GetComponent<Guest>();
        guest.checkOut = true;
        guest.currentPosition = navigationPoint.position;
        List<Vector2> route = guest.GetRoute(guest.currentPosition, desk.navigationPoint);
        guest.SetRoute(route, desk);
        guest.Interacted.Invoke();
    }
    #endregion

    #region IEnumerators
    /// <summary>
    /// Sends guest to the desk after waiting a certain amount of time.
    /// Changes the roomstate from clean to should be cleaned.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RestingGuest()
    {
        yield return new WaitForSeconds(4f);
        SendGuestToDesk();
        DoorState(true);
        ShouldClean(true);
    }

    /// <summary>
    /// Cleans the room after having waited a certain amount of time.
    /// Removes Cleaning Supply from the Bellhop.
    /// </summary>
    /// <param name="bellhop">Bellhop who cleans the room.</param>
    /// <returns></returns>
    private IEnumerator CleanRoom(Bellhop bellhop)
    {
        yield return new WaitForSeconds(2f);
        ShouldClean(false);
        availableToGuests = true;
        bellhop.itemManager.RemoveItemFromHands(ItemType.CleaningSupplies);
        bellhop.Interacted.Invoke();
    }
    #endregion
}
