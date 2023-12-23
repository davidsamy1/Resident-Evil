using UnityEngine;

public class FlickeringLightWithSound : MonoBehaviour
{
    public AudioSource flickerSound;
    
    private Light pointLight;
    private bool isLightOn = true;
    private float nextFlickerTime;

    void Start()
    {
        pointLight = GetComponent<Light>();
        RandomizeNextFlickerTime();
    }

    void Update()
    {
        if (Time.time > nextFlickerTime)
        {
            if (isLightOn)
            {
                pointLight.enabled = false;
                if (flickerSound != null && !flickerSound.isPlaying)
                {
                    flickerSound.Play();
                }
            }
            else
            {
                pointLight.enabled = true;
            }

            isLightOn = !isLightOn;
            RandomizeNextFlickerTime();
        }
    }

    void RandomizeNextFlickerTime()
    {
        nextFlickerTime = Time.time + Random.Range(0.5f, 3f);
    }
}
