using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] UpdateMessages updateMessages;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void AssignItemToSlot(Item item)
    {
        foreach(InventorySlot inventorySlot in inventorySlots)
        {
            if (!inventorySlot.GetIsSlotUsed())
            {
                inventorySlot.AssignItem(item);
                return;
            }
        }
    }

    public void RemoveItemFromSlot(int inventoryIndex)
    {
        inventorySlots[inventoryIndex].RemoveItem();
    }

    public void SelectItem(int inventoryIndex)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i == inventoryIndex)
            {
                inventorySlots[inventoryIndex].SelectItem();
                updateMessages.ChangeMessage(inventorySlots[inventoryIndex].GetItemDescription());
            }
            else
            {
                inventorySlots[i].UnselectItem();
            }
        }
    }
}
