using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;
public class Grenade : MonoBehaviour
{
    [Header("Explosion Prefab")]
    [SerializeField] private GameObject explosionEffectPrefab;

    [Header("Explosion Prefab")]
    [SerializeField] private GameObject flashEffectPrefab;
    [Header("Explosion Settings")]
    [SerializeField] private float explosionDelay = 3f; // delay before explosion
    [SerializeField] private float explosionForce = 700f; // force applied by explosion
    [SerializeField] private float explosionRadius = 50f; // radius of explosion
    [Header("Audio Effects")]
    private float countdown;

    private bool hasExploded = false;
    [Header("Grenade Prefab")]
    [SerializeField] private GameObject grenadePrefab; // reference to grenade prefab
    [Header("flash Grenade Prefab")]
    [SerializeField] private GameObject flashGrenadePrefab;
    public Transform grenadeSpawnPoint;

    [Header("Grenade Settings")]
    [SerializeField] private KeyCode throwKey = KeyCode.Mouse0;
    [SerializeField] private Vector3 throwDirection = new Vector3(0, 1, 0); // direction of the throw
    [Header("Grenade Force")]
    [SerializeField] private float throwForce = 10f; // force applied to throw the grenade
    [SerializeField] private Camera mainCamera; // reference to main camera
    [SerializeField] private float maxForce = 13f; // maximum force applied to throw the grenade
    private bool hasThrown = false;
    private GameObject createdGrenade = null;


    private bool isCharging = false; // flag to check if player is charging the throw
    private float chargeTime = 0f;
    [Header("Trajectory Settings")]
    [SerializeField] private LineRenderer trajectoryLine; // reference to the LineRenderer component

    [SerializeField]
    private bool isFlash = false;

  [SerializeField]
    private bool isExplodingGrenade = false;

    public bool InventoryHasGrenade=false;
    private StarterAssetsInputs starterAssetsInputs;
    public Animator PlayerAnimator;
    private float animcountdown = 1f;
    [SerializeField] private TPSController tpsController;
    public AudioSource grenadePull;
    public AudioSource grenade;
    public AudioSource flash;
    public HealthBarController healthBarController;

    public void isFlashSetter()
    {
        Debug.Log("..........................................");
        Debug.Log("isFlashSetter");
        this.isFlash = true;
        this.isExplodingGrenade = false;
        this.InventoryHasGrenade = true;
        Debug.Log("Is explodingGrenade "+isExplodingGrenade);
        Debug.Log("Is flash "+isFlash);
        Debug.Log("..........................................");
    }
    public void isExplodingGrenadeSetter()
    {
        Debug.Log("..........................................");
        Debug.Log("isExplodingGrenadeSetter");
        this.isExplodingGrenade = true;
        this.isFlash = false;
        this.InventoryHasGrenade = true;
        Debug.Log("Is explodingGrenade "+isExplodingGrenade);
        Debug.Log("Is flash "+isFlash);
        Debug.Log("..........................................");
    }
    public bool isFlashGetter(){
        return this.isFlash;
    }

    public bool isExplodingGrenadeGetter()
    {
        return this.isExplodingGrenade;
    }

    private void Start()
    {
        starterAssetsInputs=GetComponent<StarterAssetsInputs>();
        countdown = explosionDelay;
        if(InventoryCreator.getInstance().grenadeController==null){
        InventoryCreator.getInstance().setGrenadeController(this);
        }
        healthBarController= GetComponent<HealthBarController>();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     if(createdGrenade != null)
        //         Destroy(createdGrenade);
        //     createdGrenade = Instantiate(flashGrenadePrefab, grenadeSpawnPoint.position, grenadePrefab.transform.rotation);
        //     createdGrenade.transform.parent = grenadeSpawnPoint.transform;
        //     isFlash = true;
        // }
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     if (createdGrenade != null)
        //         Destroy(createdGrenade);
        //     createdGrenade = Instantiate(grenadePrefab, grenadeSpawnPoint.position, grenadePrefab.transform.rotation);
        //     createdGrenade.transform.parent = grenadeSpawnPoint.transform;
        //     isFlash = false;
        // }
        // Debug.Log("..........................................");
        // Debug.Log("Is explodingGrenade "+isExplodingGrenade);
        // Debug.Log("Is flash "+isFlash);
        // Debug.Log("has thrown "+hasThrown);
        // Debug.Log("..........................................");

        if (Input.GetKeyDown(KeyCode.G) && (isExplodingGrenade || isFlash) && !hasThrown )
        {
  
        tpsController.weapons[tpsController.WeaponIndex].gameObject.SetActive(false);
                       InputSystem.DisableDevice(Mouse.current,false);
            if (isFlash)
            {
                // if (createdGrenade != null)
                //     Destroy(createdGrenade);
                createdGrenade = Instantiate(flashGrenadePrefab, grenadeSpawnPoint.position, flashGrenadePrefab.transform.rotation);
            }
            else if (isExplodingGrenade)
            {
                // if (createdGrenade != null)
                // Destroy(createdGrenade);
                createdGrenade = Instantiate(grenadePrefab, grenadeSpawnPoint.position, grenadePrefab.transform.rotation);
            }
            createdGrenade.transform.parent = grenadeSpawnPoint.transform;

            //isFlash = false;
            hasExploded = false;
            hasThrown = false;
            isCharging = true;
            chargeTime = 0f;
            countdown = explosionDelay;
            StartThrowing();
            PlayerAnimator.SetLayerWeight(2, 0);
            PlayerAnimator.SetLayerWeight(1, 0);
            PlayerAnimator.SetLayerWeight(3, 1);
            PlayerAnimator.SetBool("HoldGrenade", true);
            PlayerAnimator.SetBool("ThrowGrenade", false);
        }
        if (isCharging)
        {
            ChargeThrow();
        }
        if (Input.GetKeyUp(KeyCode.G) && (isFlash || isExplodingGrenade))
        {
                    tpsController.weapons[tpsController.WeaponIndex].gameObject.SetActive(true);
            InputSystem.EnableDevice(Mouse.current);
            ReleaseThrow(createdGrenade);
            this.InventoryHasGrenade = false;
            hasThrown = true;
            isCharging = false;
            PlayerAnimator.SetBool("HoldGrenade", false);
            PlayerAnimator.SetBool("ThrowGrenade", true);

        }
        if (!hasExploded)
        {
            if (hasThrown)
            {
                countdown -= Time.deltaTime;
                animcountdown -= Time.deltaTime;
                if(animcountdown <= 0f)
                {
                    // PlayerAnimator.SetBool("ThrowGrenade", false);
                    PlayerAnimator.SetLayerWeight(3, 0);
                    animcountdown = 1f;
                }
                if (countdown <= 0f)
                {
                    Explode(createdGrenade);
                    hasExploded = true;
                    hasThrown = false;
                }
            }
        }
    }
    void Explode(GameObject gr)
    {
        if (isFlash)
        {
            GameObject flashEffect = Instantiate(flashEffectPrefab, gr.transform.position, Quaternion.identity);
            Destroy(flashEffect, 1f);
            flash.Play();
        }
        else
        {
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, gr.transform.position, Quaternion.identity);
            Destroy(explosionEffect, 1f);
            grenade.Play();

        }
        NearbyForceApply();
        isFlash = false;
        isExplodingGrenade = false;
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
            if (nearbyObject.CompareTag("AI"))
            {
                enemyScript enemy = nearbyObject.gameObject.GetComponent<enemyScript>();
                if (isFlash)
                {
                    enemy.KnockdeDown();
                }
                else
                {
                    enemy.TakeDamage(4);
                }
                
            }
            else if(nearbyObject.CompareTag("Player")&&isExplodingGrenade)
            {
                healthBarController.PlayerHealthSetter(healthBarController.PlayerHealthGetter() - 4);
                healthBarController.startHitAnimation();
            }
            
        }
    }
    void StartThrowing()
    {
        Vector3 offset = new Vector3(-0.05f, -0.05f, -0.05f);
        // GameObject grenade2 = Instantiate(grenadePrefab, grenadeSpawnPoint.position + offset, grenadePrefab.transform.rotation);
        // grenade2.transform.parent = grenadeSpawnPoint.transform;
        grenadePull.Play();
        trajectoryLine.enabled = true;


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