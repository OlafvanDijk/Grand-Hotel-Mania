using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_#", menuName = "ScriptableObjects/Create Level", order = 1)]
public class Level : ScriptableObject
{
    [Range(0, 50)]
    public int amountOfGuests = 3;
    public Objective objective;
}
