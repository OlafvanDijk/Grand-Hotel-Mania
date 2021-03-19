using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [Tooltip("LevelManager is used to pause and unpause the game")]
    [SerializeField] private LevelManager levelManager;

    #region Public Methods
    /// <summary>
    /// Invoke the PauseGame Event in LevelManager
    /// This method is called by the UI Button.
    /// </summary>
    public void Pause()
    {
        levelManager.PauseGame.Invoke();
    }

    /// <summary>
    /// Invoke the UnPauseGame Event in LevelManager
    /// This method is called by the UI Button.
    /// </summary>
    public void UnPause()
    {
        levelManager.UnPauseGame.Invoke();
    }
    #endregion
}
