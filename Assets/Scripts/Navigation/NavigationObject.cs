using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NavigationObject : MonoBehaviour
{
    [Header("Navigation")]
    [Tooltip("Movementspeed of the object")]
    [SerializeField] private float movementSpeed;

    [Header("Navigation Events")]
    public UnityEvent Interacted;
    public UnityEvent Arrived;

    [HideInInspector]
    public Vector2 currentPosition;
    
    protected List<Vector2> positions;

    private Transform objTransform;
    private bool canMove = true;

    private NavigationInteraction navigationInteraction;

    /// <summary>
    /// Set Transform of the object
    /// </summary>
    private void Awake()
    {
        objTransform = this.transform;
    }

    /// <summary>
    /// Move the object to the first available position
    /// If the object has reached the last position then the Interaciton wil start
    /// </summary>
    private void Update()
    {
        if (canMove && positions != null && positions.Count > 0)
        {
            if (objTransform.position.x == positions[0].x && objTransform.position.y == positions[0].y)
            {
                currentPosition = positions[0];

                positions.RemoveAt(0);
                if (positions.Count <= 0)
                {
                    Arrived.Invoke();
                    if (navigationInteraction)
                    {
                        navigationInteraction.NavInteract(this.gameObject);
                    }
                    return;
                }
            }

            float step = movementSpeed * Time.deltaTime;
            objTransform.position = Vector2.MoveTowards(transform.position, positions[0], step);
        }
    }

    /// <summary>
    /// Make the guest walk towards the given positions 
    /// Interact with the given interaction when the last position has been reached
    /// </summary>
    /// <param name="positions">List of positions to move towards</param>
    /// <param name="interaction">Interaciton to trigger</param>
    public void SetRoute(List<Vector2> positions, NavigationInteraction interaction)
    {
        this.positions = positions;
        navigationInteraction = interaction;
    }

    public void StopFromMoving()
    {
        canMove = false;
        //Here is where you should stop the animator if there is one
    }
}
