using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelList", menuName = "ScriptableObjects/Level/Create Level List", order = 1)]
public class LevelList : ScriptableObject
{
    [Tooltip("List of all the playable levels")]
    [SerializeField] private List<Level> levels;

    /// <summary>
    /// Get the level at the given index
    /// returns null if no level at that index was found
    /// </summary>
    /// <param name="index">Index of the level in the list</param>
    /// <returns>Level at the given index or null if nothing can be found</returns>
    public Level GetLevelAtIndex(int index)
    {
        try
        {
            return levels[index];
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message, this);

            if (levels.Count > 0)
                return levels[levels.Count - 1];

            Debug.Log("There are no levels in the Level List");
            return null;
        }
    }
}
