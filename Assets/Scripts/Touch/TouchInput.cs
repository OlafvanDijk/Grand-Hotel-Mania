using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchInput : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Bellhop to order around as you please.")]
    [SerializeField] private Bellhop bellhop;
    [Tooltip("MoneyHandler to handle the TouchInteractions.")]
    [SerializeField] private MoneyHandler moneyHandler;
    [Tooltip("ObjectiveHandler to handle the TouchInteractions.")]
    [SerializeField] private ObjectiveHandler objectiveHandler;
    [Tooltip("Tags with their corresponding Interaction scripts.")]
    [SerializeField] private List<TouchInteractionTag> correspondingInteractions;

    private Guest selectedGuest;
    private bool canTouch = false;

    #region Unity Methods
    /// <summary>
    /// Check correspondingInteractions for what script to use when touching something.
    /// </summary>
    private void Update()
    {
        if (canTouch)
        {
            RaycastHit2D hit = GetTouch();
            if (hit.collider)
            {
                foreach (TouchInteractionTag touchInteractionTag in correspondingInteractions)
                {
                    if (touchInteractionTag.tag.Equals(hit.collider.tag))
                    {
                        touchInteractionTag.touchInteraction.TouchInteract(hit.collider, moneyHandler, objectiveHandler, ref selectedGuest, ref bellhop);
                        return;
                    }
                }
            }
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Enable or disable the touch input.
    /// UI not included.
    /// </summary>
    /// <param name="enable">True enables touch input.</param>
    public void CanTouch(bool enable)
    {
        canTouch = enable;
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Check if the user touched the screen.
    /// If so return the RaycastHit.
    /// </summary>
    /// <returns>RaycastHit of the touch if there was a touch.</returns>
    private RaycastHit2D GetTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
                return Physics2D.Raycast(position, Vector2.zero);
            }
        }
        return new RaycastHit2D();
    }
    #endregion
}

[Serializable]
public struct TouchInteractionTag
{
    public string tag;
    public TouchInteraction touchInteraction;
}