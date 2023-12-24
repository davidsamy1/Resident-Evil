using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAssests : MonoBehaviour
{
  public static ItemsAssests Instance{get;private set;}
    private void Awake(){
        Instance=this;
    }
    public Sprite PistolSprite;
    public Sprite ShotGunSprite;
    public Sprite AssaultRifleSprite;
    public Sprite RevolverSprite;
    public Sprite GreenHerbSprite;
    public Sprite RedHerbSprite;
    public Sprite EmblemKeySprite;
    public Sprite CardKeySprite;
    public Sprite SpadeKeySprite;
    public Sprite HeartKeySprite;
    public Sprite DiamondKeySprite;
    public Sprite ClubKeySprite;
    public Sprite HandGrenadeSprite;
    public Sprite FlashGrenadeSprite;
    public Sprite NormalGunPowderSprite;
    public Sprite HighGradeGunPowderSprite;
    public Sprite RedMixSprite;
    public Sprite GreenMixSprite;
    public Sprite RedGreenMixSprite;
    public Sprite RubyTreasureSprite;
    public Sprite GoldTreasureSprite;
    public Sprite EmeraldTreasureSprite;
    public Sprite ShotGunAmmoSprite;
    public Sprite PistolAmmoSprite;
    public Sprite AssaultRifleAmmoSprite;
    public Sprite RevolverAmmoSprite;
    public Sprite EmptySprite;
    public Sprite KnifeSprite;

}
