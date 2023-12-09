using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEffectsVolume : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EffectsVolume");
    }

    public void updateEffects()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EffectsVolume");
    }
}
