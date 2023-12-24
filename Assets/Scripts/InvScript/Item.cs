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
    gold
}

static int id=10;
public Sprite sprite;
 public int sellPrice;
 public int buyPrice;
 public int quantity=1;
 public bool equipped=false;
 public int durability=10;
 public string Title;
    public string Description;
 public bool selected=false;
public ItemType itemType;


public Item( Sprite sprite, int sellPrice, int buyPrice, int quantity, ItemType itemType,string Title="",string Description=""){
    
    this.Title=Title;
    this.Description=Description;
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

public static Item setItemBasedOnType(ItemType type, int quantity=-1){
    if(type==ItemType.pistol){
        return getPistol();
    }
    else if(type==ItemType.shotGun){
        return getShotGun();
    }
    else if(type==ItemType.assaultRifle){
        return getAssaultRifle();
    }
    else if(type==ItemType.revolver){
        return getRevolver();
    }
    else if(type==ItemType.handGrenade){
        return getHandGrenade();
    }
    else if(type==ItemType.flashGrenade){
        return getFlashGrenade();
    }
    else if(type==ItemType.greenHerb){
        return getGreenHerb();
    }
    else if(type==ItemType.redHerb){
        return getRedHerb();
    }
    else if(type==ItemType.normalGunPowder){
        return getNormalGunPowder();
    }
    else if(type==ItemType.highGradeGunPowder){
        return getHighGunPowder();
    }
    else if(type==ItemType.greenMix){
        return getGreenMix();
    }
    else if(type==ItemType.redMix){
        return getRedMix();
    }
    else if(type==ItemType.redGreenMix){
        return getRedGreenMix();
    }
    else if(type==ItemType.shotGunAmmo){
        return getShotGunAmmo(quantity!=-1?quantity:8);
    }
    else if(type==ItemType.pistolAmmo){
       
        return getPistolAmmo(quantity!=-1?quantity:12);

        
    }
    else if(type==ItemType.assaultRifleAmmo){
        return getAssaultRifleAmmo(quantity!=-1?quantity:30);
    }
    else if(type==ItemType.revolverAmmo){
        return getRevolverAmmo(quantity!=-1?quantity:6);
    }
    else if(type==ItemType.rubyTreasure){
        return getRubyTreasure();
    }
    else if(type==ItemType.goldTreasure){
        return getGoldTreasure();
    }
    else if(type==ItemType.emeraldTreasure){
        return getEmeraldTreasure();
    }
    else if(type==ItemType.emblemKey){
        return getEmblemKey();
    }
    else if(type==ItemType.cardKey){
        return getCardKey();
    }
    else if(type==ItemType.spadeKey){
        return getSpadeKey();
    }
    else if(type==ItemType.heartKey){
        return getHeartKey();
    }
    else if(type==ItemType.diamondKey){
        return getDiamondKey();
    }
    else if(type==ItemType.clubKey){
        return getClubKey();
    
}
else{
    return null;
}
}



public static Item getPistol(){
   Sprite PistolSprite=ItemsAssests.Instance.PistolSprite;
    int sellPrice=-1;
    int buyPrice=-1;
    int quantity=1;
    string Title="Pistol";
    string Description="A pistol that can be used to shoot enemies";
    ItemType itemType=ItemType.pistol;
    return new Item(PistolSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
 }
 
    public static Item getShotGun(){
    Sprite ShotGunSprite=ItemsAssests.Instance.ShotGunSprite;
    int sellPrice=-1;
    int buyPrice=140;
    int quantity=1;
    string Title="ShotGun";
    string Description="A shotgun that can be used to shoot enemies";
    ItemType itemType=ItemType.shotGun;
    return new Item(ShotGunSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getAssaultRifle(){
        Sprite AssaultRifleSprite=ItemsAssests.Instance.AssaultRifleSprite;
        int sellPrice=-1;
        int buyPrice=150;
        int quantity=1;
        string Title="AssaultRifle";
        string Description="A AssaultRifle that can be used to shoot enemies";
        ItemType itemType=ItemType.assaultRifle;
        return new Item(AssaultRifleSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }

    public static Item getRevolver(){
        Sprite RevolverSprite=ItemsAssests.Instance.RevolverSprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        string Title="Revolver";
        string Description="A Revolver that can be used to shoot enemies";
        ItemType itemType=ItemType.revolver;
        return new Item(RevolverSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getHandGrenade(){
        Sprite HandGrenadeSprite=ItemsAssests.Instance.HandGrenadeSprite;
        int sellPrice=10;
        int buyPrice=15;
        int quantity=1;
        string Title="Hand Grenade";
        string Description="Throw it to kill enemies in a radius to receive 4 damage but be careful it can also damage you";
        ItemType itemType=ItemType.handGrenade;
        return new Item(HandGrenadeSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getFlashGrenade(){
        Sprite FlashGrenadeSprite=ItemsAssests.Instance.FlashGrenadeSprite;
        int sellPrice=10;
        int buyPrice=15;
        int quantity=1;
        string Title="Flash Grenade";
        string Description="Throw it to knocked down enemies in a radius  allowing you to escape or use the knife to kill the knocked down enemies";
        ItemType itemType=ItemType.flashGrenade;
        return new Item(FlashGrenadeSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getGreenHerb(){
        Sprite GreenHerbSprite=ItemsAssests.Instance.GreenHerbSprite;
        int sellPrice=15;
        int buyPrice=20;    
        int quantity=1;
        string Title="Green Herb";
        string Description="A herb that can be used to heal 2 health or make a special mixtures";
        ItemType itemType=ItemType.greenHerb;
        return new Item(GreenHerbSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getRedHerb(){
        Sprite RedHerbSprite=ItemsAssests.Instance.RedHerbSprite;
        int sellPrice=5;
        int buyPrice=10;
        int quantity=1;
        string Title="Red Herb";
        string Description="A herb that can be used to make special mixtures";
        ItemType itemType=ItemType.redHerb;
        return new Item(RedHerbSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getNormalGunPowder(){
        Sprite NormalGunPowderSprite=ItemsAssests.Instance.NormalGunPowderSprite;
        int sellPrice=5;
        int buyPrice=10;
        int quantity=1;
        string Title="Normal GunPowder";
        string Description="A gun powder that can be used to make ammo";
        ItemType itemType=ItemType.normalGunPowder;
        return new Item(NormalGunPowderSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getHighGunPowder(){
        Sprite HighGradeGunPowderSprite=ItemsAssests.Instance.HighGradeGunPowderSprite;
        int sellPrice=15;
        int buyPrice=20;
        int quantity=1;
        string Title="High Grade GunPowder";
        string Description="A gun powder that can be used to make Assault Rifle ammo";
        ItemType itemType=ItemType.highGradeGunPowder;
        return new Item(HighGradeGunPowderSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getGreenMix(){
        Sprite GreenMixSprite=ItemsAssests.Instance.GreenMixSprite;
        int sellPrice=30;
        int buyPrice=-1;
        int quantity=1;
        string Title="Green Mix";
        string Description="A mixture that can be used to heal 6 health";
        ItemType itemType=ItemType.greenMix;
        return new Item(GreenMixSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getRedMix(){
        Sprite RedMixSprite=ItemsAssests.Instance.RedMixSprite;
        int sellPrice=10;
        int buyPrice=-1;
        int quantity=1;
        string Title="Red Mix";
        ItemType itemType=ItemType.redMix;
        return new Item(RedMixSprite,sellPrice,buyPrice,quantity,itemType,Title);
    }
    public static Item getRedGreenMix(){
        Sprite RedGreenMixSprite=ItemsAssests.Instance.RedGreenMixSprite;
        int sellPrice=20;
        int buyPrice=-1;
        int quantity=1;
        string Title="RedGreen Mix";
        string Description="A mixture that can be used to heal 8 health";
        ItemType itemType=ItemType.redGreenMix;
        return new Item(RedGreenMixSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }

    public static Item getShotGunAmmo(int Quantity=8){
        Sprite ShotGunAmmoSprite=ItemsAssests.Instance.ShotGunAmmoSprite;
        int sellPrice=-1;
        int buyPrice=40;
        int quantity=Quantity;
        string Title="ShotGun Ammo";
        string Description="Ammo for the shotgun";
        ItemType itemType=ItemType.shotGunAmmo;
        return new Item(ShotGunAmmoSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getPistolAmmo(int Quantity=12){
        Sprite PistolAmmoSprite=ItemsAssests.Instance.PistolAmmoSprite;
        int sellPrice=-1;
        int buyPrice=30;
        int quantity=Quantity;
        string Title="Pistol Ammo";
        string Description="Ammo for the pistol";
        ItemType itemType=ItemType.pistolAmmo;
        return new Item(PistolAmmoSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getAssaultRifleAmmo(int Quantity=30){
        Sprite AssaultRifleAmmoSprite=ItemsAssests.Instance.AssaultRifleAmmoSprite;
        int sellPrice=-1;
        int buyPrice=50;
        int quantity=Quantity;
        ItemType itemType=ItemType.assaultRifleAmmo;
        string Title="AssaultRifle Ammo";
        string Description="Ammo for the AssaultRifle";
        return new Item(AssaultRifleAmmoSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getRevolverAmmo(int Quantity=6){
        Sprite RevolverAmmoSprite=ItemsAssests.Instance.RevolverAmmoSprite;
        int sellPrice=-1;
        int buyPrice=70;
        int quantity=Quantity;
        ItemType itemType=ItemType.revolverAmmo;
        string Title="RevolverAmmo";
        string Description="Ammo for the Revolver";
        return new Item(RevolverAmmoSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }

    public static Item getRubyTreasure(){
        Sprite RubyTreasureSprite=ItemsAssests.Instance.RubyTreasureSprite;
        int sellPrice=200;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.rubyTreasure;
        string Title="Ruby Treasure";
        string Description="Sell it to get a fortune!";
        return new Item(RubyTreasureSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getGoldTreasure(){
        Sprite GoldTreasureSprite=ItemsAssests.Instance.GoldTreasureSprite;
        int sellPrice=100;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.goldTreasure;
        string Title="Gold Treasure";
        string Description="Sell it to get a fortune!";
        return new Item(GoldTreasureSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getEmeraldTreasure(){
        Sprite EmeraldTreasureSprite=ItemsAssests.Instance.EmeraldTreasureSprite;
        int sellPrice=500;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.emeraldTreasure;
        string Title="Emerald Treasure";
        string Description="Sell it to get a fortune!";
        return new Item(EmeraldTreasureSprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getEmblemKey(){
        Sprite EmblemKeySprite=ItemsAssests.Instance.EmblemKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.emblemKey;
        string Title="Emblem Key";
        string Description="A key that can be used to open a door";
        return new Item(EmblemKeySprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getCardKey(){
        Sprite CardKeySprite=ItemsAssests.Instance.CardKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.cardKey;
        string Title="Card Key";
        string Description="A key that can be used to open a door";
        return new Item(CardKeySprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getSpadeKey(){
        Sprite SpadeKeySprite=ItemsAssests.Instance.SpadeKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.spadeKey;
        string Title="Spade Key";
        string Description="A key that can be used to open a door";
        return new Item(SpadeKeySprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getHeartKey(){
        Sprite HeartKeySprite=ItemsAssests.Instance.HeartKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.heartKey;
        string Title="Heart Key";
        string Description="A key that can be used to open a door";
        return new Item(HeartKeySprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getDiamondKey(){
        Sprite DiamondKeySprite=ItemsAssests.Instance.DiamondKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.diamondKey;
        string Title="Diamond Key";
        string Description="A key that can be used to open a door";
        return new Item(DiamondKeySprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
    }
    public static Item getClubKey(){
        Sprite ClubKeySprite=ItemsAssests.Instance.ClubKeySprite;
        int sellPrice=-1;
        int buyPrice=-1;
        int quantity=1;
        ItemType itemType=ItemType.clubKey;
        string Title="Club Key";
        string Description="A key that can be used to open a door";
        return new Item(ClubKeySprite,sellPrice,buyPrice,quantity,itemType,Title,Description);
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
