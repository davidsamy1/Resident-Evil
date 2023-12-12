using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    public Inventory inventory;
    private Transform itemCont1;
    private Transform EquipBTN;
    private Transform UseBTN;
    private Transform DiscardBTN;
    private Transform CraftBTN;
    private UI_PlayerStats uiPlayerStats;
    private UI_CraftCont uiCraftCont;
    // private TPSController tpsController;



    public void setInventory(Inventory inventory){
        this.inventory=inventory;
    }
    public void setUIPlayerStats(UI_PlayerStats uiPlayerStats){
        this.uiPlayerStats=uiPlayerStats;
    }

    // public void setTpsController(TPSController tpsController){
    //     this.tpsController=tpsController;
    // }
    
    public void setUICraftCont(UI_CraftCont uiCraftCont){
        this.uiCraftCont=uiCraftCont;
    }
    // Start is called before the first frame update
    void Awake()
    {
        itemCont1=transform.Find("itemCont1");
        EquipBTN=transform.Find("equipBTN");
        UseBTN=transform.Find("useBTN");
        DiscardBTN=transform.Find("discardBTN");
        CraftBTN=transform.Find("craftBTN");
        //Add a listener to each button
        EquipBTN.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{HandleEquipBTN();});
        UseBTN.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{HandleUse();});
        DiscardBTN.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{HandleDiscard();});
        CraftBTN.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{HandleCraft();});


        //Get the text inside the child

    }

  

public void ResetSelected(){
    foreach(Item inventoryItem in inventory.GetItemList()){
        if(inventoryItem!=null&&inventoryItem.selected==true){
            inventoryItem.selected=false;
        }
       EquipBTN.gameObject.SetActive(false);
    UseBTN.gameObject.SetActive(false);
    DiscardBTN.gameObject.SetActive(false);
    CraftBTN.gameObject.SetActive(false);
        RefreshInventoryItems();
    }

}

public void HandleEquipBTN(){
    inventory.EquippedItem();
    uiPlayerStats.RefreshPlayerStates();
    //Disable all buttons
    ResetSelected();
    
}
public void HandleDiscard(){
    inventory.DiscardItem();
    RefreshInventoryItems();
    uiPlayerStats.RefreshPlayerStates();
    ResetSelected();
}

public void HandleUse(){
    inventory.UseItem();
    RefreshInventoryItems();
    uiPlayerStats.RefreshPlayerStates();
    ResetSelected();

}

public void HandleCraft(){
    inventory.isCrafting=true;
    uiCraftCont.CraftStationApear();
    uiCraftCont.RefreshCraft();

}

public void SelectedItem(Item toBeSelected){

/* 
1.Check if Craft is true and go to the Craft route
2.IF craft is false then go to the normal route
......................
Craft
1.Check if there is a selected item in the inv
2.if true then set the current item to be craftSelected
3.Call the craft function
4.else if false then do nothing
......................
Normal
1.Turn all the selected to false
2.Set the current selected to true 
3.Check if the selected is weapon/grenade/knife then set the button text to be equip else set the button text to be use
*/
if(inventory.isCrafting){
    if(toBeSelected.selected==true){
        inventory.isCrafting=false;
        uiCraftCont.CraftStationDisappear();
        
    }
    else{
        foreach(Item inventoryItem in inventory.GetItemList()){
            if(inventoryItem.selected==true){
                inventory.Craft(inventoryItem,toBeSelected);
                inventory.isCrafting=false;
                uiCraftCont.CraftStationDisappear();
                break;
            }
        }
    }
    ResetSelected();
    

}
else{
    foreach(Item inventoryItem in inventory.GetItemList()){
        if(inventoryItem!=null&&inventoryItem.selected==true){
            inventoryItem.selected=false;
        }
    }

    /*
    When can I  craft if i press on a green herb/redherb/powder
When can I equip if I pressed on a weapon/grenade
When can I Use when i click no a redmix/greenmix/redgreenmix
     */

     if(inventory.isCrafting){
        EquipBTN.gameObject.SetActive(false);
        UseBTN.gameObject.SetActive(false);
        DiscardBTN.gameObject.SetActive(false);
        CraftBTN.gameObject.SetActive(false);

     }
    else if(toBeSelected.itemType==Item.ItemType.shotGun||toBeSelected.itemType==Item.ItemType.assaultRifle||toBeSelected.itemType==Item.ItemType.revolver||toBeSelected.itemType==Item.ItemType.pistol||toBeSelected.itemType==Item.ItemType.handGrenade||toBeSelected.itemType==Item.ItemType.flashGrenade){
        if(toBeSelected.equipped){
        EquipBTN.gameObject.SetActive(false);
        UseBTN.gameObject.SetActive(false);
        DiscardBTN.gameObject.SetActive(false);
        CraftBTN.gameObject.SetActive(false);
        }
        else{
        EquipBTN.gameObject.SetActive(true);
        UseBTN.gameObject.SetActive(false);
        DiscardBTN.gameObject.SetActive(false);
        CraftBTN.gameObject.SetActive(false);
        }
    }
    else if(toBeSelected.itemType==Item.ItemType.redHerb||toBeSelected.itemType==Item.ItemType.normalGunPowder||toBeSelected.itemType==Item.ItemType.highGradeGunPowder){
        //Activate all buttons but the Use one
        EquipBTN.gameObject.SetActive(false);
        UseBTN.gameObject.SetActive(false);
        DiscardBTN.gameObject.SetActive(true);
        CraftBTN.gameObject.SetActive(true);
    }
    else if(toBeSelected.itemType==Item.ItemType.greenHerb){
        EquipBTN.gameObject.SetActive(false);
        UseBTN.gameObject.SetActive(true);
        DiscardBTN.gameObject.SetActive(true);
        CraftBTN.gameObject.SetActive(true);
    }
    else if(toBeSelected.itemType==Item.ItemType.greenMix||toBeSelected.itemType==Item.ItemType.redGreenMix){
        //Activate all buttons but the Use one
        EquipBTN.gameObject.SetActive(false);
        UseBTN.gameObject.SetActive(true);
        DiscardBTN.gameObject.SetActive(true);
        CraftBTN.gameObject.SetActive(false);
    }
 
    else if (toBeSelected.itemType!=Item.ItemType.emblemKey&&toBeSelected.itemType!=Item.ItemType.cardKey&&toBeSelected.itemType!=Item.ItemType.spadeKey&&toBeSelected.itemType!=Item.ItemType.heartKey&&toBeSelected.itemType!=Item.ItemType.diamondKey&&toBeSelected.itemType!=Item.ItemType.clubKey){
   
        EquipBTN.gameObject.SetActive(false);
        UseBTN.gameObject.SetActive(false);
        DiscardBTN.gameObject.SetActive(true);
        CraftBTN.gameObject.SetActive(false);
    }
    else{
        //Activate all buttons but the Use one
        EquipBTN.gameObject.SetActive(false);
        UseBTN.gameObject.SetActive(false);
        DiscardBTN.gameObject.SetActive(false);
        CraftBTN.gameObject.SetActive(false);
    }
  


    toBeSelected.selected=true;
}

RefreshInventoryItems();

}
   








    // Refresh Iventory 
    public void RefreshInventoryItems(){
        int x=0;
        int Poistion=-500; 

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
            if(inventoryItem.selected==true){
                itemSlotRectTransform.Find("Selected").gameObject.SetActive(true);
            }
            else{
                itemSlotRectTransform.Find("Selected").gameObject.SetActive(false);
            }

            itemSlotRectTransform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{SelectedItem(inventoryItem);});
        
            }
            else{
                itemSlotRectTransform.Find("item").GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.EmptySprite;
            }

              x++;
            Poistion=Poistion+130;
          
           
        }
      
    }
  

}
