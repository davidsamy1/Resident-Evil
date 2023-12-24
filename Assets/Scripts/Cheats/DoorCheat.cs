using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheat : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenAllDoors();
        }
    }

    void OpenAllDoors()
    {
        // Debug.Log("Opening All doors");
        DoorController[] doorControllers = FindObjectsOfType<DoorController>();
        foreach (DoorController doorController in doorControllers)
        {
           if (doorController.toBeOpened)
            {
                doorController.OpenDoor();
            }
            
        }
    }
}
