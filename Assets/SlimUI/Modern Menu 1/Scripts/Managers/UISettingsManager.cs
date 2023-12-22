using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace SlimUI.ModernMenu{
	public class UISettingsManager : MonoBehaviour {

		[Header("CONTROLS SETTINGS")]
		// sliders
		public GameObject musicSlider;
		public GameObject effectsSlider;

		private float musicSliderValue = 0.0f;
		private float effectsSliderValue = 0.0f;
		

		public void  Start (){
			// check slider values
			musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
            effectsSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("EffectsVolume");
		}

		public void Update (){
			musicSliderValue = musicSlider.GetComponent<Slider>().value;
			effectsSliderValue = effectsSlider.GetComponent<Slider>().value;
		}

		public void MusicSlider (){
			PlayerPrefs.SetFloat("MusicVolume", musicSliderValue);
			PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
		}

        public void EffectsSlider()
        {
            PlayerPrefs.SetFloat("EffectsVolume", effectsSliderValue);
            PlayerPrefs.SetFloat("EffectsVolume", effectsSlider.GetComponent<Slider>().value);
        }
	}
}