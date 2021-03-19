using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private ActionValues actionValues;
    [SerializeField] private ObjectiveHandler objectiveHandler;

    [SerializeField] private TextMeshProUGUI moneyMadeText;
    [SerializeField] private TextMeshProUGUI moneyOwnedText;

    public int levelMoney;

    public void CheckIn()
    {
        AddMoney(actionValues.checkInCost);
    }

    public void CheckOut()
    {
        AddMoney(actionValues.checkOutCost);
    }

    public void BuyCoffee()
    {
        AddMoney(actionValues.coffeeCost);
    }

    public void LevelCompleted()
    {
        int ownedMoney = PlayerPrefs.GetInt("Money");
        ownedMoney += levelMoney;
        PlayerPrefs.SetInt("Money", ownedMoney);

        moneyMadeText.text = levelMoney.ToString();
        moneyOwnedText.text = ownedMoney.ToString();
    }

    private void AddMoney(int amount)
    {
        levelMoney += amount;
        objectiveHandler.AddObjectiveAmount(amount, ObjectiveType.Money);
    }
}
