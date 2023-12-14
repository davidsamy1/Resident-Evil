using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;

    public float weaponLength = 0.8f;
    public float weaponDamage = 2;

    public bool throw1 = false;

    public HealthBarController HealthBarController;
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
            
            RaycastHit hit;

            Debug.Log("first");
            //int layerMask = 1 << 8;
            //if(hit.distance > weaponLength) { }
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength))
            {
                Debug.Log("enter first loop");

                if (hit.transform.TryGetComponent(out HealthBarController HealthBarController))
                {
                    Debug.Log("enter second loop");
                    HealthBarController.PlayerHealthSetter((HealthBarController.PlayerHealth)-2);
                    hasDealtDamage = true;
                    Debug.Log("enemy hit player");
                    HealthBarController.startHitAnimation();
                }
            }
        }

        if (throw1)
        {
            RaycastHit hit;

            Debug.Log("first");
            //int layerMask = 1 << 8;
            //if(hit.distance > weaponLength) { }
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength))
            {
                Debug.Log("enter first loop");

                if (hit.transform.TryGetComponent(out HealthBarController HealthBarController))
                {
                    Debug.Log("enter second loop");
                    HealthBarController.PlayerHealthSetter((HealthBarController.PlayerHealth) - 2);
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


}
