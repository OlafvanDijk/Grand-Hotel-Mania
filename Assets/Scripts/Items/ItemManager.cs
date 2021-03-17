using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    private List<Hand> hands;

    public ItemManager(List<Hand> hands)
    {
        this.hands = hands;
    }

    /// <summary>
    /// Adds given ItemType to hand
    /// </summary>
    /// <param name="itemType">ItemType to add</param>
    public void AddItemToHands(ItemType itemType)
    {
        SetItemType(ItemType.None, itemType);
    }

    /// <summary>
    /// Removes given ItemType from hand
    /// </summary>
    /// <param name="itemType">ItemType to remove</param>
    public void RemoveItemFromHands(ItemType itemType)
    {
        SetItemType(itemType, ItemType.None);
    }

    /// <summary>
    /// Check if the bellhop is currently holding an item of the given ItemType
    /// </summary>
    /// <param name="itemType">ItemType to match the Bellhop's items</param>
    /// <returns>true if the bellhop is holding an item of the given ItemType, false if not</returns>
    public bool HasItem(ItemType itemType)
    {
        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].itemType == itemType)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Change one of the items that matches the oldType to the newType
    /// This method can be used to remove or add items as a None type also exists
    /// </summary>
    /// <param name="oldType">Type you want to change</param>
    /// <param name="newType">New type to change into</param>
    private void SetItemType(ItemType oldType, ItemType newType)
    {
        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].itemType == oldType)
            {
                hands[i].SetItemType(newType);
                break;
            }
        }
    }
}
