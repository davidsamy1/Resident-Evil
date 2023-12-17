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

    public InteractionController interactionController;

    public bool inGrapple = false;

    public enemyScript enemy;
    public bool throw1 = false;

    public HealthBarController HealthBarPlayer;
    public Transform player;
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
            // RaycastHit hit;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            // Debug.Log("distance to player: " + distanceToPlayer);
            if (distanceToPlayer > 2) { return; }
            if (!inGrapple)
            {
                Debug.Log("enemy hit player");
                if (enemy.tryGrapple)
                {
                    inGrapple = interactionController.isPlayerInGrapple= true;
                    Debug.Log("pllayer in grapple");
                    hasDealtDamage = true;
                    Invoke("StartHitAnimationDelayed", 4.0f);
                }
                else
                {

                    HealthBarPlayer.PlayerHealthSetter((HealthBarPlayer.PlayerHealth) - (int)weaponDamage);
                    hasDealtDamage = true;
                    HealthBarPlayer.startHitAnimation();
                }
            }
        }

        if (throw1)
        {
            RaycastHit hit;
            Debug.Log("first");
            //int layerMask = 1 << 8;
            //if(hit.distance > weaponLength) { }
            int ThrowLength = 30;
            if (Physics.Raycast(transform.position, -transform.up, out hit, ThrowLength))
            {
                Debug.Log("enter first loop");

                if (hit.transform.TryGetComponent(out HealthBarController HealthBarPlayer))
                {
                    Debug.Log("enter second loop");
                    HealthBarPlayer.PlayerHealthSetter((HealthBarPlayer.PlayerHealth) - 3);
                    //hasDealtDamage = true;
                    throw1 = false;
                    Debug.Log("enemy hit player");
                    HealthBarPlayer.startHitAnimation();
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
        if ((HealthBarPlayer.PlayerHealth) - 5 < 0)
        {
            HealthBarPlayer.PlayerHealthSetter(0);
        }

        else
        {
            HealthBarPlayer.PlayerHealthSetter((HealthBarPlayer.PlayerHealth) - 5);

        }
        HealthBarPlayer.startHitAnimation();
        inGrapple = interactionController.isPlayerInGrapple = false;
    }

}
