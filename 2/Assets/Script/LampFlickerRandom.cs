using UnityEngine;

public class LampFlickerRandom : MonoBehaviour
{
    public Light[] lights;               
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time * flickerSpeed, 1f);
        float currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, t);

        foreach (Light light in lights)
        {
            if (light != null)
                light.intensity = currentIntensity;
        }
    }
}
