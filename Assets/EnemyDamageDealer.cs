using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;

    public float weaponLength = 0.8f;
    public float weaponDamage;

    public static bool inGrapple = false;

    public enemyScript enemy;
    public bool throw1 = false;

    public HealthBarController HealthBarController;

    public HealthBarController HealthBarPlayer;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage && !hasDealtDamage)
        {
            if(!enemy.tryGrapple&& !enemy.isArmed)
            {
                weaponLength = weaponLength + 20;
            }
            RaycastHit hit;
            if (!inGrapple)
            {   
                if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength))
                {


                    if (hit.transform.TryGetComponent(out HealthBarController HealthBarController))
                    {
                        if (enemy.tryGrapple)
                        {
                            inGrapple = true;
                            Debug.Log("pllayer in grapple");
                            hasDealtDamage = true;
                            Invoke("StartHitAnimationDelayed", 5.0f);

                        }
                        else
                        {

                            HealthBarController.PlayerHealthSetter((HealthBarController.PlayerHealth) - (int)weaponDamage);
                            hasDealtDamage = true;

                            HealthBarController.startHitAnimation();
                        }

                    }
                }
            }
            weaponLength = weaponLength - 20;
        }

        if (throw1)
        {
            RaycastHit hit;

            Debug.Log("first");
            //int layerMask = 1 << 8;
            //if(hit.distance > weaponLength) { }
            int ThrowLength = 15;
            if (Physics.Raycast(transform.position, -transform.up, out hit, ThrowLength))
            {
                Debug.Log("enter first loop");

                if (hit.transform.TryGetComponent(out HealthBarController HealthBarController))
                {
                    Debug.Log("enter second loop");
                    HealthBarController.PlayerHealthSetter((HealthBarController.PlayerHealth) - 3);
                    //hasDealtDamage = true;
                    throw1 = false;
                    Debug.Log("enemy hit player");
                    HealthBarController.startHitAnimation();
                }
            }

        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }


    private void StartHitAnimationDelayed()
    {
        if((HealthBarPlayer.PlayerHealth) -5 < 0){
            HealthBarPlayer.PlayerHealthSetter(0);
        }

        else
        {
            HealthBarPlayer.PlayerHealthSetter((HealthBarPlayer.PlayerHealth) - 5);

        }
        HealthBarPlayer.startHitAnimation();
        inGrapple = false;
    }

}
