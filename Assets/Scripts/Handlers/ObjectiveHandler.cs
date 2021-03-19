using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveHandler : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private TextMeshProUGUI currentProgressText;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private Image objectiveImage;
    [SerializeField] private Slider progressBar;

    private ObjectiveObject currentObjective;
    private int objectiveProgress;
    private bool objectiveReached = false;

    private void Start()
    {
        currentObjective = levelManager.currentLevel.objective;
        objectiveText.text = currentObjective.amount.ToString();
        objectiveImage.sprite = currentObjective.objective.sprite;
        objectiveImage.color = new Color(1,1,1,255);
        progressBar.maxValue = currentObjective.amount;
    }

    public void AddObjectiveAmount(int amount, ObjectiveType objectiveType)
    {
        ObjectiveType currentObjectiveType = currentObjective.objective.objectiveType;
        if (currentObjectiveType.Equals(objectiveType))
        {
            objectiveProgress += amount;
        }

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
