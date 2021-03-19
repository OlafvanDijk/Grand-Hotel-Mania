using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : NavigationInteraction
{
    [HideInInspector]
    public bool availableToGuests = true;

    [SerializeField] private GameObject closedDoor;
    [SerializeField] private GameObject cleaningSign;
    [SerializeField] private GameObject highlightObject;
    [SerializeField] private NavigationInteraction desk;

    [HideInInspector]
    public bool shouldClean = false;

    private GameObject currentGuest;

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

    public void ShouldClean(bool shouldClean, bool cleanSign)
    {
        this.shouldClean = shouldClean;
        cleaningSign.SetActive(shouldClean);
    }

    public void HighlightRoom(bool highlight)
    {
        if (highlight)
        {
            highlight = availableToGuests;
        }
        highlightObject.SetActive(highlight);
    }

    private void InteractWithGuest(GameObject gameObject)
    {
        currentGuest = gameObject;
        currentGuest.SetActive(false);
        StartCoroutine(RestingGuest());
    }

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

    private IEnumerator RestingGuest()
    {
        //TODO ASK FOR COFFEE?
        yield return new WaitForSeconds(4f);
        SendGuestToDesk();
        DoorState(true);
        ShouldClean(true, true);
    }

    private IEnumerator CleanRoom(Bellhop bellhop)
    {
        yield return new WaitForSeconds(2f);
        ShouldClean(false, false);
        availableToGuests = true;
        bellhop.itemManager.RemoveItemFromHands(ItemType.CleaningSupplies);
        bellhop.Interacted.Invoke();
    }
}
