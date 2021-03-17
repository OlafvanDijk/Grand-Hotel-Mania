using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchInput : MonoBehaviour
{
    [SerializeField] private Bellhop bellhop;
    [SerializeField] private List<TouchInteractionTag> correspondingInteractions;

    private Guest selectedGuest;
    

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = GetTouch();
        if (hit.collider)
        {
            foreach (TouchInteractionTag touchInteractionTag in correspondingInteractions)
            {
                if (touchInteractionTag.tag.Equals(hit.collider.tag))
                {
                    touchInteractionTag.touchInteraction.TouchInteract(hit.collider, ref selectedGuest, ref bellhop);
                    return;
                }
            }
        }
    }

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