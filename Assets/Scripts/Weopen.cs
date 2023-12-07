using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weopen", menuName = "Weopen")]
public class Weopen : ScriptableObject
{
    public enum FiringMode
    {
        Automatic,
        SingleShot
    }
    public FiringMode firingMode;
    public float damageAmount;
    public float timeBetweenShots;
    public float range;
    public int clipCapacity;
    public int currentAmmoInClip;
    public int totalAmmoInInventory;
    public float reloadTime;
}
