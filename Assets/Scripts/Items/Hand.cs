using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Hand : MonoBehaviour
{
    [SerializeField] private ItemTypeImages itemTypeImages;
    [SerializeField] private Image itemImage;

    [HideInInspector]
    public ItemType itemType;

    private void Start()
    {
        itemType = ItemType.None;
    }

    public void SetItemType(ItemType itemType)
    {
        this.itemType = itemType;

        ItemTypeImage itemTypeImage = itemTypeImages.itemTypeImages.Find(m => m.itemType == itemType);
        if (itemTypeImage != null)
        {
            itemImage.sprite = itemTypeImage.sprite;
            itemImage.color = new Color(1, 1, 1, 255);
        }
        else
        {
            itemImage.color = new Color(1, 1, 1, 0);
            itemImage.sprite = null;
        }
        
    }

    public void RemoveItem()
    {
        SetItemType(ItemType.None);
    }
}

public enum ItemType
{ 
    None,
    CleaningSupplies,
    Coffee
}