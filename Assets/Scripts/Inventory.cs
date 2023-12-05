using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory;
    InventoryUIController inventoryUIController;

    bool isSwapping;
    int tempIndex;
    Slot tempSlot;

    private void Start()
    {
        inventoryUIController=gameObject.GetComponent<InventoryUIController>();   
        inventoryUIController.UpdateUI();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (playerInventory.AddItem(other.gameObject.GetComponent<Item>().item))
            {
                Destroy(other.gameObject);
                inventoryUIController.UpdateUI();
            }

        }
    }

    public void DeleteItem()
    {
        if(isSwapping)
        {
            playerInventory.DeleteItem(tempIndex);
            isSwapping = false;
            inventoryUIController.UpdateUI();
        }
    }
    public void SwapItems(int index)
    {
        if(!isSwapping)
        {
            tempIndex = index;
            tempSlot = playerInventory.inventorySlots[tempIndex];
            isSwapping = true;
        }
        else if(isSwapping)
        {
            playerInventory.inventorySlots[tempIndex] = playerInventory.inventorySlots[index];
            playerInventory.inventorySlots[index] = tempSlot;
            isSwapping=false;
        }
        inventoryUIController.UpdateUI();
    }
}
