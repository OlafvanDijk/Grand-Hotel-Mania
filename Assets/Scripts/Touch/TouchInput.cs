using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchInput : MonoBehaviour
{
    [SerializeField] private Bellhop bellhop;
    [SerializeField] private MoneyHandler moneyHandler;
    [SerializeField] private ObjectiveHandler objectiveHandler;
    [SerializeField] private List<TouchInteractionTag> correspondingInteractions;

    private Guest selectedGuest;
    private bool canTouch = false;

    /// <summary>
    /// Check correspondingInteractions for what script to use when touching something
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


    /// <summary>
    /// Enable or disable the touch input.
    /// UI not included.
    /// </summary>
    /// <param name="enable">True enables touch input</param>
    public void CanTouch(bool enable)
    {
        canTouch = enable;
    }

    /// <summary>
    /// Check if the user touched the screen.
    /// If so return th RaycastHit
    /// </summary>
    /// <returns></returns>
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
}

[Serializable]
public struct TouchInteractionTag
{
    public string tag;
    public TouchInteraction touchInteraction;
}