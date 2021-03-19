using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("ScriptableObject with a list that contains all the playable levels.")]
    [SerializeField] private LevelList levelList;
    [Tooltip("Seconds you'll have to wait before the game actually starts.")]
    [SerializeField] private float delayBeforeStart;

    [Header("Events")]
    public UnityEvent StartGame;
    public UnityEvent PauseGame;
    public UnityEvent UnPauseGame;
    public UnityEvent GameOver;
    public UnityEvent LevelCompleted;

    [HideInInspector]
    public Level currentLevel { get; private set; }

    private bool objectiveReached = false;

    #region Unity Methods
    /// <summary>
    /// Set current level and set the Objective UI.
    /// </summary>
    private void Awake()
    {
        SetCurrentLevel();
    }

    /// <summary>
    /// Invoke StartGame Event after a small delay.
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartWithDelay());
    }
    #endregion

    #region Public Methods
    #region GameState Methods
    /// <summary>
    /// Set the timescale to 0.
    /// This method is called when the PauseGame is invoked.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Set the timescale back to 1.
    /// This method is called when the UnPauseGame is invoked.
    /// </summary>
    public void UnPause()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Ends the game. LevelCompleted or GameOver will be invoke based on the boolean objectiveReached.
    /// </summary>
    public void EndGame()
    {
        if (objectiveReached)
        {
            LevelCompleted.Invoke();
        }
        else
        {
            GameOver.Invoke();
        }
    }
    #endregion

    /// <summary>
    /// Changes the state of objectiveReached with the given value.
    /// </summary>
    /// <param name="objectiveReached">Has the objective been reached?</param>
    public void ObjectiveReached(bool objectiveReached)
    {
        this.objectiveReached = objectiveReached;
    }

    /// <summary>
    /// Loads or reloads the level based on the objectiveReached boolean.
    /// Sets TimeScale to 1.
    /// </summary>
    public void LoadLevel()
    {
        int index = PlayerPrefs.GetInt("Level");
        if (objectiveReached)
        {
            PlayerPrefs.SetInt("Level", index + 1);
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Get the current level index from the player prefs.
    /// Destory this gameobject if GetLevelAtIndex returns null.
    /// </summary>
    private void SetCurrentLevel()
    {
        int levelIndex = PlayerPrefs.GetInt("Level");
        currentLevel = levelList.GetLevelAtIndex(levelIndex);

        if (!currentLevel)
            Destroy(this.gameObject);
    }
    #endregion

    #region IEnumerators
    /// <summary>
    /// Invoke StartGame Event after a small delay.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        StartGame.Invoke();
    }
    #endregion
}
