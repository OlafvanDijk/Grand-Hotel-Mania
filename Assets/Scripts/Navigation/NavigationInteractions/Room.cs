using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : NavigationInteraction
{
    [HideInInspector]
    public bool availableToGuests = true;

    [SerializeField] private GameObject closedDoor;
    [SerializeField] private GameObject cleaningSign;
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

    private void InteractWithGuest(GameObject gameObject)
    {
        currentGuest = gameObject;
        currentGuest.SetActive(false);
        StartCoroutine(RestingGuest());
    }

    private void InteractWithBellhop(GameObject gameObject)
    {
        Bellhop bellhop = gameObject.GetComponent<Bellhop>();
        if (bellhop.HasItem(ItemType.CleaningSupplies))
        {
            StartCoroutine(CleanRoom(bellhop));
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
        yield return new WaitForSecondsRealtime(4f);
        SendGuestToDesk();
        DoorState(true);
        ShouldClean(true, true);
        Debug.Log(this.gameObject.name + " should be cleaned");
    }

    private IEnumerator CleanRoom(Bellhop bellhop)
    {
        yield return new WaitForSecondsRealtime(2f);
        ShouldClean(false, false);
        availableToGuests = true;
        bellhop.RemoveItemFromHands(ItemType.CleaningSupplies);
        bellhop.Interacted.Invoke();
    }
}
