using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStore : MonoBehaviour
{
    [SerializeField] UI_InvStore uiInvStore;
    [SerializeField] UI_Store uiStore;




    // Start is called before the first frame update
    void Start()
    {
        Inventory inventory=InventoryCreator.getInstance();
        Store store=new Store();
        uiInvStore.setInventory(inventory);
        store.SetInventory(inventory);
        uiStore.setStore(store);
        uiStore.setUIInvStore(uiInvStore);
        uiInvStore.setUIStore(uiStore);
        uiInvStore.RefreshStoreInvItems();
        uiStore.RefreshUIStore();
     
        
    }

    void OnEnable(){
        if(uiInvStore.inventory!=null&&uiStore.UIinvStore!=null){

        Debug.Log("I AM ENABLING THE STORE");
         uiInvStore.RefreshStoreInvItems();
        uiStore.RefreshUIStore();
        }
    
    
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Z)){
        if(InventoryCreator.getInstance()!=null){
        InventoryCreator.getInstance().addGold(100);
        uiInvStore.RefreshStoreInvItems();
        uiStore.RefreshUIStore();

        }

        }
        
    }
}
