using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemsList;
    public GameObject inventoryPanel;
    public UI_Inventory ui_inventory;
    public Item Knife;
    private int gold=30;
    public bool isCrafting=false;

public Inventory(){
        itemsList=new List<Item>();
        Knife=new Item(ItemsAssests.Instance.KnifeSprite,100,100,1,Item.ItemType.knife);
        Knife.durability=100;

         while(itemsList.Count<6){
            itemsList.Add(null);
        
         }
    }

    public void populateInventory()
    {
        Item Pistol = Item.getPistol();
        Pistol.equipped = true;
        Item Pistol2 = Item.getPistol();
        Item PistolAmmo = Item.getPistolAmmo();
        Item GreenHerb = Item.getGreenHerb();
        Item RedHerb = Item.getRedHerb();
        Item redMix = Item.getRedMix();
        Item handGrenade = Item.getHandGrenade();


        this.AddItem(Pistol);
        this.AddItem(Pistol2);
        this.AddItem(PistolAmmo); 
/*        this.AddItem(GreenHerb);
        this.AddItem(GreenHerb);
        this.AddItem(RedHerb);
        this.AddItem(redMix);
        this.AddItem(handGrenade);*/

/*        this.AddItem(Item.getGoldTreasure());
        this.AddItem(Item.getRubyTreasure());
        this.AddItem(Item.getEmeraldTreasure());*/
    }

    public void Craft(Item selectedItem,Item craftSelectedItem){
/* 
1.greenHerb + greenHerb = greenMix
2.greenHerb + redHerb = redGreenMix
3.redHerb + greenHerb = redGreenMix
3.redHerb + redHerb = redMix
4.normalGunPowder + normalGunPowder = pistolAmmo
5.normalGunPowder + highGradeGunPowder = shotgunAmmo
6.highGradeGunPowder + normalGunPowder = shotgunAmmo
7.highGradeGunPowder + highGradeGunPowder = assaultRifleAmmo
Check if we can make the combination and if we can the remove the two items from the inventory and add the new one to the inventory
*/

if(selectedItem.itemType==Item.ItemType.greenHerb && craftSelectedItem.itemType==Item.ItemType.greenHerb){
    Item greenMix=new Item(ItemsAssests.Instance.GreenMixSprite,100,100,1,Item.ItemType.greenMix);
    this.Remove(selectedItem);
    this.Remove(craftSelectedItem);
    this.AddItem(greenMix);
     this.isCrafting=false;
     

}
else if(selectedItem.itemType==Item.ItemType.greenHerb && craftSelectedItem.itemType==Item.ItemType.redHerb){
    Item redGreenMix=new Item(ItemsAssests.Instance.RedGreenMixSprite,100,100,1,Item.ItemType.redGreenMix);
    this.Remove(selectedItem);
    this.Remove(craftSelectedItem);
    this.AddItem(redGreenMix);
     this.isCrafting=false;

}
else if(selectedItem.itemType==Item.ItemType.redHerb && craftSelectedItem.itemType==Item.ItemType.greenHerb){
    Item redGreenMix=new Item(ItemsAssests.Instance.RedGreenMixSprite,100,100,1,Item.ItemType.redGreenMix);
    this.Remove(selectedItem);
    this.Remove(craftSelectedItem);
    this.AddItem(redGreenMix);
     this.isCrafting=false;

}
else if(selectedItem.itemType==Item.ItemType.redHerb && craftSelectedItem.itemType==Item.ItemType.redHerb){
    Item redMix=new Item(ItemsAssests.Instance.RedMixSprite,100,100,1,Item.ItemType.redMix);
    this.Remove(selectedItem);
    this.Remove(craftSelectedItem);
    this.AddItem(redMix);
     this.isCrafting=false;

}
else if(selectedItem.itemType==Item.ItemType.normalGunPowder && craftSelectedItem.itemType==Item.ItemType.normalGunPowder){
    Item pistolAmmo=new Item(ItemsAssests.Instance.PistolAmmoSprite,100,100,12,Item.ItemType.pistolAmmo);
    this.Remove(selectedItem);
    this.Remove(craftSelectedItem);
    this.AddItem(pistolAmmo);
     this.isCrafting=false;

}
else if(selectedItem.itemType==Item.ItemType.normalGunPowder && craftSelectedItem.itemType==Item.ItemType.highGradeGunPowder){
    Item shotgunAmmo=new Item(ItemsAssests.Instance.ShotGunAmmoSprite,100,100,8,Item.ItemType.shotGunAmmo);
    this.Remove(selectedItem);
    this.Remove(craftSelectedItem);
    this.AddItem(shotgunAmmo);
     this.isCrafting=false;

}
else if(selectedItem.itemType==Item.ItemType.highGradeGunPowder && craftSelectedItem.itemType==Item.ItemType.normalGunPowder){
    Item shotgunAmmo=new Item(ItemsAssests.Instance.ShotGunAmmoSprite,100,100,8,Item.ItemType.shotGunAmmo);
    this.Remove(selectedItem);
    this.Remove(craftSelectedItem);
    this.AddItem(shotgunAmmo);
     this.isCrafting=false;

}
else if(selectedItem.itemType==Item.ItemType.highGradeGunPowder && craftSelectedItem.itemType==Item.ItemType.highGradeGunPowder){
    Item assaultRifleAmmo=new Item(ItemsAssests.Instance.AssaultRifleAmmoSprite,100,100,30,Item.ItemType.assaultRifleAmmo);
    this.Remove(selectedItem);
    this.Remove(craftSelectedItem);
    this.AddItem(assaultRifleAmmo);
     this.isCrafting=false;

}
else{
    this.isCrafting=false;



}
}

public Item[][] CraftOptions(Item selectedItem ){
Item[][] ResultValue=new Item[2][];
if(selectedItem==null){
    return null;
}    
if(selectedItem.itemType==Item.ItemType.greenHerb){
    Item GreenMix=new Item(ItemsAssests.Instance.GreenMixSprite,100,100,1,Item.ItemType.greenMix);
    Item RedGreenMix=new Item(ItemsAssests.Instance.RedGreenMixSprite,100,100,1,Item.ItemType.redGreenMix);
    Item RedHerb=new Item(ItemsAssests.Instance.RedHerbSprite,100,100,1,Item.ItemType.redHerb);
    Item GreenHerb=new Item(ItemsAssests.Instance.GreenHerbSprite,100,100,1,Item.ItemType.greenHerb);
     ResultValue[0]=new Item[]{GreenHerb,RedGreenMix};
        ResultValue[1]=new Item[]{RedHerb,GreenMix};
    return ResultValue;
}
else if(selectedItem.itemType==Item.ItemType.redHerb){
    Item RedMix=new Item(ItemsAssests.Instance.RedMixSprite,100,100,1,Item.ItemType.redMix);
    Item RedGreenMix=new Item(ItemsAssests.Instance.RedGreenMixSprite,100,100,1,Item.ItemType.redGreenMix);
    Item RedHerb=new Item(ItemsAssests.Instance.RedHerbSprite,100,100,1,Item.ItemType.redHerb);
    Item GreenHerb=new Item(ItemsAssests.Instance.GreenHerbSprite,100,100,1,Item.ItemType.greenHerb);
    ResultValue[0]=new Item[]{RedHerb,RedGreenMix};
    ResultValue[1]=new Item[]{GreenHerb,RedMix};
    return ResultValue;
}
else if(selectedItem.itemType==Item.ItemType.normalGunPowder){
    Item PistolAmmo=new Item(ItemsAssests.Instance.PistolAmmoSprite,100,100,12,Item.ItemType.pistolAmmo);
    Item ShotgunAmmo=new Item(ItemsAssests.Instance.ShotGunAmmoSprite,100,100,8,Item.ItemType.shotGunAmmo);
    Item NormalGunPowder=new Item(ItemsAssests.Instance.NormalGunPowderSprite,100,100,1,Item.ItemType.normalGunPowder);
    Item HighGradeGunPowder=new Item(ItemsAssests.Instance.HighGradeGunPowderSprite,100,100,1,Item.ItemType.highGradeGunPowder);
    ResultValue[0]=new Item[]{NormalGunPowder,PistolAmmo};
    ResultValue[1]=new Item[]{HighGradeGunPowder,ShotgunAmmo};
    return ResultValue;
    // return [[NormalGunPowder,PistolAmmo],[HighGradeGunPowder,ShotgunAmmo]];
}
else if(selectedItem.itemType==Item.ItemType.highGradeGunPowder){
    Item ShotgunAmmo=new Item(ItemsAssests.Instance.ShotGunAmmoSprite,100,100,8,Item.ItemType.shotGunAmmo);
    Item AssaultRifleAmmo=new Item(ItemsAssests.Instance.AssaultRifleAmmoSprite,100,100,30,Item.ItemType.assaultRifleAmmo);
    Item NormalGunPowder=new Item(ItemsAssests.Instance.NormalGunPowderSprite,100,100,1,Item.ItemType.normalGunPowder);
    Item HighGradeGunPowder=new Item(ItemsAssests.Instance.HighGradeGunPowderSprite,100,100,1,Item.ItemType.highGradeGunPowder);
    // return [[NormalGunPowder,ShotgunAmmo],[HighGradeGunPowder,AssaultRifleAmmo]];
    ResultValue[0]=new Item[]{NormalGunPowder,ShotgunAmmo};
    ResultValue[1]=new Item[]{HighGradeGunPowder,AssaultRifleAmmo};
    return ResultValue;
} 
else{
    return null;
}

}


public void Fix(){
    if(this.gold>=100){
        Knife.durability=10;
        this.gold=this.gold-100;
    }

}
public void setGold(int gold){
    this.gold=gold;
}
public int getGold(){
    return this.gold;
}
public bool isFull(int Max=6){
    int count=0;
    foreach(Item item in itemsList){
        if(item!=null){
            count=count+1;
        }
    }
    if(count==Max){
        return true;
    }
    else{
        return false;
    }


}
public bool AddItem(Item item){

        /* 
        1.Check if the added item is ammo
        2.If true then check if there is already an ammo of the same type in the inventory
        3.If true then add the quantity of the new ammo to the old one and remove the new one
        4.Check if the inventory is full if full then return else
        4.If false then add the new ammo to the inventory
        5.Else add the item to the inventory
        */
        if(item.itemType==Item.ItemType.pistolAmmo||item.itemType==Item.ItemType.shotGunAmmo||item.itemType==Item.ItemType.assaultRifleAmmo||item.itemType==Item.ItemType.revolverAmmo){
            foreach(Item inventoryItem in itemsList){
                if(inventoryItem!=null&&inventoryItem.itemType==item.itemType){
                    inventoryItem.quantity=inventoryItem.quantity+item.quantity;
                    ui_inventory.RefreshInventoryItems();
                    return true;
                }

            }
            if(isFull()){
                Debug.Log("Inventory is full");
                return false;
            }
            else{
                for (int i = 0; i < itemsList.Count; i++)
                {
                    if (itemsList[i] == null)
                    {
                        itemsList[i] = item;
                        ui_inventory.RefreshInventoryItems();
                        return true;
                    }
                }
            }
        }
        else{

            if(isFull()){
                Debug.Log("Inventory is full");
                return false;
            }
            else{
                for(int i=0;i<itemsList.Count;i++){
                    if(itemsList[i]==null){
                        itemsList[i]=item;
                        ui_inventory.RefreshInventoryItems();
                        return true;
                    }
                }
            }
        }

        
         
return false;

        
        
        
    }
  
 public List<Item> GetItemList(){
        return itemsList;
    }


public int getItemAmount(Item.ItemType itemType){
        int amount=0;
        foreach(Item item in itemsList){
            if(item!=null&&item.itemType==itemType){
                amount=amount+item.quantity;
            }
        }
        return amount;
        
    }




public  bool isCraftable(Item selectedItem){
    if(selectedItem.itemType==Item.ItemType.greenHerb){
        return true;
    }
    else if(selectedItem.itemType==Item.ItemType.redHerb){
        return true;
    }
    else if(selectedItem.itemType==Item.ItemType.normalGunPowder){
        return true;
    }
    else if(selectedItem.itemType==Item.ItemType.highGradeGunPowder){
        return true;
    }
    else{
        return false;
    }
}


public Item getSelected(){
    foreach(Item item in itemsList){
        if(item!=null&&item.selected==true){
            return item;
        }
    }
    return null;

}


public void Remove(Item item){
    itemsList.Remove(item);
    itemsList.Add(null);
    ui_inventory.RefreshInventoryItems();

}
public void EquippedItem(){
    Item.ItemType SelectedItemType=getSelected().itemType;
    /* 
    1.I have 2 optins either the equipped is a weapon or a grenade so we check if the selected is not a handGrenade or a flashGrenade then loop on all the items
    and set all of the equipped to false except for the handGrenade or the flashGrenade
   2.If the selected is a handGrenadre or flashGrenade then loop on all the items and only set handGrenade and flashGrenade equipped to false 
    */
    if(SelectedItemType!=Item.ItemType.handGrenade&&SelectedItemType!=Item.ItemType.flashGrenade){
        foreach(Item item in itemsList){
            if(item!=null&&item.itemType!=Item.ItemType.handGrenade&&item.itemType!=Item.ItemType.flashGrenade){
                item.equipped=false;
            }
        }
    }
    else{
        foreach(Item item in itemsList){
            if(item!=null&&(item.itemType==Item.ItemType.handGrenade||item.itemType==Item.ItemType.flashGrenade)){
                item.equipped=false;
            }
        }
    }

    //Now we set the selected item to be equipped
    getSelected().equipped=true;

    //Now set everything to be not selected
    foreach(Item item in itemsList){
        if(item!=null&&item.selected==true){
            item.selected=false;
        }
    }
    
    
  
}


public void Buy(Item item){
    if(item.buyPrice>gold){
        Debug.Log("Not enough gold");
        return;
    }
    else{
        bool isAdded=AddItem(item);
        if(isAdded==true){
        gold=gold-item.buyPrice;
        }
    }
   
}

public void Sell(Item item){
    if(item.sellPrice!=-1&&item.equipped==false){
    gold=gold+item.sellPrice;
    Remove(item);
    }
}


public void SetCraft(){
    this.isCrafting=true;
}
public void UseItem(){
    //1.Check if there is a selected item in the inv
    //2.if true then use the item and according to the item it will be sent to a different use function
    //3.else do nothing
}

public void DiscardItem(){
    //Check if the selected Item is of type weapon/grenade/knife  then do nothing
    //else remove the current selected item from the invetory array

    if(getSelected().itemType==Item.ItemType.shotGun||getSelected().itemType==Item.ItemType.assaultRifle||getSelected().itemType==Item.ItemType.revolver||getSelected().itemType==Item.ItemType.pistol||getSelected().itemType==Item.ItemType.handGrenade||getSelected().itemType==Item.ItemType.flashGrenade||getSelected().itemType==Item.ItemType.knife){
        return;
    }
    else{
        //set it to null
        this.Remove(getSelected());
    }
    

}


public void AddPickUpItem(Item.ItemType itemType)
    {
        switch (itemType)
        {
            // Herbs
            case Item.ItemType.greenHerb:
                this.AddItem(Item.getGreenHerb()); break;
            case Item.ItemType.redHerb:
                this.AddItem(Item.getRedHerb()); break;

            // Grenades & Weapons
            case Item.ItemType.handGrenade:
                this.AddItem(Item.getHandGrenade()); break;
            case Item.ItemType.flashGrenade:
                this.AddItem(Item.getFlashGrenade()); break;
            case Item.ItemType.revolver:
                {
                    Debug.Log(SearchItem(Item.ItemType.cardKey));
                    if (SearchItem(Item.ItemType.cardKey)!=null)
                    {
                        this.AddItem(Item.getRevolver()); 
                    }
                    else
                    {
                        // error message here
                    }
                    break;
                }
                

             // Treasures
            case Item.ItemType.goldTreasure:
                this.AddItem(Item.getGoldTreasure()); break;
            case Item.ItemType.emeraldTreasure:
                this.AddItem(Item.getEmeraldTreasure()); break;
            case Item.ItemType.rubyTreasure:
                this.AddItem(Item.getRubyTreasure()); break;

             // Keys
            case Item.ItemType.spadeKey:
                this.AddItem(Item.getSpadeKey()); break;
            case Item.ItemType.emblemKey:
                this.AddItem(Item.getEmblemKey()); break;
            case Item.ItemType.heartKey:
                this.AddItem(Item.getHeartKey()); break;
            case Item.ItemType.diamondKey:
                this.AddItem(Item.getDiamondKey()); break;
            case Item.ItemType.cardKey:
                this.AddItem(Item.getCardKey()); break;
            case Item.ItemType.clubKey:
                this.AddItem(Item.getClubKey()); break;

            // Gunpowder
            case Item.ItemType.normalGunPowder:
                this.AddItem(Item.getNormalGunPowder()); break;
            case Item.ItemType.highGradeGunPowder:
                this.AddItem(Item.getHighGunPowder()); break;

            // Gold
            case Item.ItemType.gold:
                {
                    int randomGoldIncrement = new System.Random().Next(5, 51);// random num between [5,50]
                    Debug.Log("Gold Added: " + randomGoldIncrement);
                    gold += randomGoldIncrement;
                    Debug.Log("Current Gold: " + gold);  break;
                }

        }

    }


    public Item SearchItem(Item.ItemType itemType)
    {
        foreach (Item item in itemsList)
        {
            if (item != null && item.itemType == itemType)
            {
                return item;
            }
        }
        return null; // Item not found
    }








}

