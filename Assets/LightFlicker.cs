using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
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
