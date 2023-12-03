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
    public int WeaponIndex=0;
    public GameObject leon;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    // Start is called before the first frame update
    private void Awake()
    {
        starterAssetsInputs=GetComponent<StarterAssetsInputs>();
        thirdPersonController=GetComponent<ThirdPersonController>();
        
        
    }

    // Update is called once per frame
    private void Update()
    {

        //handle crosshair position (later will detect the enemies)

        
        Vector3 mouseWorldPosition1 = Vector3.zero;
        Vector3 mouseWorldPosition2 = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2((Screen.width / 2f), Screen.height / 2f);
        Vector2 screenCenterPointShifted = new Vector2((Screen.width / 2f) + 180, (Screen.height / 2f)-20);
        Ray ray1 = Camera.main.ScreenPointToRay(screenCenterPoint);
        Ray ray2 = Camera.main.ScreenPointToRay(screenCenterPointShifted);


        if (Physics.Raycast(ray1, out RaycastHit raycastHitBullet, 999f, aimColliderLayerMask))
        {
            aimdebug1.position = raycastHitBullet.point;
            mouseWorldPosition1 = raycastHitBullet.point;
        }

        if (Physics.Raycast(ray2, out RaycastHit raycastHit2, 999f, aimColliderLayerMask))
        {
            aimdebug2.position = raycastHit2.point;
            mouseWorldPosition2 = raycastHit2.point;
        }

        //if right click (ADS)
        if (starterAssetsInputs.aim)
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
        if(WeaponIndex ==2 ||WeaponIndex ==3)
        {
            PlayerAnimator.SetLayerWeight(2, 1);
        }
    }

    private void StartADS(Vector3 mouseWorldPosition)
    {
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
        if(WeaponIndex == 2 || WeaponIndex == 3) {
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

        //set the index to be the new weapon index
        WeaponIndex = index;

        //enable the new active weapon
        weapons[WeaponIndex].gameObject.SetActive(true);

    }
}
