using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Objective_####", menuName = "ScriptableObjects/Objective/Create Objective", order = 1)]
public class Objective : ScriptableObject
{
    [Tooltip("Type of Objective.")]
    public ObjectiveType objectiveType;
    [Tooltip("Sprite matching the objective.")]
    public Sprite sprite;
}

[Serializable]
public class ObjectiveObject
{
    [Tooltip("Amount to reach.")]
    public int amount;
    [Tooltip("Scriptable object of the objective.")]
    public Objective objective;

    public ObjectiveObject(int amount, ObjectiveType objectiveType)
    {
        this.amount = amount;
        this.objective.objectiveType = objectiveType;
    }
}

public enum ObjectiveType
{
    Keys,
    Money
}