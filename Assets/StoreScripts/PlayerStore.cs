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
        Inventory inventory=new Inventory();
        Store store=new Store();
        uiInvStore.setInventory(inventory);
        store.SetInventory(inventory);
        uiStore.setStore(store);
        uiStore.setUIInvStore(uiInvStore);
        uiInvStore.setUIStore(uiStore);
        uiInvStore.RefreshStoreInvItems();
        
        
        uiStore.RefreshUIStore();
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
