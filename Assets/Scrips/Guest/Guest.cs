using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Guest : MonoBehaviour
{
    [Tooltip("Different colors for the guests.")]
    [SerializeField] private List<Color> colors;
    [Tooltip("Movementspeed of the guest")]
    [SerializeField] private float movementSpeed;

    private SpriteRenderer spriteRenderer;

    private List<Vector2> positions;
    private Transform guestTransform;
    private bool canMove = true;

    /// <summary>
    /// Set Transform and Sprite of the Guest
    /// </summary>
    private void Awake()
    {
        guestTransform = this.transform;
        SetSprite();
    }

    /// <summary>
    /// Move the guest to the first available position
    /// If the guest has reached the last position then the Interaciton wil start
    /// </summary>
    private void Update()
    {
        if (canMove && positions != null && positions.Count > 0)
        {
            if (guestTransform.position.x == positions[0].x && guestTransform.position.y == positions[0].y)
            {
                positions.RemoveAt(0);
                if (positions.Count <= 0)
                {
                    //TODO Interact With object
                    return;
                }
            }

            float step = movementSpeed * Time.deltaTime;
            guestTransform.position = Vector2.MoveTowards(transform.position, positions[0], step);
        }
    }

    /// <summary>
    /// Make the guest walk towards the given positions 
    /// Interact with the given interaction when the last position has been reached
    /// </summary>
    /// <param name="positions">List of positions to move towards</param>
    /// <param name="obj">Interaciton to trigger</param>
    public void WalkToAndInteractWith(List<Vector2> positions, Object obj)
    {
        this.positions = positions;
    }

    public void StopGuest()
    {
        canMove = false;
        //Here is where you should stop the animator if there is one
    }

    /// <summary>
    /// Set the sprite of the guest
    /// </summary>
    private void SetSprite()
    {
        if (colors.Count > 0)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            int index = Random.Range(0, colors.Count);
            spriteRenderer.color = colors[index];
        }
    }
}
