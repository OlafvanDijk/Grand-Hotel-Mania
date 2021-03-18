using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("ScriptableObject with a list that contains all the playable levels")]
    [SerializeField] private LevelList levelList;
    [Tooltip("Seconds you'll have to wait before the game actually starts")]
    [SerializeField] private float delayBeforeStart;

    [Header("Events")]
    public UnityEvent StartGame;
    public UnityEvent GameOver;

    [HideInInspector]
    public Level currentLevel;

    /// <summary>
    /// Set current level and set the Objective UI
    /// </summary>
    private void Awake()
    {
        SetCurrentLevel();
    }

    /// <summary>
    /// Invoke StartGame Event after a small delay
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartWithDelay());
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

    /// <summary>
    /// Invoke StartGame Event after a small delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeStart);
        StartGame.Invoke();
    }
}
