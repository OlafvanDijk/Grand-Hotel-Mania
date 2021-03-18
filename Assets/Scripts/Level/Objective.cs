using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Objective_####", menuName = "ScriptableObjects/Objective/Create Objective", order = 1)]
public class Objective : ScriptableObject
{
    public ObjectiveType objectiveType;
    public Sprite sprite;
}

[Serializable]
public class ObjectiveObject
{
    public int amount;
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