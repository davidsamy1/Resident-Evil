using UnityEngine;

public class InventoryCreator
{
    private static Inventory inventoryInstance = null;
    public static Inventory getInstance()
    {
        if (inventoryInstance == null)
        {
            inventoryInstance = new Inventory();
            inventoryInstance.populateInventory();
            Debug.Log("Inventory Instance Created");
        }
        return inventoryInstance;
    }
}
