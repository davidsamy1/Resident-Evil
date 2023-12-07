using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using UnityEditor.ShaderGraph;
using UnityEngine.UI;
using System.Drawing;
using UnityEngine.UIElements;
using UnityEngine.Animations.Rigging;
using System;
using TMPro;

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
    public List<GameObject> weapons;
    public int WeaponIndex = 1;
    public GameObject leon;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    public List<Weopen> weaponsScriptableObjects;
    public GameObject muzzleFlash;
    private bool canFire = true;
    private bool isReloading = false;
    public GameObject BulletHole;
    private RaycastHit BulletCollider;
    public TMP_Text CurrentAmmo;
    public TMP_Text InventoryAmmo;
    public List<GameObject> WeaponsHUD;
    // Start is called before the first frame update
    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        ResetWeaponsInfo();

    }

    // Update is called once per frame
    private void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetWeaponIndex(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeaponIndex(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeaponIndex(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeaponIndex(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetWeaponIndex(4);
        }

        if (WeaponIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {   
                isReloading = true; 
                StartCoroutine(ReloadCooldown());
                reload();


            }
            if (Input.GetKey(KeyCode.Mouse0) && weaponsScriptableObjects[WeaponIndex-1].firingMode==Weopen.FiringMode.Automatic && starterAssetsInputs.aim)
            {
                if (canFire & !isReloading)
                {
                    fire();
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
        print(BulletCollider.distance);
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


    }
    IEnumerator FireCooldown()
{
    canFire = false;
    yield return new WaitForSeconds(weaponsScriptableObjects[WeaponIndex - 1].timeBetweenShots);
    canFire = true;
}
    public void fire()
    {
        if (weaponsScriptableObjects[WeaponIndex - 1].currentAmmoInClip <= 0)
        {
            // reload();
            // play sound no ammo

        }
        else
        {
            weaponsScriptableObjects[WeaponIndex - 1].currentAmmoInClip -= 1;
            // weaponsScriptableObjects[WeaponIndex - 1].totalAmmoInInventory -= 1;
            GameObject muzzleFlasheffect = Instantiate(muzzleFlash, weapons[WeaponIndex].transform.GetChild(weapons[WeaponIndex].transform.childCount - 1).position, weapons[WeaponIndex].transform.GetChild(weapons[WeaponIndex].transform.childCount - 1).rotation);
            Destroy(muzzleFlasheffect, 1f);

            //handle for enemy range later
            //check if the ray hit within the fire range of the equiped gun
            if (BulletCollider.distance <= weaponsScriptableObjects[WeaponIndex - 1].range)
            {
                Instantiate(BulletHole
                    , BulletCollider.point + (BulletCollider.normal * 0.1f)
                    , Quaternion.FromToRotation(Vector3.up, BulletCollider.normal)
                    );
            }
        }

        //play sound
        //play particle effect
        //play muzzle flash
        //play bullet
        //play recoil

    }
    IEnumerator ReloadCooldown()
{
    yield return new WaitForSeconds(weaponsScriptableObjects[WeaponIndex - 1].reloadTime);
    isReloading = false;
}
    public void reload()
    {
        var weapon = weaponsScriptableObjects[WeaponIndex - 1];
        if (weapon.currentAmmoInClip == weapon.clipCapacity)
        {
            return;
        }
        if (weapon.totalAmmoInInventory <= 0)
        {
            //play sound no ammo
            return;
        }
        else
        {
            if (weapon.totalAmmoInInventory < weapon.clipCapacity)
            {
                if (weapon.currentAmmoInClip + weapon.totalAmmoInInventory < weapon.clipCapacity)// clip 30, iventory 10, current 10
                {
                    weapon.currentAmmoInClip += weapon.totalAmmoInInventory;
                    weapon.totalAmmoInInventory = 0;
                }
                else
                {// clip 30, iventory 20, current 10
                    weapon.currentAmmoInClip = weapon.clipCapacity;
                    weapon.totalAmmoInInventory -= weapon.clipCapacity - weapon.currentAmmoInClip;
                }
            }
            else
            { // clip 30, iventory 60, current 29
                weapon.totalAmmoInInventory -= weapon.clipCapacity - weapon.currentAmmoInClip;
                weapon.currentAmmoInClip = weapon.clipCapacity;

            }
        }
        //play sound
        //play animation
        //play reload

    }

    void ResetWeaponsInfo()
    {
        foreach (Weopen weapon in weaponsScriptableObjects)
        {
            weapon.currentAmmoInClip = weapon.clipCapacity;
            weapon.totalAmmoInInventory=weapon.clipCapacity;
        }
    }
}
