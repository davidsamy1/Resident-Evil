using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item{

public enum ItemType{
    pistol,
    shotGun,
    assaultRifle,
    revolver,
    knife,
    greenHerb,
    redHerb,
    emblemKey,
    cardKey,
    spadeKey,
    heartKey,
    diamondKey,
    clubKey,
    handGrenade,
    flashGrenade,
    normalGunPowder,
    highGradeGunPowder,
    redMix,
    greenMix,
    redGreenMix,
    rubyTreasure,
    goldTreasure,
    emeraldTreasure,
    shotGunAmmo,
    pistolAmmo,
    assaultRifleAmmo,
    revolverAmmo,
}

static int id=10;
public Sprite sprite;
 public int sellPrice;
 public int buyPrice;
 public int quantity=1;
 public bool equipped=false;
 public int durability=10;
 public bool selected=false;
public ItemType itemType;


public Item( Sprite sprite, int sellPrice, int buyPrice, int quantity, ItemType itemType){
    
    this.sprite=sprite;
    this.sellPrice = sellPrice;
    this.buyPrice = buyPrice;
    this.quantity = quantity;
    this.itemType=itemType;
     if(itemType==(ItemType.shotGunAmmo)){
        id=1;
        //Add shotGunAmmo class
    }
    else if(itemType==(ItemType.pistolAmmo)){
        id=2;
        //Add pistolAmmo class
    }
    else if(itemType==(ItemType.assaultRifleAmmo)){
        id=3;
        //Add aRAmmo class
    }
    else if(itemType==(ItemType.revolverAmmo)){
        id=4;
        //Add revolverAmmo class
    }
    else{
        id=id+1;
    }
   
   
}

public static Item getPistol(){
   Sprite PistolSprite=ItemsAssests.Instance.PistolSprite;
    int sellPrice=-1;
    int buyPrice=-1;
    int quantity=1;
    ItemType itemType=ItemType.pistol;
    return new Item(PistolSprite,sellPrice,buyPrice,quantity,itemType);
 }
 
    public static Item getShotGun(){
    Sprite ShotGunSprite=ItemsAssests.Instance.ShotGunSprite;
    int sellPrice=-1;
    int buyPrice=140;
    int quantity=1;
    ItemType itemType=ItemType.shotGun;
    return new Item(ShotGunSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getAssaultRifle(){
        Sprite AssaultRifleSprite=ItemsAssests.Instance.AssaultRifleSprite;
        int sellPrice=-1;
        int buyPrice=150;
        int quantity=1;
        ItemType itemType=ItemType.assaultRifle;
        return new Item(AssaultRifleSprite,sellPrice,buyPrice,quantity,itemType);
    }

    public static Item getRevolver(){
        Sprite RevolverSprite=ItemsAssests.Instance.RevolverSprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.revolver;
        return new Item(RevolverSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getHandGrenade(){
        Sprite HandGrenadeSprite=ItemsAssests.Instance.HandGrenadeSprite;
        int sellPrice=10;
        int buyPrice=15;
        int quantity=1;
        ItemType itemType=ItemType.handGrenade;
        return new Item(HandGrenadeSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getFlashGrenade(){
        Sprite FlashGrenadeSprite=ItemsAssests.Instance.FlashGrenadeSprite;
        int sellPrice=10;
        int buyPrice=15;
        int quantity=1;
        ItemType itemType=ItemType.flashGrenade;
        return new Item(FlashGrenadeSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getGreenHerb(){
        Sprite GreenHerbSprite=ItemsAssests.Instance.GreenHerbSprite;
        int sellPrice=15;
        int buyPrice=20;    
        int quantity=1;
        ItemType itemType=ItemType.greenHerb;
        return new Item(GreenHerbSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getRedHerb(){
        Sprite RedHerbSprite=ItemsAssests.Instance.RedHerbSprite;
        int sellPrice=5;
        int buyPrice=10;
        int quantity=1;
        ItemType itemType=ItemType.redHerb;
        return new Item(RedHerbSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getNormalGunPowder(){
        Sprite NormalGunPowderSprite=ItemsAssests.Instance.NormalGunPowderSprite;
        int sellPrice=5;
        int buyPrice=10;
        int quantity=1;
        ItemType itemType=ItemType.normalGunPowder;
        return new Item(NormalGunPowderSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getHighGunPowder(){
        Sprite HighGradeGunPowderSprite=ItemsAssests.Instance.HighGradeGunPowderSprite;
        int sellPrice=15;
        int buyPrice=20;
        int quantity=1;
        ItemType itemType=ItemType.highGradeGunPowder;
        return new Item(HighGradeGunPowderSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getGreenMix(){
        Sprite GreenMixSprite=ItemsAssests.Instance.GreenMixSprite;
        int sellPrice=30;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.greenMix;
        return new Item(GreenMixSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getRedMix(){
        Sprite RedMixSprite=ItemsAssests.Instance.RedMixSprite;
        int sellPrice=10;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.redMix;
        return new Item(RedMixSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getRedGreenMix(){
        Sprite RedGreenMixSprite=ItemsAssests.Instance.RedGreenMixSprite;
        int sellPrice=20;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.redGreenMix;
        return new Item(RedGreenMixSprite,sellPrice,buyPrice,quantity,itemType);
    }

    public static Item getShotGunAmmo(){
        Sprite ShotGunAmmoSprite=ItemsAssests.Instance.ShotGunAmmoSprite;
        int sellPrice=-1;
        int buyPrice=40;
        int quantity=8;
        ItemType itemType=ItemType.shotGunAmmo;
        return new Item(ShotGunAmmoSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getPistolAmmo(){
        Sprite PistolAmmoSprite=ItemsAssests.Instance.PistolAmmoSprite;
        int sellPrice=-1;
        int buyPrice=30;
        int quantity=12;
        ItemType itemType=ItemType.pistolAmmo;
        return new Item(PistolAmmoSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getAssaultRifleAmmo(){
        Sprite AssaultRifleAmmoSprite=ItemsAssests.Instance.AssaultRifleAmmoSprite;
        int sellPrice=-1;
        int buyPrice=50;
        int quantity=30;
        ItemType itemType=ItemType.assaultRifleAmmo;
        return new Item(AssaultRifleAmmoSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getRevolverAmmo(){
        Sprite RevolverAmmoSprite=ItemsAssests.Instance.RevolverAmmoSprite;
        int sellPrice=-1;
        int buyPrice=70;
        int quantity=6;
        ItemType itemType=ItemType.revolverAmmo;
        return new Item(RevolverAmmoSprite,sellPrice,buyPrice,quantity,itemType);
    }

    public static Item getRubyTreasure(){
        Sprite RubyTreasureSprite=ItemsAssests.Instance.RubyTreasureSprite;
        int sellPrice=200;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.rubyTreasure;
        return new Item(RubyTreasureSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getGoldTreasure(){
        Sprite GoldTreasureSprite=ItemsAssests.Instance.GoldTreasureSprite;
        int sellPrice=100;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.goldTreasure;
        return new Item(GoldTreasureSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getEmeraldTreasure(){
        Sprite EmeraldTreasureSprite=ItemsAssests.Instance.EmeraldTreasureSprite;
        int sellPrice=500;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.emeraldTreasure;
        return new Item(EmeraldTreasureSprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getEmblemKey(){
        Sprite EmblemKeySprite=ItemsAssests.Instance.EmblemKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.emblemKey;
        return new Item(EmblemKeySprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getCardKey(){
        Sprite CardKeySprite=ItemsAssests.Instance.CardKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.cardKey;
        return new Item(CardKeySprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getSpadeKey(){
        Sprite SpadeKeySprite=ItemsAssests.Instance.SpadeKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.spadeKey;
        return new Item(SpadeKeySprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getHeartKey(){
        Sprite HeartKeySprite=ItemsAssests.Instance.HeartKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.heartKey;
        return new Item(HeartKeySprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getDiamondKey(){
        Sprite DiamondKeySprite=ItemsAssests.Instance.DiamondKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.diamondKey;
        return new Item(DiamondKeySprite,sellPrice,buyPrice,quantity,itemType);
    }
    public static Item getClubKey(){
        Sprite ClubKeySprite=ItemsAssests.Instance.ClubKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.clubKey;
        return new Item(ClubKeySprite,sellPrice,buyPrice,quantity,itemType);
    }
    

 
  

    


    
    // Start is called before the first frame update
    void Start()
    {
        // GetPistol();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
