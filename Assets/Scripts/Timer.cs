using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(LevelManager))]
public class Timer : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Textfield that shows the minutes and seconds left of the timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    [Tooltip("Clock image that displays how much time has passed")]
    [SerializeField] private Image clockImage;

    private float time;
    private float timeRemaining;
    private bool timerIsRunning = false;

    private LevelManager levelManager;

    #region Unity Methods
    /// <summary>
    /// Get the time for the current level and set local variables.
    /// </summary>
    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
        time = levelManager.currentLevel.timeInSeconds;
        timeRemaining = time;
        UpdateTimer();
    }

    /// <summary>
    /// Update timer + UI clock if timer is running.
    /// </summary>
    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimer();
            }
            else
            {
                levelManager.EndGame();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Start the timer.
    /// </summary>
    public void StartTimer()
    {
        timerIsRunning = true;
    }

    /// <summary>
    /// Stop the timer
    /// This method does not reset the timer.
    /// </summary>
    public void StopTimer()
    {
        timerIsRunning = false;
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Updates the timer on the screen.
    /// This includes setting a text field and the fillamount of the clock image.
    /// </summary>
    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);

            if (timerText)
            {
                timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
            }

            clockImage.fillAmount = (timeRemaining / time);
        }
        else
        {
            timerText.text = "0:00";
            clockImage.fillAmount = 0;
        }
    }
    #endregion
}
