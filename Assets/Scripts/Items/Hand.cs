using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [Tooltip("Scriptable Object that contains the images that fit the item types.")]
    [SerializeField] private ItemTypeImages itemTypeImages;
    [Tooltip("Image that displays the item held.")]
    [SerializeField] private Image itemImage;

    [HideInInspector]
    public ItemType itemType { get; private set; }

    #region Unity Methods
    /// <summary>
    /// Set itemType to None to always start with empty hands.
    /// </summary>
    private void Start()
    {
        itemType = ItemType.None;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Set item type of this hand.
    /// Displays a corresponding image if one is found in the itemTypeImages object.
    /// </summary>
    /// <param name="itemType">Type of Item to hold.</param>
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

    /// <summary>
    /// Removes the item in the hand.
    /// This method is called by the UI hand button.
    /// </summary>
    public void RemoveItem()
    {
        SetItemType(ItemType.None);
    }
    #endregion
}

public enum ItemType
{ 
    None,
    CleaningSupplies,
    Coffee
}