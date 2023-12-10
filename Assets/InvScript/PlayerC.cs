using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour
{
    [SerializeField] UI_Inventory uiInventory;
    [SerializeField] UI_PlayerStats uiPlayerStats;
    [SerializeField] UI_CraftCont uiCraftCont;


    // Start is called before the first frame update
    Inventory inventory;
    void Start()
    {
        inventory = InventoryCreator.getInstance();
        uiInventory.setInventory(inventory);
        uiInventory.setUIPlayerStats(uiPlayerStats);
        uiInventory.setUICraftCont(uiCraftCont);
        uiPlayerStats.setInventory(inventory);
        uiCraftCont.setInventory(inventory);
        uiPlayerStats.setPlayerGold(inventory.getGold());
        uiPlayerStats.setPlayerHealth(100);
       
        inventory.ui_inventory = uiInventory;
        inventory.populateInventory();


        uiPlayerStats.RefreshPlayerStates();
        uiInventory.RefreshInventoryItems();




        
    }

    // Update is called once per frame
    void Update()
    {
/*        uiInventory.setInventory(inventory);
        uiPlayerStats.setInventory(inventory);
        uiCraftCont.setInventory(inventory);
        uiInventory.RefreshInventoryItems();*/
    }
}
