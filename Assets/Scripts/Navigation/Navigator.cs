using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Navigator : MonoBehaviour
{
    [Tooltip("List of all rooms. This is used to highlight the rooms that are available.")]
    [SerializeField] private List<Room> rooms;

    private List<NavigationPoint> navigationPoints;

    /// <summary>
    /// Get all NavigationPoints in children.
    /// </summary>
    private void Awake()
    {
        navigationPoints = gameObject.GetComponentsInChildren<NavigationPoint>().ToList();
    }

    /// <summary>
    /// Get route from the current Vector2 position to the given navigation point.
    /// </summary>
    /// <param name="from">Current Position. This should match an existing NavigationPoint.</param>
    /// <param name="goal">NavigationPoint you want to travel towards.</param>
    /// <returns>List of Vector2 positions to your goal. Returns null if no route was found.</returns>
    public List<Vector2> GetRoute(Vector2 from, NavigationPoint goal)
    {
        NavigationPoint currentNode = navigationPoints.Find(point => point.position == from);
        List<NavigationPoint> route = GetRoute(currentNode, goal);
        if (route != null && route.Count > 0)
        {
            List<Vector2> routePositions = new List<Vector2>();
            for (int i = 0; i < route.Count; i++)
            {
                routePositions.Add(route[i].position);
            }
            return routePositions;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Get the route from the current node to the given end Point.
    /// This is a recursive method so for the first time you won't have to give a currentroute.
    /// </summary>
    /// <param name="currentPoint">Current NavigationPoint.</param>
    /// <param name="goal">End Point you want to travel to.</param>
    /// <param name="currentRoute">Route to add to. Leave this blank if you don't know what you are doing.</param>
    /// <returns>Route from the current point to the end point. Returns null when no route was found.</returns>
    public List<NavigationPoint> GetRoute(NavigationPoint currentPoint, NavigationPoint goal, List<NavigationPoint> currentRoute = null)
    {
        //If goal is in neighbours return list
        if (currentPoint.nodes.Contains(goal))
        {
            currentRoute = AddToList(goal, currentRoute);
            return currentRoute;
        }

        //Search nodes for best route
        List<NavigationPoint> bestRoute = new List<NavigationPoint>();
        foreach (NavigationPoint neighbourNode in currentPoint.nodes)
        {
            List<NavigationPoint> route = new List<NavigationPoint>();

            if (currentRoute != null)
            {
                if (currentRoute.Contains(neighbourNode))
                {
                    continue;
                }
                else
                {
                    route.AddRange(currentRoute);
                }
            }                

            route = AddToList(neighbourNode, route);
            route = GetRoute(neighbourNode, goal, route);
            if (route != null)
            {
                if (bestRoute.Count == 0 || route.Count < bestRoute.Count)
                {
                    bestRoute = route;
                }
            }
        }

        if (bestRoute.Count > 0)
        {
            return bestRoute;
        }

        return null;
    }

    public void HighlightRooms(bool highlight)
    {
        foreach (Room room in rooms)
        {
            room.HighlightRoom(highlight);
        }
    }

    /// <summary>
    /// Adds given node to list.
    /// Creates list if given list was null.
    /// </summary>
    /// <param name="point">Node to add to the list.</param>
    /// <param name="currentRoute">Current RouteList.</param>
    /// <returns>NavigationPoint list with added node.</returns>
    private List<NavigationPoint> AddToList(NavigationPoint point, List<NavigationPoint> currentRoute)
    {
        if (currentRoute == null)
        {
            currentRoute = new List<NavigationPoint>();
        }
        currentRoute.Add(point);
        return currentRoute;
    }
}
