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
    public Level currentLevel;

    private bool objectiveReached = false;

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

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

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

    public void ObjectiveReached(bool objectiveReached)
    {
        this.objectiveReached = objectiveReached;
    }

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

    /// <summary>
    /// Invoke StartGame Event after a small delay.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        StartGame.Invoke();
    }
}
