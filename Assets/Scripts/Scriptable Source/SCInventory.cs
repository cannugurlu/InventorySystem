using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable/Inventory")]
public class SCInventory : ScriptableObject
{
    public List<Slot> inventorySlots = new List<Slot>();
    int stackLimit = 4;

    public void DeleteItem(int index)
    {
        inventorySlots[index].isFull = false;
        inventorySlots[index].itemCount = 0;
        inventorySlots[index].item = null;

    }
    public bool AddItem(SCItem item)
    {
        foreach (Slot slot in inventorySlots)
        {
            if(slot.item == item && 
               slot.item.canStackable && 
               slot.itemCount<stackLimit)
            {
                slot.itemCount++;
                if(slot.itemCount>=stackLimit) slot.isFull = true;
                return true;
            }
            else if(slot.itemCount==0)
            {
                slot.AddItemToSlot(item);
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class Slot
{
    public bool isFull;
    public int itemCount;
    public SCItem item;

    public void AddItemToSlot(SCItem item)
    {
        this.item = item;
        if (!item.canStackable)
        {
            isFull = true;
        }
        itemCount++;
    }
}
