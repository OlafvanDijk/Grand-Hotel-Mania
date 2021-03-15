using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationObject : MonoBehaviour
{
    [Header("Navigation")]
    [Tooltip("Movementspeed of the object")]
    [SerializeField] private float movementSpeed;

    [HideInInspector]
    public Vector2 currentPosition;
    protected List<Vector2> positions;
    private Transform objTransform;
    private bool canMove = true;

    //TODO Object to interact with

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
                if (positions.Count == 1)
                    currentPosition = positions[0];

                positions.RemoveAt(0);
                if (positions.Count <= 0)
                {
                    //TODO Interact With object
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
    /// <param name="obj">Interaciton to trigger</param>
    public void SetRoute(List<Vector2> positions)
    {
        this.positions = positions;
    }

    public void StopFromMoving()
    {
        canMove = false;
        //Here is where you should stop the animator if there is one
    }

    /// <summary>
    /// Set current position of the Navigation object
    /// To use the navigator this position should be a positon of a NavigationPoint
    /// </summary>
    /// <param name="position"></param>
    public void SetCurrentPosition(Vector2 position)
    {
        currentPosition = position;
    }
}
