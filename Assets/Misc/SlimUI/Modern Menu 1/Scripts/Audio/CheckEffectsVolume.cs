using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CheckEffectsVolume : MonoBehaviour
{

    public AudioMixer audioMixer;
    string exposedParameter = "SFXVolume";

    void Start()
    {
        UpdateEffects();
    }

    float MapToRange(float value, float originalMin, float originalMax, float newMin, float newMax)
    {
        return (value - originalMin) / (originalMax - originalMin) * (newMax - newMin) + newMin;
    }

public void UpdateEffects()
    {
        float musicVolume = PlayerPrefs.GetFloat("EffectsVolume");
        float mappedVolume = -80f;
        if (musicVolume > 0.05)
        {
            mappedVolume = MapToRange(musicVolume, 0.05f, 1f, -45f, -8f);
        }
        audioMixer.SetFloat(exposedParameter, mappedVolume);
    }
}
