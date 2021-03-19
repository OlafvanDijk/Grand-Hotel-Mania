using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_#", menuName = "ScriptableObjects/Level/Create Level", order = 2)]
public class Level : ScriptableObject
{
    [Header("Level")]
    [Tooltip("Time the Timer will countdown from.")]
    public float timeInSeconds = 180f;
    [Tooltip("Objective of the Level.")]
    public ObjectiveObject objective;

    [Header("Guests")]
    [Tooltip("Amount of Guests that will spawn.")]
    public int amountOfGuests = 3;
    [Tooltip("Min and Max time in between spawning the next Guest.")]
    public Vector2 minMaxTimeNextGuest;
}
