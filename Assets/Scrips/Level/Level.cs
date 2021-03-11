using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_#", menuName = "ScriptableObjects/Level/Create Level", order = 2)]
public class Level : ScriptableObject
{
    [Range(0, 50)]
    public int amountOfGuests = 3;
    public float timeInSeconds = 180f;
    public Vector2 minMaxTimeNextGuest;
    public Objective objective;
}
