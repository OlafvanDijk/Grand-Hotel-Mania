using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Objective
{
    public int amount;
    public ObjectiveType objectiveType;
}

public enum ObjectiveType
{
    Keys,
    Gold
}