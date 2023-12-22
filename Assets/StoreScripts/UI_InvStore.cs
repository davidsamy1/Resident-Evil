using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InvStore : MonoBehaviour
{
    public Inventory inventory;
    private Transform GoldText;

    private UI_Store uiStore;

    private Transform itemCont1;

void Awake(){
        itemCont1=transform.Find("itemCont1");
        GoldText=transform.Find("Gold");

}

public void setInventory(Inventory inventory){
    this.inventory=inventory;
}
public void setUIStore(UI_Store uiStore){
    this.uiStore=uiStore;
}

public void HandleInventoryToStorage(Item item){
    if(item.equipped==true){
        return;
    }
    inventory.Remove(item);
    // Debug.Log("Item Removed");
    uiStore.store.AddToStorage(item);
    // Debug.Log("Item Added to Storage");
    uiStore.RefreshUIStore();
    // Debug.Log("UI Store Refreshed");
     RefreshStoreInvItems();
    // Debug.Log("UI Inventory Refreshed");

}

public void HandleSellInv(Item item){
    inventory.Sell(item);
    RefreshStoreInvItems();
}


 public void RefreshStoreInvItems(){
        int x=0;
        int Poistion=-500; 
        GoldText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Current Gold : "+inventory.getGold().ToString());
        //Delete all any itemCont1(Clone) in the scene
        foreach(Transform child in transform){
            if(child.name.Equals("itemCont1(Clone)")){
                Destroy(child.gameObject);
            }
        }
         
        foreach(Item inventoryItem in inventory.GetItemList()){            

             RectTransform itemSlotRectTransform=Instantiate(itemCont1).GetComponent<RectTransform>();
             float y=itemCont1.GetComponent<RectTransform>().anchoredPosition.y;
             itemSlotRectTransform.anchoredPosition=new Vector2(Poistion+130,y);
             itemSlotRectTransform.transform.SetParent(transform,false);
            if(inventoryItem!=null){ 
              //Check if it is a knife then dont draw it as I dont want the knife to take place in the inv
                   
            itemSlotRectTransform.Find("item").GetComponent<UnityEngine.UI.Image>().sprite=inventoryItem.sprite;
            //Get text field in a TextMesh pro called amount and edit the text and set the amount to be active
            if(inventoryItem.quantity>1){

        itemSlotRectTransform.Find("amount").GetComponent<TMPro.TextMeshProUGUI>().SetText(inventoryItem.quantity.ToString());
            itemSlotRectTransform.Find("amount").gameObject.SetActive(true);
            }

            if(uiStore.store.storeName==Store.StoreNameType.Storage){
               Debug.Log("HIIIII");
            itemSlotRectTransform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{HandleInventoryToStorage(inventoryItem);});
            }
            else if (uiStore.store.storeName==Store.StoreNameType.Sell){
                itemSlotRectTransform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{HandleSellInv(inventoryItem);});
                //Set the Price 
                if(inventoryItem.sellPrice!=-1){
                itemSlotRectTransform.Find("price").GetComponent<TMPro.TextMeshProUGUI>().SetText(inventoryItem.sellPrice.ToString());
                itemSlotRectTransform.Find("price").gameObject.SetActive(true);
                }
            }
            } 

            else{
                itemSlotRectTransform.Find("item").GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.EmptySprite;
            }

              x++;
            Poistion=Poistion+130;
          
           
        }
      
 }
    
    

 
}
