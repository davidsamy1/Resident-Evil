using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grenade : MonoBehaviour
{
    [Header("Explosion Prefab")]
    [SerializeField] private GameObject explosionEffectPrefab; // reference to explosion effect prefab
    [Header("Explosion Settings")]
    [SerializeField] private float explosionDelay = 1.75f; // delay before explosion
    [SerializeField] private float explosionForce = 700f; // force applied by explosion
    [SerializeField] private float explosionRadius = 5f; // radius of explosion
    [Header("Audio Effects")]
    private float countdown;

    private bool hasExploded = false;
    [Header("Grenade Prefab")]
    [SerializeField] private GameObject grenadePrefab; // reference to grenade prefab

    public Transform grenadeSpawnPoint;

    [Header("Grenade Settings")]
    [SerializeField] private KeyCode throwKey = KeyCode.Mouse0;
    [SerializeField] private Vector3 throwDirection = new Vector3(0, 1, 0); // direction of the throw
    [Header("Grenade Force")]
    [SerializeField] private float throwForce = 10f; // force applied to throw the grenade
    [SerializeField] private Camera mainCamera; // reference to main camera
    [SerializeField] private float maxForce = 15f; // maximum force applied to throw the grenade
    private bool hasThrown = false;
    private GameObject createdGrenade = null;

    private bool isCharging = false; // flag to check if player is charging the throw
    private float chargeTime = 0f;
    [Header("Trajectory Settings")]
    [SerializeField] private LineRenderer trajectoryLine; // reference to the LineRenderer component
    private void Start()
    {
        countdown = explosionDelay;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            hasExploded = false;
            hasThrown = false;
            isCharging = true;
            chargeTime = 0f;
            countdown = explosionDelay;
            createdGrenade = StartThrowing();
        }
        if (isCharging)
        {
            ChargeThrow();
        }
        if (Input.GetKeyUp(throwKey))
        {
            ReleaseThrow(createdGrenade);
            hasThrown = true;
            isCharging = false;
        }
        if (!hasExploded)
        {
            if (hasThrown)
            {
                countdown -= Time.deltaTime;
                if (countdown <= 0f)
                {
                    Explode(createdGrenade);
                    hasExploded = true;
                }
            }
        }
    }
    void Explode(GameObject gr)
    {

        GameObject explosionEffect = Instantiate(explosionEffectPrefab, gr.transform.position, Quaternion.identity);
        Destroy(explosionEffect, 1f);
        //Play Sound Effect
        //Affect Other Physics Objects
        // NearbyForceApply();
        Destroy(gr);
    }

    void NearbyForceApply()
    {
        Collider[] colliders = Physics.OverlapSphere(createdGrenade.transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
    GameObject StartThrowing()
    {
        Vector3 offset = new Vector3(-0.05f, -0.05f, -0.05f);
        GameObject grenade2 = Instantiate(grenadePrefab, grenadeSpawnPoint.position + offset, grenadePrefab.transform.rotation);
        grenade2.transform.parent = grenadeSpawnPoint.transform;
        //Pull pin sound
        trajectoryLine.enabled = true;
        return grenade2;

    }
    void ChargeThrow()
    {
        chargeTime += Time.deltaTime;
        Vector3 grenadeVelocity = (mainCamera.transform.forward + throwDirection).normalized * Mathf.Min(chargeTime * throwForce, maxForce);
        ShowTrajectory(grenadeSpawnPoint.position + grenadeSpawnPoint.forward, grenadeVelocity);
    }

    private void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        trajectoryLine.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + 0.5f * Physics.gravity * time * time;
        }
        trajectoryLine.SetPositions(points);
    }


    void ReleaseThrow(GameObject grenade)
    {
        ThrowGrenade(Mathf.Min(chargeTime * throwForce, maxForce), grenade);
        trajectoryLine.enabled = false;
    }
    void ThrowGrenade(float force, GameObject grenade)
    {
        grenade.AddComponent<Rigidbody>();
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.mass = 5;
        rb.drag = 0.05f;
        grenade.transform.parent = null;
        Vector3 finalThrowDirection = (mainCamera.transform.forward + throwDirection).normalized;
        rb.AddForce(finalThrowDirection * force, ForceMode.VelocityChange);

        //Throwing sound
    }
}