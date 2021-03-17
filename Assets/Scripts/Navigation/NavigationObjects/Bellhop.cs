using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellhop : NavigationObject
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private NavigationPoint start;

    private List<NavigationInteraction> interactionQueue;
    private bool busy = false;

    /// <summary>
    /// Create list and Add Listeners to the events
    /// </summary>
    private void Start()
    {
        interactionQueue = new List<NavigationInteraction>();
        Interacted.AddListener(PerformInteraction);
        Arrived.AddListener(UpdateUI);
        currentPosition = start.position;
    }

    /// <summary>
    /// Remove listeners
    /// </summary>
    private void OnDestroy()
    {
        Interacted.RemoveListener(PerformInteraction);
        Arrived.RemoveListener(UpdateUI);
    }

    /// <summary>
    /// Add an Interaction to the queue
    /// If the bellhop isn't busy then the first task will be performed
    /// </summary>
    /// <param name="navigationInteraction">Interaction to add</param>
    public void AddInteractionToQueue(NavigationInteraction navigationInteraction)
    {
        if (navigationInteraction)
        {
            interactionQueue.Add(navigationInteraction);
            UpdateUI();
        }

        if(!busy)
        {
            PerformInteraction();
        }
    }

    /// <summary>
    /// Perform the next available Interaction
    /// When no Interactions are available then busy gets set to false
    /// </summary>
    private void PerformInteraction()
    {
        NavigationInteraction navigationInteraction = null;
        NavigationPoint navigationPoint = null;

        if (interactionQueue.Count > 0)
        {
            busy = true;
            navigationInteraction = interactionQueue[0];
            navigationPoint = navigationInteraction.navigationPoint;
            interactionQueue.RemoveAt(0);
        }
        else
        {
            busy = false;
            navigationPoint = start;
        }

        Vector2 position = currentPosition;
        List<Vector2> route = navigator.GetRoute(position, navigationPoint);
        SetRoute(route, navigationInteraction);
    }

    /// <summary>
    /// Updates the Queue UI on screen
    /// </summary>
    private void UpdateUI()
    {
        //TODO make and update UI
        Debug.Log("Update UI");
    }

}
