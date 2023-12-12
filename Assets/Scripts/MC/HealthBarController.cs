using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class HealthBarController : MonoBehaviour
{

    private int PlayerHealth=2;
    private HPLevel HpLevel;
    private bool PlayerDeath = false;
    public List<GameObject> HealthBarSegments;
    public Material SegmentInActiveMaterial;
    public Material SegmentActiveMaterial;
    private StarterAssetsInputs starterAssetsInputs;

    public Animator PlayerAnimator;

    public enum HPLevel
    {
        Low,
        Medium,
        High

    }

    // Start is called before the first frame update
    void Start()
    {
        /* //get the glowing green material
         Renderer renderer = HealthBarSegments[0].GetComponent<Renderer>();
         SegmentActiveMaterial = renderer.material;
         SegmentActiveMaterial.SetColor("_EmissionColor", Color.red);
        */
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        AdabtHPLevel();
                        InventoryCreator.getInstance().setHealthBarController(this);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus)) {
            PlayerHealthSetter(PlayerHealthGetter() + 4);

        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            PlayerHealthSetter(PlayerHealthGetter() - 1);
            PlayerAnimator.SetLayerWeight(6,1);
        
            if(PlayerHealthGetter()>0){
                PlayerAnimator.SetTrigger("hit");
                Invoke("setWeight",1.6f);
                }
            else{
                PlayerAnimator.SetTrigger("death");
                PlayerDeath = true;
                starterAssetsInputs.enabled = false;
            }

        }

    }
    void setWeight(){
        PlayerAnimator.SetLayerWeight(6,0);
    }

    void ToggleSegmentActivity(int SegmentIndex,bool active)
    {
        Renderer renderer = HealthBarSegments[SegmentIndex].GetComponent<Renderer>();
        // Remove the last material from the Renderer
        Material[] materials = renderer.materials;
        Array.Resize(ref materials, materials.Length - 1);
        renderer.materials = materials;

        Array.Resize(ref materials, materials.Length + 1);
        if(active)
            materials[materials.Length - 1] = SegmentActiveMaterial;
        else
            materials[materials.Length - 1] = SegmentInActiveMaterial;
        renderer.materials = materials;
    }

    

    void AdabtHPLevel()
    {
        if (PlayerHealth <= 2)
        {
            HpLevel=HPLevel.Low;
            SegmentActiveMaterial.SetColor("_EmissionColor", Color.red);
        }
        else if(PlayerHealth <= 4)
        {
            HpLevel = HPLevel.Medium;
            SegmentActiveMaterial.SetColor("_EmissionColor", Color.yellow);
        }
        else
        {
            HpLevel=HPLevel.High;
            SegmentActiveMaterial.SetColor("_EmissionColor", Color.green);
        }

        for(int i = 0; i < HealthBarSegments.Count; i++)
        {
            if (i > PlayerHealth - 1)
                ToggleSegmentActivity(i, false);
            else 
                ToggleSegmentActivity(i, true);
        }


    }
    public void IncreasePlayerHealth(int HP)
    {
        PlayerHealthSetter(PlayerHealthGetter() + HP);
    }

    void PlayerHealthSetter(int HP) {
        if (HP >= 0)
        {
            PlayerHealth = HP;
            if(PlayerHealth>8)
                PlayerHealth = 8;
            AdabtHPLevel();
           
        }
        PlayerDeath=PlayerHealth==0 ? true : false;
    }

    public int PlayerHealthGetter()
    {
        return PlayerHealth;
    }
    /*
    void PlayerDeathSetter(bool dead)
    {
        PlayerDeath=dead;
    }
    */
    bool PlayerDeathGetter()
    {
        return PlayerDeath;
    }
}
