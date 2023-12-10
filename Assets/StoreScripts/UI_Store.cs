using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Store : MonoBehaviour
{
    /* 
    1.Get the Buy View
    2.Get the Sell View
    3.Get the StorageView
    4.DrawStore function
      1.Check the Type of Store 
        1.If Type is view then 
          1.Set buy view to active and other views to inactive
          2.Loop on the BuyItems and call the DrawStoreItem function
      2.Check if Type is Storage
        1.Set Storage view to active and other views to inactive
        2.Loop on the StorageItems and call the DrawStoreItem function
      3.Check if Type is Sell
        1.Set Sell view to active and other views to inactive
      4.Check if Type is Fix
       1.Set Fix view to active and other views to inactive
       
    */

    public Store store;
    private Transform BuyView;
    private Transform SellView;
    private Transform StorageView;
    private Transform FixView;
    public UI_InvStore UIinvStore;

    public Transform BuyBtn;
    public Transform SellBtn;
    public Transform StorageBtn;
    public Transform FixBtn;
    public void setStore(Store store)
    {
        this.store = store;
    }
    public void setUIInvStore(UI_InvStore UIinvStore)
    {
        this.UIinvStore = UIinvStore;
    }

    public void handleBuyClick(Item item){
        store.inventory.Buy(item);
        UIinvStore.RefreshStoreInvItems();
    }

    void Awake()
    {
        BuyView = transform.Find("BuyView");
        SellView = transform.Find("SellView");
        StorageView = transform.Find("StorageView");
        FixView = transform.Find("FixView");
        BuyBtn=transform.Find("BuyBTN");
        SellBtn=transform.Find("SellBTN");
        StorageBtn=transform.Find("StoreBTN");
        FixBtn=transform.Find("RepairBTN");
        BuyBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{SetName(Store.StoreNameType.Buy);});
        SellBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{SetName(Store.StoreNameType.Sell);});
        StorageBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{SetName(Store.StoreNameType.Storage);});
        FixBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{SetName(Store.StoreNameType.Fix);});

    
        
    }

public void SetName(Store.StoreNameType storeName){
    store.storeName=storeName;
    UIinvStore.RefreshStoreInvItems();
    RefreshUIStore();

}




public void HandleStoreClick(Item item){
    // Check if the Item type is any kind of ammo
        Item ToAddItem=new Item(item.sprite,item.sellPrice,item.buyPrice,item.quantity,item.itemType);
    if(item.itemType!=Item.ItemType.pistolAmmo&&item.itemType!=Item.ItemType.shotGunAmmo&&item.itemType!=Item.ItemType.assaultRifleAmmo&&item.itemType!=Item.ItemType.revolverAmmo){
        ToAddItem.quantity=1;
        
    }

    bool isAdded=store.inventory.AddItem(ToAddItem);
    if(isAdded==true){
        UIinvStore.RefreshStoreInvItems();
                store.RemoveFromStorage(ToAddItem);

        RefreshUIStore();
    }
    else{
        Debug.Log("Inventory is full");
    }
  
}



public void HandleFixClick(){
    store.inventory.Fix();
    RefreshUIStore();
    UIinvStore.RefreshStoreInvItems();
}


public void DrawStorage(){
        BuyView.gameObject.SetActive(false);
        SellView.gameObject.SetActive(false);
        StorageView.gameObject.SetActive(true);
        FixView.gameObject.SetActive(false);
        Transform StorageItemCont=StorageView.Find("StorageContainer");
        Transform itemCont1=StorageItemCont.Find("itemCont1");

        for(int i=0;i<StorageItemCont.childCount;i++){
            if(StorageItemCont.GetChild(i).name.Equals("itemCont1(Clone)")){
                Destroy(StorageItemCont.GetChild(i).gameObject);
            }
        }
        
 
        foreach(Item item in store.StorageItems){
            //Initialize a new ItemCont1 and set its parent to be BuyItemCont
            Transform itemSlotRectTransform=Instantiate(itemCont1).GetComponent<RectTransform>();
            //Set Active to true
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.transform.SetParent(StorageItemCont,false);
            //Set the item sprite to be the item sprite
            itemSlotRectTransform.Find("item").GetComponent<UnityEngine.UI.Image>().sprite=item.sprite;
            //Add a listener to the button
            itemSlotRectTransform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{HandleStoreClick(item);});
            //Set the item amount to be the item quantity
            if(item.quantity>1){
                itemSlotRectTransform.Find("amount").GetComponent<TMPro.TextMeshProUGUI>().SetText(item.quantity.ToString());
                itemSlotRectTransform.Find("amount").gameObject.SetActive(true);
            }
        }
}

public void DrawBuy(){
        BuyView.gameObject.SetActive(true);
        SellView.gameObject.SetActive(false);
        StorageView.gameObject.SetActive(false);
        FixView.gameObject.SetActive(false);
        Transform BuyItemCont=BuyView.Find("BuyItemsCont");
        Transform itemCont1=BuyItemCont.Find("itemCont1");
        for(int i=0;i<BuyItemCont.childCount;i++){
            if(BuyItemCont.GetChild(i).name.Equals("itemCont1(Clone)")){
                Destroy(BuyItemCont.GetChild(i).gameObject);
            }
        }
        // Transform itemSprite=itemCont1.Find("item");
        // Transform itemAmount=itemCont1.Find("amount");
        // Transform itemPrice=itemCont1.Find("price");
        foreach(Item item in store.BuyItems){
            //Initialize a new ItemCont1 and set its parent to be BuyItemCont
            Transform itemSlotRectTransform=Instantiate(itemCont1).GetComponent<RectTransform>();
            //Set Active to true
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.transform.SetParent(BuyItemCont,false);
            //Set the item sprite to be the item sprite
            itemSlotRectTransform.Find("item").GetComponent<UnityEngine.UI.Image>().sprite=item.sprite;
            //Set the item amount to be the item quantity
            if(item.quantity>1){
                itemSlotRectTransform.Find("amount").GetComponent<TMPro.TextMeshProUGUI>().SetText(item.quantity.ToString());
                itemSlotRectTransform.Find("amount").gameObject.SetActive(true);


            }
            //Set the item price to be the item price
            itemSlotRectTransform.Find("price").GetComponent<TMPro.TextMeshProUGUI>().SetText(item.buyPrice.ToString());
            //Add a listener to the button
            itemSlotRectTransform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{handleBuyClick(item);});
            

            
        }
 }
public void DrawFix(){
    BuyView.gameObject.SetActive(false);
    SellView.gameObject.SetActive(false);
    StorageView.gameObject.SetActive(false);
    FixView.gameObject.SetActive(true);
    Transform RepairButton=FixView.Find("Button");
    Transform itemSprint=FixView.Find("itemCont1");
    Transform DurabilityText=itemSprint.Find("amount");
    int KnifeDurability=store.inventory.Knife.durability;


    //Add Listner
    RepairButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{HandleFixClick();});


    DurabilityText.GetComponent<TMPro.TextMeshProUGUI>().SetText(KnifeDurability.ToString());
    

}

public void DrawSell(){
    //Set the Sell view to be active and other views to be inactive
    BuyView.gameObject.SetActive(false);
    SellView.gameObject.SetActive(true);
    StorageView.gameObject.SetActive(false);
    FixView.gameObject.SetActive(false);
    
}


public void RefreshUIStore(){
                    
        if(store.storeName==Store.StoreNameType.Buy){
            DrawBuy();
        }
        else if(store.storeName==Store.StoreNameType.Storage){
            DrawStorage();
        }
        else if(store.storeName==Store.StoreNameType.Sell){
            DrawSell();
        }
        else if(store.storeName==Store.StoreNameType.Fix){
            DrawFix();
        }

    }

}
