using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStats : MonoBehaviour
{
    private Inventory inventory;
    private int PlayerGold;
    private int PlayerHealth;
    private Transform EquipedItems;
    private Transform GoldText;
    private Transform HealthText;
     private Transform WeaponEquiped;
    private Transform AmmoEquiped;
        private Transform GrenadeEquiped;
    private Transform knifeEquiped;

   
  
  



    // Start is called before the first frame update
    
    public void setInventory(Inventory inventory){
        this.inventory=inventory;
    }
    public void setPlayerGold(int PlayerGold){
        this.PlayerGold=PlayerGold;
    }
    public void setPlayerHealth(int PlayerHealth){
        this.PlayerHealth=PlayerHealth;
    }

    void Awake()
    {
        EquipedItems=transform.Find("Equiped");
        GoldText=transform.Find("goldText");
        HealthText=transform.Find("healthText");
        WeaponEquiped=transform.Find("Equiped").Find("weaponEq");
        AmmoEquiped=transform.Find("Equiped").Find("weaponAmmoEq");
        GrenadeEquiped=transform.Find("Equiped").Find("grenadeEq");
        knifeEquiped=transform.Find("Equiped").Find("knifeEq");

     
    }

public void RefreshPlayerStates(){
    /* 
    1. Refresh Gold by getting the Gold textmesh from the gold text game 
    2. Refresh Health by getting the Health textmesh from the health text game
    3. Refresh Equiped items by 
       1.Loop on the inventory and check the equipped items
       2.If the equipped itemType is a weapon(shotGun/assaultRifle/Revolver/Pistol) then 
         1.Access to the (item) component inside the WeaponEquiped game object and change its image to the weapon 
         2.Access to the (item) component inside the AmmoEquiped game object and change its image to the ammo image
         2.Access to the (amount) component inside the AmmoEquiped game object and change its textmesh to the ammo quantity and set it to active
        3.If the equipped itemType is a grenade(hand/flash) then 
         1.Access to the (item) component inside the GrenadeEquiped game object and change its image to the grenade image
        
        4.If the equipped itemType is a knife then 
         1.Access to the (item) component inside the knifeEquiped game object and change its image to the knife image
         2.Access to the (amount) component inside the knifeEquiped game object and change its textmesh to the knife durability and set it to active
        
        5.Add a flag for each itemType of weapon/grenade/knife if the flag of any is false then 
         1.Access to the (item) component inside the WeaponEquiped game object and change its image to empty spire
         2.Access to the (item) component inside the AmmoEquiped game object and change its image to empty spire
         3.Access to the (amount) component inside the AmmoEquiped/WeaponEquiped and set it to inactive 
    
    */

GoldText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Gold : "+100);
HealthText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Health : "+100);    
bool weaponFlag=false;
bool revolverFlag=false;
bool grenadeFlag=false;
Transform WeaponImageCont=WeaponEquiped.Find("item");
Transform AmmoImageCont=AmmoEquiped.Find("item");
Transform AmmoAmountCont=AmmoEquiped.Find("amount");
Transform GrenadeImageCont=GrenadeEquiped.Find("item");
Transform KnifeImageCont=knifeEquiped.Find("item");
Transform KnifeDurabilityCont=knifeEquiped.Find("amount");
//Adding knife to the UI and updateing its durability
Item knife=inventory.Knife;
int KnifeDurability=knife.durability;
KnifeImageCont.GetComponent<UnityEngine.UI.Image>().sprite=knife.sprite;
KnifeDurabilityCont.GetComponent<TMPro.TextMeshProUGUI>().SetText(KnifeDurability.ToString());
KnifeDurabilityCont.gameObject.SetActive(true);
//

foreach(Item inventoryItem in inventory.GetItemList()){
    if(inventoryItem!=null&&inventoryItem.equipped==true){
        if(inventoryItem.itemType==Item.ItemType.pistol){
            weaponFlag=true;
            int AmmoAmount=inventory.getItemAmount(Item.ItemType.pistolAmmo);
            WeaponImageCont.GetComponent<UnityEngine.UI.Image>().sprite=inventoryItem.sprite;
                AmmoImageCont.GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.PistolAmmoSprite;
                AmmoAmountCont.GetComponent<TMPro.TextMeshProUGUI>().SetText(AmmoAmount.ToString());
                AmmoAmountCont.gameObject.SetActive(true);
                
            }
            else if(inventoryItem.itemType==Item.ItemType.shotGun){
                weaponFlag=true;
                int AmmoAmount=inventory.getItemAmount(Item.ItemType.shotGunAmmo);
                WeaponImageCont.GetComponent<UnityEngine.UI.Image>().sprite=inventoryItem.sprite;
                AmmoImageCont.GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.ShotGunAmmoSprite;
                AmmoAmountCont.GetComponent<TMPro.TextMeshProUGUI>().SetText(AmmoAmount.ToString());
                AmmoAmountCont.gameObject.SetActive(true);
            }
            else if(inventoryItem.itemType==Item.ItemType.assaultRifle){
                weaponFlag=true;
                int AmmoAmount=inventory.getItemAmount(Item.ItemType.assaultRifleAmmo);
                WeaponImageCont.GetComponent<UnityEngine.UI.Image>().sprite=inventoryItem.sprite;
                AmmoImageCont.GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.AssaultRifleAmmoSprite;
                AmmoAmountCont.GetComponent<TMPro.TextMeshProUGUI>().SetText(AmmoAmount.ToString());
                AmmoAmountCont.gameObject.SetActive(true);
            }
            else if(inventoryItem.itemType==Item.ItemType.revolver){
                weaponFlag=true;
                revolverFlag=true;
                int AmmoAmount=inventory.getItemAmount(Item.ItemType.revolverAmmo);
                WeaponImageCont.GetComponent<UnityEngine.UI.Image>().sprite=inventoryItem.sprite;
                AmmoImageCont.GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.RevolverAmmoSprite;
                AmmoAmountCont.GetComponent<TMPro.TextMeshProUGUI>().SetText(AmmoAmount.ToString());
                AmmoAmountCont.gameObject.SetActive(true);
            }
            else if(inventoryItem.itemType==Item.ItemType.handGrenade){
                grenadeFlag=true;
                GrenadeImageCont.GetComponent<UnityEngine.UI.Image>().sprite=inventoryItem.sprite;
            }
            else if(inventoryItem.itemType==Item.ItemType.flashGrenade){
                grenadeFlag=true;
                GrenadeImageCont.GetComponent<UnityEngine.UI.Image>().sprite=inventoryItem.sprite;
            }
        
           
        }

        
    }

//Clean up the Equiped items
if(weaponFlag==false){
    WeaponImageCont.GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.EmptySprite;
    AmmoImageCont.GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.EmptySprite;
    AmmoAmountCont.gameObject.SetActive(false);

}
if(grenadeFlag==false){
    GrenadeImageCont.GetComponent<UnityEngine.UI.Image>().sprite=ItemsAssests.Instance.EmptySprite;
}


}



}