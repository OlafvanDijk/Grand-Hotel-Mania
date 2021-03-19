using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionValues", menuName = "ScriptableObjects/Upgrades/Create Action Values Object", order = 1)]
public class ActionValues : ScriptableObject
{
    [Header("Costs")]
    [Tooltip("Cost for checking in.")]
    public int checkInCost;
    [Tooltip("Cost for checking out.")]
    public int checkOutCost;
    [Tooltip("Cost for coffee.")]
    public int coffeeCost;
}
