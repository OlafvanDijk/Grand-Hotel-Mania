using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Bellhop : NavigationObject
{
    [Header("Bellhop Variables")]
    [SerializeField] private Navigator navigator;
    [SerializeField] private NavigationPoint start;
    [SerializeField] private int maxQueue;
    [SerializeField] private List<Hand> hands;

    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Transform textParent;

    private bool busy = false;
    private Dictionary<NavigationInteraction, GameObject> interactionQueueText;
    private List<NavigationInteraction> interactionQueue;

    /// <summary>
    /// Create list and Add Listeners to the events
    /// </summary>
    private void Start()
    {
        interactionQueueText = new Dictionary<NavigationInteraction, GameObject>();
        interactionQueue = new List<NavigationInteraction>();
        Interacted.AddListener(PerformInteraction);
        Arrived.AddListener(ArrivedAtInteraction);
        currentPosition = start.position;
    }

    /// <summary>
    /// Remove listeners
    /// </summary>
    private void OnDestroy()
    {
        Interacted.RemoveListener(PerformInteraction);
        Arrived.RemoveListener(ArrivedAtInteraction);
    }

    /// <summary>
    /// Add an Interaction to the queue
    /// If the bellhop isn't busy then the first task will be performed
    /// </summary>
    /// <param name="navigationInteraction">Interaction to add</param>
    public void AddInteractionToQueue(NavigationInteraction navigationInteraction)
    {
        if (navigationInteraction && interactionQueue.Count < maxQueue)
        {
            interactionQueue.Add(navigationInteraction);
            UpdateUI();
        }

        if (!busy)
        {
            PerformInteraction();
        }
    }

    public void AddItemToHands(ItemType itemType)
    {
        SetItemType(ItemType.None, itemType);
    }

    public void RemoveItemFromHands(ItemType itemType)
    {
        SetItemType(itemType, ItemType.None);
    }

    public bool HasItem(ItemType itemType)
    {
        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].itemType == itemType)
            {
                return true;
            }
        }
        return false;
    }

    private void SetItemType(ItemType oldType, ItemType newType)
    {
        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].itemType == oldType)
            {
                hands[i].SetItemType(newType);
                break;
            }
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
        }
        else
        {
            busy = false;
            navigationPoint = start;
        }

        List<Vector2> route = new List<Vector2>();
        if (currentPosition == navigationPoint.position)
        {
            route = new List<Vector2>() { currentPosition };
        }
        else
        {
            Vector2 position = currentPosition;
            route = navigator.GetRoute(position, navigationPoint);
        }
        SetRoute(route, navigationInteraction);
    }

    public void ArrivedAtInteraction()
    {
        if (interactionQueue.Count > 0)
        {
            string name = interactionQueue[0].name;
            interactionQueue.RemoveAt(0);
        }
        UpdateUI();
    }

    /// <summary>
    /// Updates the Queue UI on screen
    /// </summary>
    private void UpdateUI()
    {
        RemoveInteractionText();
        SetInteractionText();
    }

    private void RemoveInteractionText()
    {
        List<NavigationInteraction> toBeRemoved = new List<NavigationInteraction>();
        foreach (NavigationInteraction interaction in interactionQueueText.Keys)
        {
            if (!interactionQueue.Contains(interaction))
            {
                toBeRemoved.Add(interaction);
            }
        }

        foreach (NavigationInteraction interaction in toBeRemoved)
        {
            Destroy(interactionQueueText[interaction]);
            interactionQueueText.Remove(interaction);
        }
    }

    private void SetInteractionText()
    {
        List<NavigationInteraction> alreadyReset = new List<NavigationInteraction>();
        for (int i = 0; i < interactionQueue.Count; i++)
        {
            NavigationInteraction interaction = interactionQueue[i];
            string text = string.Empty;
            TextMeshPro tmpObject = null;

            if (!interactionQueueText.ContainsKey(interaction))
            {
                GameObject textObject = Instantiate(textPrefab, interaction.navigationPoint.position, Quaternion.Euler(Vector3.zero), textParent);
                interactionQueueText.Add(interaction, textObject);
                tmpObject = textObject.GetComponentInChildren<TextMeshPro>();
                text = "(";
            }
            else
            {
                tmpObject = interactionQueueText[interaction].GetComponentInChildren<TextMeshPro>();
                if (alreadyReset.Contains(interaction))
                {
                    text = tmpObject.text;
                    text = text.Trim(')');
                    text += "+";
                }
                else
                {
                    text = "(";
                    alreadyReset.Add(interaction);
                }
            }

            int queueCount = i + 1;
            text += queueCount + ")";
            tmpObject.text = text;
        }
    }
}