using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Image selectedItemBorder;

    
    bool isSlotUsed;

    public bool GetIsSlotUsed()
    {
        return isSlotUsed;
    }
    
    public void AssignItem(Item item)
    {
        image.sprite = item.GetSprite();
        image.color = Color.white;
        isSlotUsed = true;
    }

    public void RemoveItem()
    {
        image.sprite = null;
        image.color = new Color(1, 1, 1, 0);
        isSlotUsed = false;
    }

    public void SelectItem()
    {
        if (isSlotUsed)
        {
            selectedItemBorder.gameObject.SetActive(true);
        }
    }

    public void UnselectItem()
    {
        selectedItemBorder.gameObject.SetActive(false);
    }
}
