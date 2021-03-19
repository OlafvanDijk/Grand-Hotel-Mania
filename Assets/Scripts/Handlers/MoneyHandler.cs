using UnityEngine;
using TMPro;

public class MoneyHandler : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Scriptable Object that contains all the costs of actions.")]
    [SerializeField] private ActionValues actionValues;
    [Tooltip("Reference to the objectiveHandler. Used for the money objective.")]
    [SerializeField] private ObjectiveHandler objectiveHandler;

    [Header("UI")]
    [Tooltip("Text field that displays the amount of money made this level.")]
    [SerializeField] private TextMeshProUGUI moneyMadeText;
    [Tooltip("Text field that displays the amount of money the Player has.")]
    [SerializeField] private TextMeshProUGUI moneyOwnedText;

    private int levelMoney;

    #region Public Methods
    #region Money Add Actions
    /// <summary>
    /// Adds CheckIn money to the current LevelMoney.
    /// </summary>
    public void CheckIn()
    {
        AddMoney(actionValues.checkInCost);
    }

    /// <summary>
    /// Adds CheckOut money to the current LevelMoney.
    /// </summary>
    public void CheckOut()
    {
        AddMoney(actionValues.checkOutCost);
    }

    /// <summary>
    /// Adds Coffee money to the current LevelMoney.
    /// </summary>
    public void BuyCoffee()
    {
        AddMoney(actionValues.coffeeCost);
    }
    #endregion

    /// <summary>
    /// Adds levelMoney to the money owned and saves it in the PlayerPrefs.
    /// Also sets the UI fields to match the level money and money owned.
    /// </summary>
    public void LevelCompleted()
    {
        int ownedMoney = PlayerPrefs.GetInt("Money");
        ownedMoney += levelMoney;
        PlayerPrefs.SetInt("Money", ownedMoney);

        moneyMadeText.text = levelMoney.ToString();
        moneyOwnedText.text = ownedMoney.ToString();
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Add money to the level money and give the same amount to the objective handler.
    /// </summary>
    /// <param name="amount">Amount of money to add.</param>
    private void AddMoney(int amount)
    {
        levelMoney += amount;
        objectiveHandler.AddObjectiveAmount(amount, ObjectiveType.Money);
    }
    #endregion
}
