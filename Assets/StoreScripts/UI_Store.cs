using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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
    Item ToAddItem=Item.setItemBasedOnType(item.itemType,item.quantity);
    if(item.itemType!=Item.ItemType.pistolAmmo&&item.itemType!=Item.ItemType.shotGunAmmo&&item.itemType!=Item.ItemType.assaultRifleAmmo&&item.itemType!=Item.ItemType.revolverAmmo){
        ToAddItem.quantity=1;
    }

    bool isAdded=store.inventory.StoreToInv(ToAddItem);
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

      public void setUIToolTip(Transform itemSlotRectTransform,int index,Item item ){
                    //Get the ToolTip plane and then go to tis child and add a title and a description

            Transform Parent=itemSlotRectTransform.parent.parent;
            //Create a new toolTip and set its parent to be the parent
            Transform ToolTip=Instantiate(Parent.Find("toolTip")).GetComponent<RectTransform>();
            //set the parent to be the parent
            ToolTip.SetParent(Parent,false);
            // Transform ToolTip=Parent.Find("toolTip");
            Transform Title=ToolTip.Find("Title");
            Transform Description=ToolTip.Find("Desc");
            Title.GetComponent<TMPro.TextMeshProUGUI>().SetText(item.Title);
            Description.GetComponent<TMPro.TextMeshProUGUI>().SetText(item.Description);
          /* 
          1.Get the position X-195 of each child in the grid, the width of each box is 120 and they have a gap or 5 between them and
          each Row fit 6 items so I want when the row end to start from the beginning of the row
           

          2.Get the poistion of Y of each child in the grid, the height of each box is 120 and they have a gap or 5 between them and each row
           fit 5 elements so divide the current index by 5 then multiply by 125
          
          */
            float Xpoistion=(index%6)*125-195;
            float Ypoistion=((index/6)*-125)-20;

            ToolTip.GetComponent<RectTransform>().anchoredPosition=new Vector2(Xpoistion,Ypoistion);
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => {ToolTip.gameObject.SetActive(true);});   
            itemSlotRectTransform.GetComponent<EventTrigger>().triggers.Add(entry);

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerExit;
            entry2.callback.AddListener((eventData) => {ToolTip.gameObject.SetActive(false);});
            itemSlotRectTransform.GetComponent<EventTrigger>().triggers.Add(entry2);
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
       for(int i=0;i<BuyView.childCount;i++){
           if(BuyView.GetChild(i).name.Equals("toolTip(Clone)")){
               Destroy(BuyView.GetChild(i).gameObject);
           }
       }
        // Transform itemSprite=itemCont1.Find("item");
        // Transform itemAmount=itemCont1.Find("amount");
        // Transform itemPrice=itemCont1.Find("price");
        foreach(Item item in store.BuyItems){
            int index=store.BuyItems.IndexOf(item);
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
                        setUIToolTip(itemSlotRectTransform,index,item);


            
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
    int KnifeDurability=store.inventory.tpsController.knifeDurabilityGetter();

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
