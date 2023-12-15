using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu{
	public class UIMenuManager : MonoBehaviour {
		private Animator CameraObject;

		// campaign button sub menu
        [Header("MENUS")]
        public GameObject mainMenu;
        public GameObject firstMenu;
        public GameObject exitMenu;

        [Header("PANELS")]
        public GameObject mainCanvas;
        public GameObject PanelTeam;
        public GameObject PanelAsset;
        public GameObject PanelGame;

        // highlights in settings screen
        [Header("SETTINGS SCREEN")]
        public GameObject lineAudio;
        public GameObject lineTeam;
        public GameObject lineAsset;

        [Header("LOADING SCREEN")]
		public bool waitForInput = true;
        public GameObject loadingMenu;
        public Slider loadingBar;
        public TMP_Text loadPromptText;
		public KeyCode userPromptKey;

		[Header("SFX")]
        public AudioSource hoverSound;
        public AudioSource sliderSound;
        public AudioSource swooshSound;

		void Start(){
			CameraObject = transform.GetComponent<Animator>();
			exitMenu.SetActive(false);
			firstMenu.SetActive(true);
			mainMenu.SetActive(true);
		}


		public void PlayCampaign(){
            exitMenu.SetActive(false);
            StartCoroutine(LoadAsynchronously("Mina"));
        }

		public void ReturnMenu(){
			exitMenu.SetActive(false);
			mainMenu.SetActive(true);
		}

		public void LoadScene(string scene){
			if(scene != ""){
				StartCoroutine(LoadAsynchronously(scene));
			}
		}

		public void Position2(){
			CameraObject.SetFloat("Animate",1);
		}

		public void Position1(){
			CameraObject.SetFloat("Animate",0);
		}

		void DisablePanels(){
            PanelTeam.SetActive(false);
            PanelAsset.SetActive(false);
			PanelGame.SetActive(false);
            lineAudio.SetActive(false);
            lineAsset.SetActive(false);
            lineTeam.SetActive(false);

		}

		public void AudioPanel(){
			DisablePanels();
			PanelGame.SetActive(true);
            lineAudio.SetActive(true);
		}

		public void AssetPanel(){
			DisablePanels();
            PanelAsset.SetActive(true);
            lineTeam.SetActive(true);
		}

		public void TeamPanel(){
			DisablePanels();
            PanelTeam.SetActive(true);
            lineAsset.SetActive(true);
		}

		public void PlayHover(){
			hoverSound.Play();
		}

		public void PlaySFXHover(){
			sliderSound.Play();
		}

		public void PlaySwoosh(){
			swooshSound.Play();
		}

		// Are You Sure - Quit Panel Pop Up
		public void AreYouSure(){
			exitMenu.SetActive(true);
		}

		public void QuitGame(){
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}

        // Load Bar synching animation
        IEnumerator LoadAsynchronously(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            mainCanvas.SetActive(false);
            loadingMenu.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                loadingBar.value = progress;

                if (operation.progress >= 0.9f && waitForInput)
                {
                    loadPromptText.text = "Press " + userPromptKey.ToString().ToUpper() + " to continue";
                    loadingBar.value = 1;

                    if (Input.GetKeyDown(userPromptKey))
                    {
                        operation.allowSceneActivation = true;
                    }
                }
                else if (operation.progress >= 0.9f && !waitForInput)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}