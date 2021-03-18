using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private ActionValues actionValues;
    [SerializeField] private ObjectiveHandler objectiveHandler;

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

    private void AddMoney(int amount)
    {
        levelMoney += amount;
        objectiveHandler.AddObjectiveAmount(amount, ObjectiveType.Money);
    }
}
