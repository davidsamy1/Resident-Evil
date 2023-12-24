using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

namespace SlimUI.ModernMenu{
	public class CheckMusicVolume : MonoBehaviour {

        public AudioMixer audioMixer;
        string exposedParameter = "MusicVolume";

        void Start()
        {
            UpdateMusic();
        }

        float MapToRange(float value, float originalMin, float originalMax, float newMin, float newMax)
        {
            return (value - originalMin) / (originalMax - originalMin) * (newMax - newMin) + newMin;
        }

public void UpdateMusic()
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            float mappedVolume = -80f;
            if (musicVolume > 0.05)
            {
                mappedVolume = MapToRange(musicVolume, 0.05f, 1f, -50f, -8f);
            }
            audioMixer.SetFloat(exposedParameter, mappedVolume);
        }
    }
}