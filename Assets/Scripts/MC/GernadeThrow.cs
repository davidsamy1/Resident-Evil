// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GernadeThrow : MonoBehaviour
// {
//     [Header("Grenade Prefab")]
//     [SerializeField] private GameObject grenadePrefab; // reference to grenade prefab

//     public Transform grenadeSpawnPoint;

//     [Header("Grenade Settings")]
//     [SerializeField] private KeyCode throwKey = KeyCode.Mouse0;
// [SerializeField] private Vector3 throwDirection = new Vector3 (0, 1, 0); // direction of the throw
//     [Header("Grenade Force")]
//     [SerializeField] private float throwForce = 10f; // force applied to throw the grenade
//     [SerializeField] private Camera mainCamera; // reference to main camera
//     [SerializeField] private float maxForce = 10f; // maximum force applied to throw the grenade
//     private bool hasThrown=false;
//     private GameObject createdGrenade=null;
//     public GameObject newCreatedGrenade
//     {
//         get { return createdGrenade; }
       
//     }
//     public bool hasbeenThrown
//     {
//         get { return hasThrown; }
//         set { hasThrown = value; }
//     }

//     private bool isCharging = false; // flag to check if player is charging the throw
//     private float chargeTime = 0f; // time player has been charging the throw

//     void Start()
//     {
    
//     }

//     // Update is called once per frame
//     private void Update()
//     {
         
//         if (Input.GetKeyDown(throwKey))
//         {
//             createdGrenade=StartThrowing();
//         }
//         if (isCharging)
//         {
//             ChargeThrow();
//         }
//         if (Input.GetKeyUp(throwKey))
//         {

//             ReleaseThrow(createdGrenade);
//         }
//     }
//     GameObject StartThrowing()
//     {
//         Vector3 offset =new Vector3(-0.05f,-0.05f,-0.05f);
//         GameObject grenade2=Instantiate(grenadePrefab, grenadeSpawnPoint.position+offset, grenadePrefab.transform.rotation);
//         grenade2.transform.parent = grenadeSpawnPoint.transform;
//         //Pull pin sound
//         isCharging = true;
//         chargeTime = 0f;
//         return grenade2;
//         //Trajectory line
//     }
//     void ChargeThrow()
//     {
//         chargeTime += Time.deltaTime;
//         //trajectory line velocity
//     }

//     void ReleaseThrow(GameObject grenade)
//     {
//         ThrowGrenade(Mathf.Min(chargeTime * throwForce, maxForce),grenade);
//         hasbeenThrown = true;
//         isCharging = false;
//         //hide line
//     }
// void ThrowGrenade (float force, GameObject grenade)
// {
// grenade.AddComponent<Rigidbody>();
// Rigidbody rb= grenade.GetComponent<Rigidbody>();
// rb.mass=5;
// rb.drag=0.05f;
// grenade.transform.parent = null;
// Vector3 finalThrowDirection = (mainCamera.transform. forward + throwDirection). normalized;
// rb.AddForce (finalThrowDirection *force, ForceMode. VelocityChange);

// //Throwing sound
// }
// }
