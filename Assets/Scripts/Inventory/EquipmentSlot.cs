using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image defaultIcon;
    [SerializeField] private Image itemIcon;
    public EquipmentSlotType type;

    private ItemData itemData;
    public Image placeholderIcon;
    public Sprite defaultSprite;
    public Sprite swordSprite;
    public Sprite shieldSprite;
    public Sprite bowSprite;
    public Sprite hpPotionSprite;
    public Sprite mpPotionSprite;

    public void SetItem(ItemData data)
    {
        itemData = data;

        // Set the item icon based on the item type
        switch (itemData.type)
        {
            case ItemType.Equipabble:
                SetEquipabbleItemIcon();
                break;
            case ItemType.Consumable:
                SetConsumableItemIcon();
                break;
            default:
                // Set the default sprite for other item types
                itemIcon.sprite = defaultSprite;
                break;
        }

        // Enable the item icon to display it
        placeholderIcon.enabled = false;
        itemIcon.enabled = true;
    }

    private void SetEquipabbleItemIcon()
    {
        // Check the specific item IDs
        switch (itemData.id)
        {
            case "Sword":
                itemIcon.sprite = swordSprite;
                break;
            case "Shield":
                itemIcon.sprite = shieldSprite;
                break;
            case "Bow":
                itemIcon.sprite = bowSprite;
                break;
            default:
                // Set the default sprite for other equipabble item types
                itemIcon.sprite = defaultSprite;
                break;
        }
    }

    private void SetConsumableItemIcon()
    {
        // Check the specific item IDs
        switch (itemData.id)
        {
            case "HP Potion":
                itemIcon.sprite = hpPotionSprite;
                break;
            case "MP Potion":
                itemIcon.sprite = mpPotionSprite;
                break;
            default:
                // Set the default sprite for other consumable item types
                itemIcon.sprite = defaultSprite;
                break;
        }
    }

    private void ApplyItemAttributes()
    {
        // Apply the attributes of the item to the player
        if (itemData != null)
        {
            Player player = FindObjectOfType<Player>();

            if (player != null)
            {
                player.AddAttributes(itemData.attributes);
            }
        }
    }

    public void Unequip()
    {
        // TODO
        // Check if there is an available inventory slot before removing the item.
        // Make sure to return the equipment to the inventory when there is an available slot.
        // Reset the item data and icons here
        if (itemData != null)
        {
            // Remove the attributes of the item from the player before unequipping
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.RemoveAttributes(itemData.attributes);
            }

            // Check if there is an available inventory slot
            int emptySlotIndex = InventoryManager.Instance.GetEmptyInventorySlot();

            if (emptySlotIndex != -1)
            {
                // Return the item to the inventory
                InventoryManager.Instance.AddItem(itemData.id);

                Debug.Log("Unequipped item: " + itemData.id);
            }
            else
            {
                Debug.Log("No available inventory slot to unequip item: " + itemData.id);
            }

            // Reset the item data and icons
            itemData = null;
            itemIcon.sprite = defaultIcon.sprite;
            itemIcon.enabled = false;
            placeholderIcon.enabled = true;
        }
    }
}
