using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelList levelList;

    [HideInInspector]
    public Level currentLevel;

    /// <summary>
    /// Set current level and set the Objective UI
    /// </summary>
    private void Awake()
    {
        SetCurrentLevel();

        //TODO SET UI (Objective)
    }

    /// <summary>
    /// Get the current level index from the player prefs
    /// Destory this gameobject if GetLevelAtIndex returns null
    /// </summary>
    private void SetCurrentLevel()
    {
        int levelIndex = PlayerPrefs.GetInt("Level");
        currentLevel = levelList.GetLevelAtIndex(levelIndex);

        if (!currentLevel)
            Destroy(this.gameObject);
    }
}
