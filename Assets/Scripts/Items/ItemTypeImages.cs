using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemTypeImages_#", menuName = "ScriptableObjects/Items/Create ItemType Images Holder", order = 1)]
public class ItemTypeImages : ScriptableObject
{
    public List<ItemTypeImage> itemTypeImages;
}

[Serializable]
public class ItemTypeImage
{
    public ItemType itemType;
    public Sprite sprite;
}