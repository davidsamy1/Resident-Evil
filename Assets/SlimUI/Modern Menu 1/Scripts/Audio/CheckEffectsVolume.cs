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
        float mappedVolume = MapToRange(musicVolume, 0f, 1f, -80f, 0f);
        audioMixer.SetFloat(exposedParameter, mappedVolume);
    }
}
