using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour
{
    [SerializeField] UI_Inventory uiInventory;
    [SerializeField] UI_PlayerStats uiPlayerStats;
    [SerializeField] UI_CraftCont uiCraftCont;
    // [SerializeField] TPSController tpsController;
    // [SerializeField] HealthBarController healthBarController;


    // Start is called before the first frame update
    Inventory inventory;
    void Start()
    {
        
        inventory = InventoryCreator.getInstance();
        inventory.PlayerToInvChanges();
        if(uiInventory==null||uiPlayerStats==null){
            Debug.Log("NULL");
            return;
        }
    
    
        //UI Inventory Setter
        uiInventory.setInventory(inventory);
        inventory.ui_inventory = uiInventory;
        //
        //UIPLAYER SETTER
        uiPlayerStats.setInventory(inventory);
        uiPlayerStats.setPlayerGold(inventory.getGold());
        uiPlayerStats.setPlayerHealth(100);
        ///
        //UICRAFTCONT SETTER
        uiCraftCont.setInventory(inventory);
        uiInventory.setUIPlayerStats(uiPlayerStats);
        uiInventory.setUICraftCont(uiCraftCont);
       ///
        uiPlayerStats.RefreshPlayerStates();
        uiInventory.RefreshInventoryItems();        
    }




    void OnEnable(){
        Debug.Log("I AM ENABELELEDLEWDLEDEL");
        //         Debug.Log("I AM ENABLING THE INVENTORY");
        // Debug.Log("TBS CONTROLLER IS" +tpsController.WeaponIndex);
        // tpsController.weaponsScriptableObjects[tpsController.WeaponIndex-1].totalAmmoInInventory=8;
        if(uiInventory==null||uiPlayerStats==null||inventory==null){
            return;
        
        }
        inventory.PlayerToInvChanges();
        uiPlayerStats.RefreshPlayerStates();
        uiInventory.RefreshInventoryItems(); 
        
            

    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Z)){
        InventoryCreator.getInstance().addGold(100);
        if(uiPlayerStats==null||uiInventory==null){
            return;
        }
    
        uiPlayerStats.RefreshPlayerStates();
        uiInventory.RefreshInventoryItems();
        
        }
    }
}
