using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Boolean isOneSided = true;
    public Boolean toBeOpened = true;

    private Animator doorAnimator;
    private Collider doorCollider;

    private void Start()
    {
        doorCollider = GetComponent<Collider>();
        doorAnimator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        if (isOneSided)
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("Unlock");
            }

            if (doorCollider != null)
            {
                doorCollider.enabled = false;
            }
        }
    }
}
