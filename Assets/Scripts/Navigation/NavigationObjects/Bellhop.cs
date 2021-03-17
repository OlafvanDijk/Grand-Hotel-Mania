using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Bellhop : NavigationObject
{
    [Header("Bellhop Variables")]
    [Tooltip("Reference to the navigator so the Bellhop can walk around as much as he wants")]
    [SerializeField] private Navigator navigator;
    [Tooltip("Location where the Bellhop will stand when he has nothing to do")]
    [SerializeField] private NavigationPoint start;
    [Tooltip("Max number of interactions to put in the Queue")]
    [SerializeField] private int maxQueue;
    [Tooltip("Hands that hold the items of the Bellhop")]
    [SerializeField] private List<Hand> hands;

    [Header("Queue Text")]
    [Tooltip("Prefab of the Queue Text Object")]
    [SerializeField] private GameObject textPrefab;
    [Tooltip("GameObject that will hold all the text objects")]
    [SerializeField] private Transform textParent;

    [HideInInspector]
    public ItemManager itemManager;

    private bool busy = false;
    private List<NavigationInteraction> interactionQueue;
    private Dictionary<NavigationInteraction, GameObject> interactionQueueText;

    #region Unity Mehtods
    /// <summary>
    /// Create list and Add Listeners to the events
    /// </summary>
    private void Start()
    {
        itemManager = new ItemManager(hands);
        interactionQueue = new List<NavigationInteraction>();
        interactionQueueText = new Dictionary<NavigationInteraction, GameObject>();
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
    #endregion

    #region Public Methods
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
    #endregion

    #region Private Methods
    /// <summary>
    /// Remove interaction from Queue and Update the UI
    /// </summary>
    private void ArrivedAtInteraction()
    {
        if (interactionQueue.Count > 0)
        {
            interactionQueue.RemoveAt(0);
        }
        UpdateUI();
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

    /// <summary>
    /// Updates the Queue UI on screen
    /// </summary>
    private void UpdateUI()
    {
        RemoveInteractionText();
        SetInteractionText();
    }

    /// <summary>
    /// Removes all Interaction text objects that are no longer needed
    /// </summary>
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

    /// <summary>
    /// Change Interaction Queue text to match the order of the interaction in the queue
    /// </summary>
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
    #endregion
}