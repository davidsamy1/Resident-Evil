using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class TPSController : MonoBehaviour
{
    public CinemachineVirtualCamera ADSCamera;
    public float NormalSens;
    public float ADSSens;
    public UnityEngine.UI.Image crosshair;
    public LayerMask aimColliderLayerMask;
    public Transform aimdebug1;
    public Transform aimdebug2;
    public Animator PlayerAnimator;
    // public Animator AK47Animator;
    public List<GameObject> weapons;
    public int WeaponIndex = 1;
    public GameObject leon;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    public List<Weopen> weaponsScriptableObjects;
    public TMP_Text WeaponAmmo;
    public GameObject muzzleFlash;
    private bool canFire = true;
    private bool isReloading = false;
    public GameObject BulletHole;
    private RaycastHit BulletCollider;
    public TMP_Text CurrentAmmo;
    public TMP_Text InventoryAmmo;
    public List<GameObject> WeaponsHUD;
    private Coroutine reloadCoroutine = null;
    public AudioSource pistolReload;
    public AudioSource revolverReload;
    public AudioSource rifleReload;
    public AudioSource shotgunReload;
    public AudioSource pistolFire;
    public AudioSource revolverFire;
    public AudioSource rifleFire;
    public AudioSource shotgunFire;
    public AudioSource hitWallSFX;
    public AudioSource noAmmo;

    public UIVisibility UIVisibility;


    public bool isPlayerInGrapple = false;
    public bool isPlayerInGrappleStabAnimation = false;

    private int knifeDurability = 10;

    private bool isInvincible = false;
    private bool isSlowMotion = false;
    public bool isInvincibleGetter()
    {
        return isInvincible;
    }

    public RaycastHit getBulletCollider()
    {
        return BulletCollider;
    }


    // Start is called before the first frame update
    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        ResetWeaponsInfo();
        WeaponAmmo.text = weaponsScriptableObjects[WeaponIndex - 1].currentAmmoInClip.ToString();
        InventoryCreator.getInstance().setTPSController(this);


    }

    // Update is called once per frame
    private void Update()
    {
        if (UIVisibility.isInventoryOpened || UIVisibility.isStoreOpened || UIVisibility.isPaused)
            return;
        if (isPlayerInGrapple || isPlayerInGrappleStabAnimation)
        {
            InputSystem.DisableDevice(Keyboard.current, false);
            PlayerAnimator.SetBool("PlayerInGrapple", true);
        }
        else
        {
            InputSystem.EnableDevice(Keyboard.current);
            PlayerAnimator.SetBool("PlayerInGrapple", false);

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInvincible = !isInvincible;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            isSlowMotion = !isSlowMotion;
            if (isSlowMotion)
                Time.timeScale = 0.5f;
            else
                Time.timeScale = 1f;
        }
        if (WeaponIndex != 0)
        {
            CurrentAmmo.text = weaponsScriptableObjects[WeaponIndex - 1].currentAmmoInClip.ToString();
            InventoryAmmo.text = "/" + weaponsScriptableObjects[WeaponIndex - 1].totalAmmoInInventory.ToString();
        }
        else
        {
            CurrentAmmo.text = "0";
            InventoryAmmo.text = "/" + "0";
        }
        //handle crosshair position (later will detect the enemies)
        Vector3 mouseWorldPosition1 = Vector3.zero;
        Vector3 mouseWorldPosition2 = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2((Screen.width / 2f), Screen.height / 2f);
        Vector2 screenCenterPointShifted = new Vector2((Screen.width / 2f) + 180, (Screen.height / 2f) - 20);
        Ray ray1 = Camera.main.ScreenPointToRay(screenCenterPoint);
        Ray ray2 = Camera.main.ScreenPointToRay(screenCenterPointShifted);


        if (Physics.Raycast(ray1, out RaycastHit raycastHitBullet, 999f, aimColliderLayerMask))
        {
            aimdebug1.position = raycastHitBullet.point;
            mouseWorldPosition1 = raycastHitBullet.point;
            BulletCollider = raycastHitBullet;
        }

        if (Physics.Raycast(ray2, out RaycastHit raycastHit2, 999f, aimColliderLayerMask))
        {
            aimdebug2.position = raycastHit2.point;
            mouseWorldPosition2 = raycastHit2.point;
        }

        //if right click (ADS)
        if (starterAssetsInputs.aim && WeaponIndex != 0)
        {
            StartADS(mouseWorldPosition2);
        }
        else
        {
            EndADS();
        }
        /*
        //if the player shoots with knife
        if(starterAssetsInputs.shoot && WeaponIndex == 0)
        {

        }
        */

        //if the equiped gun is rifle or shotgun
        if (WeaponIndex == 3 || WeaponIndex == 4)
        {
            PlayerAnimator.SetLayerWeight(2, 1);
        }

        // if (Input.GetKeyDown(KeyCode.Alpha0))
        // {
        //     SetWeaponIndex(0);
        //     if (reloadCoroutine != null)
        //         StopCoroutine(reloadCoroutine);
        //     PlayerAnimator.SetBool("isReload", false);
        //     PlayerAnimator.SetBool("PistolReload", false);
        //     PlayerAnimator.SetLayerWeight(4, 0);
        //     isReloading = false;
        //     PlayerAnimator.speed = 1;
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     SetWeaponIndex(1);
        //     if (reloadCoroutine != null)
        //         StopCoroutine(reloadCoroutine);
        //     PlayerAnimator.SetBool("isReload", false);
        //     PlayerAnimator.SetBool("PistolReload", false);
        //     PlayerAnimator.SetLayerWeight(4, 0);
        //     isReloading = false;
        //     PlayerAnimator.speed = 1;
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     SetWeaponIndex(2);
        //     if (reloadCoroutine != null)
        //         StopCoroutine(reloadCoroutine);
        //     PlayerAnimator.SetBool("isReload", false);
        //     PlayerAnimator.SetBool("PistolReload", false);
        //     PlayerAnimator.SetLayerWeight(4, 0);
        //     isReloading = false;
        //     PlayerAnimator.speed = 1;
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     SetWeaponIndex(3);
        //     if (reloadCoroutine != null)
        //         StopCoroutine(reloadCoroutine);
        //     PlayerAnimator.SetBool("isReload", false);
        //     PlayerAnimator.SetBool("PistolReload", false);
        //     PlayerAnimator.SetLayerWeight(4, 0);
        //     isReloading = false;
        //     PlayerAnimator.speed = 1;
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha4))
        // {
        //     SetWeaponIndex(4);
        //     if (reloadCoroutine != null)
        //         StopCoroutine(reloadCoroutine);
        //     PlayerAnimator.SetBool("isReload", false);
        //     PlayerAnimator.SetBool("PistolReload", false);
        //     PlayerAnimator.SetLayerWeight(4, 0);
        //     isReloading = false;
        //     PlayerAnimator.speed = 1;
        // }

        if (WeaponIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                isReloading = true;
                reload();
            }
            if (Input.GetKey(KeyCode.Mouse0) && weaponsScriptableObjects[WeaponIndex - 1].firingMode == Weopen.FiringMode.Automatic && starterAssetsInputs.aim)
            {
                if (canFire & !isReloading)
                {
                    fire();
                    // AK47Animator.SetBool("fire", true);
                    StartCoroutine(FireCooldown());
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && weaponsScriptableObjects[WeaponIndex - 1].firingMode == Weopen.FiringMode.SingleShot && starterAssetsInputs.aim)
            {
                if (canFire & !isReloading)
                {
                    fire();
                    StartCoroutine(FireCooldown());
                }

            }
        }
    }

    private void StartADS(Vector3 mouseWorldPosition)
    {
        if (isPlayerInGrapple || isPlayerInGrappleStabAnimation)
        {
            return;
        }
        // print(BulletCollider.distance);
        //adabt the camera to the ADS camera
        ADSCamera.gameObject.SetActive(true);
        thirdPersonController.setSens(ADSSens);
        crosshair.gameObject.SetActive(true);

        //make the player rotate with the mouse movements
        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        //play ADS animation
        PlayADSAnimation();


        //if the player shoots while aiming
        if (starterAssetsInputs.shoot)
        {

            //will be changed for the diff weapons
            starterAssetsInputs.shoot = false;
        }

        //leon.GetComponent<RigBuilder>().layers[0].active = true;
    }

    private void EndADS()
    {
        ADSCamera.gameObject.SetActive(false);
        thirdPersonController.setSens(NormalSens);
        crosshair.gameObject.SetActive(false);
        if (!isReloading)
            PlayerAnimator.SetLayerWeight(1, 0);
        PlayerAnimator.SetLayerWeight(2, 0);
        PlayerAnimator.SetBool("ADS", false);

        //leon.GetComponent<RigBuilder>().layers[0].active = false;
    }

    private void PlayADSAnimation()
    {

        if (WeaponIndex == 3 || WeaponIndex == 4)
        {
            PlayerAnimator.SetLayerWeight(2, 1);
            PlayerAnimator.SetLayerWeight(1, 0);
        }

        else
        {
            PlayerAnimator.SetLayerWeight(2, 0);
            PlayerAnimator.SetLayerWeight(1, 1);
        }
        PlayerAnimator.SetBool("ADS", true);


    }

    public void SetWeaponIndex(int index)
    {
        //disable the current active weapon
        weapons[WeaponIndex].gameObject.SetActive(false);
        WeaponsHUD[WeaponIndex].gameObject.SetActive(false);

        //set the index to be the new weapon index
        WeaponIndex = index;

        //enable the new active weapon
        weapons[WeaponIndex].gameObject.SetActive(true);
        WeaponsHUD[WeaponIndex].gameObject.SetActive(true);

        SetCurrentWeaponAmmo();


    }

    void SetCurrentWeaponAmmo()
    {
        if (WeaponIndex == 0)
            WeaponAmmo.text = "";
        else
            WeaponAmmo.text = weaponsScriptableObjects[WeaponIndex - 1].currentAmmoInClip.ToString();
    }
    public void stabKnockedDownEnemy()
    {
        weapons[WeaponIndex].gameObject.SetActive(false);
        weapons[0].gameObject.SetActive(true);
        InputSystem.DisableDevice(Keyboard.current, false);
        PlayerAnimator.SetLayerWeight(5, 1);
        PlayerAnimator.SetBool("Stab", true);
        StartCoroutine(Stab());

    }
    IEnumerator Stab()
    {

        yield return new WaitForSeconds(1.85f);
        InputSystem.EnableDevice(Keyboard.current);
        // knifeDurabilitySetter(knifeDurability-1);
        PlayerAnimator.SetBool("Stab", false);
        PlayerAnimator.SetLayerWeight(5, 0);
        weapons[0].gameObject.SetActive(false);
        weapons[WeaponIndex].gameObject.SetActive(true);
    }
    public void breakGrapple()
    {
        weapons[WeaponIndex].gameObject.SetActive(false);
        weapons[0].gameObject.SetActive(true);
        InputSystem.DisableDevice(Keyboard.current, false);
        PlayerAnimator.SetLayerWeight(5, 1);
        PlayerAnimator.SetTrigger("BreakGrapple");
        isPlayerInGrapple = false;
        isPlayerInGrappleStabAnimation = true;
        StartCoroutine(BreakGrapple());

    }
    IEnumerator BreakGrapple()
    {
        yield return new WaitForSeconds(2.6f);
        InputSystem.EnableDevice(Keyboard.current);
        PlayerAnimator.SetLayerWeight(5, 0);
        weapons[0].gameObject.SetActive(false);
        isPlayerInGrappleStabAnimation = false;
        weapons[WeaponIndex].gameObject.SetActive(true);
    }
    // public void beingGrappled(){
    //     weapons[WeaponIndex].gameObject.SetActive(false);
    //     PlayerAnimator.SetLayerWeight(1, 0);
    //     PlayerAnimator.SetLayerWeight(2, 0);
    //     PlayerAnimator.SetLayerWeight(3, 0);
    //     PlayerAnimator.SetLayerWeight(4, 0);

    //     isPlayerInGrapple = true;
    // }
    public void knifeDurabilitySetter(int value)
    {
        if (value < 0)
            print("knife needs repair");
        else
            knifeDurability = value;
    }
    public int knifeDurabilityGetter()
    {
        return knifeDurability;
    }
    IEnumerator FireCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(weaponsScriptableObjects[WeaponIndex - 1].timeBetweenShots);
        // AK47Animator.SetBool("fire", false);
        canFire = true;
    }
    public void fire()
    {
        if (weaponsScriptableObjects[WeaponIndex - 1].currentAmmoInClip <= 0)
        {
            // reload();
            noAmmo.Play();
        }
        else
        {
            weaponsScriptableObjects[WeaponIndex - 1].currentAmmoInClip -= 1;
            // weaponsScriptableObjects[WeaponIndex - 1].totalAmmoInInventory -= 1;
            GameObject muzzleFlasheffect = Instantiate(muzzleFlash, weapons[WeaponIndex].transform.GetChild(weapons[WeaponIndex].transform.childCount - 1).position, weapons[WeaponIndex].transform.GetChild(weapons[WeaponIndex].transform.childCount - 1).rotation);
            Destroy(muzzleFlasheffect, 0.5f);
            if (WeaponIndex == 1)
            {
                pistolFire.Play();
            }
            else if (WeaponIndex == 2)
            {
                revolverFire.Play();
            }
            else if (WeaponIndex == 3)
            {
                rifleFire.Play();
            }
            else
            {
                shotgunFire.Play();
            }
            //handle for enemy range later
            //check if the ray hit within the fire range of the equiped gun
            if (BulletCollider.distance <= weaponsScriptableObjects[WeaponIndex - 1].range)
            {
                if (BulletCollider.collider != null)

                {
                    enemyScript enemy = BulletCollider.collider.gameObject.GetComponent<enemyScript>();
                    if (enemy != null)
                    {
                        float damage = weaponsScriptableObjects[WeaponIndex - 1].damageAmount;

                        // Apply damage to the enemy (you may replace this with your own damage logic)
                        enemy.TakeDamage(damage);
                    }
                    else
                    {
                        hitWallSFX.Play();
                        GameObject bulletHole = Instantiate(BulletHole
                            , BulletCollider.point + (BulletCollider.normal * 0.01f)
                            , Quaternion.FromToRotation(Vector3.up, BulletCollider.normal)
                            );
                        Destroy(bulletHole, 7f);
                    }

                }


            }
        }
        //play sound
        //play particle effect
        //play muzzle flash
        //play bullet
        //play recoil
        SetCurrentWeaponAmmo();
    }
    IEnumerator ReloadCooldown(bool isPistol)
    {
        yield return new WaitForSeconds(weaponsScriptableObjects[WeaponIndex - 1].reloadTime);
        var weapon = weaponsScriptableObjects[WeaponIndex - 1];
        if (weapon.totalAmmoInInventory < weapon.clipCapacity)
        {
            if (weapon.currentAmmoInClip + weapon.totalAmmoInInventory < weapon.clipCapacity)// clip 30, iventory 10, current 10
            {
                weapon.currentAmmoInClip += weapon.totalAmmoInInventory;
                weapon.totalAmmoInInventory = 0;
            }
            else
            {// clip 30, iventory 20, current 10    
                weapon.totalAmmoInInventory -= weapon.clipCapacity - weapon.currentAmmoInClip;
                weapon.currentAmmoInClip = weapon.clipCapacity;
            }
        }
        else
        { // clip 30, iventory 60, current 29
            weapon.totalAmmoInInventory -= weapon.clipCapacity - weapon.currentAmmoInClip;
            weapon.currentAmmoInClip = weapon.clipCapacity;

        }
        SetCurrentWeaponAmmo();
        isReloading = false;
        if (isPistol)
        {
            PlayerAnimator.SetBool("PistolReload", false);
            // PlayerAnimator.SetLayerWeight(1, 1);
            PlayerAnimator.SetLayerWeight(4, 0);
            PlayerAnimator.SetBool("ADS", false);
        }
        else
            PlayerAnimator.SetBool("isReload", false);

        PlayerAnimator.speed = 1;
        reloadCoroutine = null;
    }
    public void reload()
    {
        var weapon = weaponsScriptableObjects[WeaponIndex - 1];
        if (weapon.currentAmmoInClip == weapon.clipCapacity)
        {
            isReloading = false;
            return;
        }
        else if (weapon.totalAmmoInInventory <= 0)
        {
            noAmmo.Play();
            isReloading = false;
            return;
        }
        else
        {
            if (WeaponIndex == 1 || WeaponIndex == 2)
            {
                // PlayerAnimator.SetLayerWeight(1, 1);
                if (WeaponIndex == 1)
                {
                    pistolReload.Play();
                } else
                {
                    revolverReload.Play();
                }
                PlayerAnimator.SetBool("PistolReload", true);
                if (!PlayerAnimator.GetBool("ADS"))
                {
                    PlayerAnimator.SetLayerWeight(1, 0);
                    PlayerAnimator.SetLayerWeight(4, 1);
                }
                PlayerAnimator.speed = 1.033f / weapon.reloadTime;
                reloadCoroutine = StartCoroutine(ReloadCooldown(true));
            }
            else
            {
                if (WeaponIndex == 3)
                {
                    rifleReload.Play();
                } else
                {
                    shotgunReload.Play();
                }
                PlayerAnimator.SetBool("isReload", true);

                // AnimatorStateInfo stateInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(2);
                // float animationDuration = stateInfo.length;
                // AnimatorClipInfo[] m_CurrentClipInfo = PlayerAnimator.GetCurrentAnimatorClipInfo(2);
                // float animationDuration = m_CurrentClipInfo[0].clip.length;
                // print(animationDuration);
                PlayerAnimator.speed = 3.115f / weapon.reloadTime;

                reloadCoroutine = StartCoroutine(ReloadCooldown(false));
            }
        }
        //play sound

    }

    void ResetWeaponsInfo()
    {
        foreach (Weopen weapon in weaponsScriptableObjects)
        {
            weapon.currentAmmoInClip = weapon.clipCapacity;
            weapon.totalAmmoInInventory = weapon.clipCapacity;
        }
    }
}
