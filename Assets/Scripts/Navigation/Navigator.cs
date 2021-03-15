using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Navigator : MonoBehaviour
{
    [SerializeField] private List<NavigationPoint> navigationPoints;

    public List<Vector2> GetRoute(Vector2 from, NavigationPoint to)
    {
        NavigationPoint currentNode = navigationPoints.Find(point => point.position == from);
        List<NavigationPoint> route = GetRoute(currentNode, to);
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

    public List<NavigationPoint> GetRoute(NavigationPoint currentNode, NavigationPoint to, List<NavigationPoint> currentRoute = null)
    {
        //If goal is in neighbours return list
        if (currentNode.nodes.Contains(to))
        {
            currentRoute = AddToList(to, currentRoute);
            return currentRoute;
        }

        //Search nodes for best route
        List<NavigationPoint> bestRoute = new List<NavigationPoint>();
        foreach (NavigationPoint neighbourNode in currentNode.nodes)
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
            route = GetRoute(neighbourNode, to, route);
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

    private List<NavigationPoint> AddToList(NavigationPoint node, List<NavigationPoint> currentRoute)
    {
        if (currentRoute == null)
        {
            currentRoute = new List<NavigationPoint>();
        }
        currentRoute.Add(node);
        return currentRoute;
    }
}
