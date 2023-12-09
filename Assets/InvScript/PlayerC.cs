using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour
{
    [SerializeField] UI_Inventory uiInventory;
    [SerializeField] UI_PlayerStats uiPlayerStats;
    [SerializeField] UI_CraftCont uiCraftCont;




    // Start is called before the first frame update
    void Start()
    {
        Inventory inventory=new Inventory();
        uiInventory.setInventory(inventory);
        uiPlayerStats.setInventory(inventory);
        uiCraftCont.setInventory(inventory);
        uiPlayerStats.setPlayerGold(100);
        uiPlayerStats.setPlayerHealth(100);
        uiPlayerStats.RefreshPlayerStates();
    
        uiInventory.RefreshInventoryItems();
        uiInventory.setUIPlayerStats(uiPlayerStats);
        uiInventory.setUICraftCont(uiCraftCont);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
