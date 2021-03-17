﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Room : NavigationInteraction
{
    [HideInInspector]
    public bool availableToGuests = true;

    [SerializeField] private GameObject closedDoor;
    [SerializeField] private GameObject cleaningSign;
    [SerializeField] private NavigationInteraction desk;

    [HideInInspector]
    public bool shouldClean = false;

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
        //TODO CLEAN ROOM COROUTINE
        Bellhop bellhop = gameObject.GetComponent<Bellhop>();
        StartCoroutine(CleanRoom(bellhop));
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
        bellhop.Interacted.Invoke();
    }
}
