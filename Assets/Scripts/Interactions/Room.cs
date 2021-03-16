using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Room : NavigationInteraction
{
    [HideInInspector]
    public bool availableToGuests = true;

    [SerializeField] private GameObject closedDoor;
    [SerializeField] private NavigationPoint desk;

    private Navigator navigator;
    private Collider2D collider;
    private GameObject currentGuest;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

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
            collider.enabled = true;
            availableToGuests = true;
        }
        else
        {
            closedDoor.SetActive(true);
            collider.enabled = false;
            availableToGuests = false;
        }
    }

    public void SetNavigator(Navigator navigator)
    {
        this.navigator = navigator;
    }

    private void InteractWithGuest(GameObject gameObject)
    {
        Debug.Log("Interacting with: " + this.name);
        currentGuest = gameObject;
        currentGuest.SetActive(false);
        //TODO ASK FOR COFFEE?
        //TODO START TIMER FOR STAY
    }

    private void InteractWithBellhop(GameObject gameObject)
    {
        //TODO CHECK ROOM FOR CLEANING + CLEAN ROOM
    }

    private IEnumerator RestingGuest()
    {
        yield return new WaitForSecondsRealtime(4f);
        DoorState(true);
        Guest guest = currentGuest.GetComponent<Guest>();
        List<Vector2> route = navigator.GetRoute(guest.currentPosition, desk);
        guest.SetRoute(route, null);
    }
}
