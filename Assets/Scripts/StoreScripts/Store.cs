using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{

    public enum StoreNameType{
        Buy,
        Sell,
        Storage,
        Fix,
    }
    public StoreNameType storeName=StoreNameType.Sell;
    public List<Item> StorageItems=new List<Item>();
    public List<Item> BuyItems=new List<Item>();
    public Inventory inventory;


    /*
    0.Initlize the BuyItems
    1.Set Inventory
    2.Set Type
    3.Add to Storage
    4.Remove From Storage
    
     */

     public Store(){
            //Initlize the BuyItems
        Item NormalGunPowder=Item.getNormalGunPowder();  
        Item HighGunPowder=Item.getHighGunPowder();
        Item GreenHerb=Item.getGreenHerb();
        Item RedHerb=Item.getRedHerb();
        Item HandGrenade=Item.getHandGrenade();
        Item FlashGrenade=Item.getFlashGrenade();
        Item Shotgun=Item.getShotGun();
        Item AssaultRifle=Item.getAssaultRifle();
        Item PistolAmmo=Item.getPistolAmmo();
        Item ShotgunAmmo=Item.getShotGunAmmo();
        Item AssaultRifleAmmo=Item.getAssaultRifleAmmo();
        Item RevolverAmmo=Item.getRevolverAmmo();
        BuyItems.Add(NormalGunPowder);
        BuyItems.Add(HighGunPowder);
        BuyItems.Add(GreenHerb);
        BuyItems.Add(RedHerb);
        BuyItems.Add(HandGrenade);
        BuyItems.Add(FlashGrenade);
        BuyItems.Add(Shotgun);
        BuyItems.Add(AssaultRifle);
        BuyItems.Add(PistolAmmo);
        BuyItems.Add(ShotgunAmmo);
        BuyItems.Add(AssaultRifleAmmo);
        BuyItems.Add(RevolverAmmo);


     }

    public void SetInventory(Inventory inventory){
        this.inventory=inventory;
    }
   
    public void SetType( StoreNameType type){
        this.storeName=type;
    }
    public void AddToStorage(Item item){
        //Loop on the StorgeItmest and check if the item is already there
        //If it is there then add the quantity
        //If it is not there then add it to the list
        // Debug.Log("I am hereE");
        bool found=false;
        foreach(Item storageItem in StorageItems){
            if(storageItem.itemType==item.itemType){
                storageItem.quantity+=item.quantity;
                found=true;
            }
        }
        if(found==false){
            // Debug.Log("I am here ADDING");
            StorageItems.Add(item);
        }

    }




  public void RemoveFromStorage(Item item){
        //Loop on the StorgeItmest and check if the item is already there
            //If it is there then add the quantity
            //If it is not there then add it to the list
            bool found=false;
            Item ItemToRemove=null;
            foreach(Item storageItem in StorageItems){
                if(storageItem.itemType==item.itemType){
                    storageItem.quantity-=item.quantity;
                    if(storageItem.quantity==0){
                        found=false;
                        ItemToRemove=storageItem;
                        break;
                    }
                    else{
                    found=true;
                    break;
                    }
                   
                }
            }
            if(found==false&&ItemToRemove!=null){
                StorageItems.Remove(ItemToRemove);

            }
  }

  
}
