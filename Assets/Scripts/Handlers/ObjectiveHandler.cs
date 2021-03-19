using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveHandler : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Used to get the current objective and call the Objective Reached method.")]
    [SerializeField] private LevelManager levelManager;

    [Header("UI")]
    [Tooltip("Text field that displays the current progress amount.")]
    [SerializeField] private TextMeshProUGUI currentProgressText;
    [Tooltip("Text field that displays the objective goal amount.")]
    [SerializeField] private TextMeshProUGUI objectiveText;
    [Tooltip("Image that displays the objective's sprite.")]
    [SerializeField] private Image objectiveImage;
    [Tooltip("Slider that gives an indication on how far the player is to reaching the objective.")]
    [SerializeField] private Slider progressBar;

    private int objectiveProgress;
    private bool objectiveReached = false;

    private ObjectiveObject currentObjective;

    #region Unity Methods
    /// <summary>
    /// Set Current Objective and set UI to match the current objective.
    /// </summary>
    private void Start()
    {
        currentObjective = levelManager.currentLevel.objective;
        objectiveText.text = currentObjective.amount.ToString();
        objectiveImage.sprite = currentObjective.objective.sprite;
        objectiveImage.color = new Color(1,1,1,255);
        progressBar.maxValue = currentObjective.amount;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Adds the amount to the current progress if the given objectiveType matches the level objective.
    /// Also Updates the progress bar and tells the LevelManager when the Objective goals has been reached.
    /// </summary>
    /// <param name="amount">Amount to add to the objective progress.</param>
    /// <param name="objectiveType">Type of objective to add progress to.</param>
    public void AddObjectiveAmount(int amount, ObjectiveType objectiveType)
    {
        ObjectiveType currentObjectiveType = currentObjective.objective.objectiveType;
        if (currentObjectiveType.Equals(objectiveType))
        {
            objectiveProgress += amount;
            currentProgressText.text = objectiveProgress.ToString();

            if (!objectiveReached)
            {
                if (objectiveProgress < progressBar.maxValue)
                {
                    progressBar.value = objectiveProgress;
                }
                else
                {
                    objectiveReached = true;
                    progressBar.value = progressBar.maxValue;
                    levelManager.ObjectiveReached(objectiveReached);
                }
            }
        }
    }
    #endregion
}
