using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionValues", menuName = "ScriptableObjects/Upgrades/Create Action Values Object", order = 1)]
public class ActionValues : ScriptableObject
{
    public int checkInCost;
    public int checkOutCost;
    public int coffeeCost;
}
