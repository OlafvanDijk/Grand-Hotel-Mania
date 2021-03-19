using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [Tooltip("LevelManager is used to pause and unpause the game")]
    [SerializeField] private LevelManager levelManager;

    public void Pause()
    {
        levelManager.PauseGame.Invoke();
    }

    public void UnPause()
    {
        levelManager.UnPauseGame.Invoke();
    }
}
