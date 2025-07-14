using UnityEngine;

public class TrafficLightSwitcher : MonoBehaviour
{
    public Light flickerLight;
    public float interval = 0.5f;

    private bool isOn = true;
    private bool isFlashing = false;
    private float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                ToggleLight();
                timer = interval; 
            }
        }
    }
    public void StartFlashing()
    {
        isFlashing = true;
        timer = interval; 
        Debug.Log("ºìµÆ¿ªÊ¼ÉÁË¸£¡");
    }

    public void StopFlashing()
    {
        isFlashing = false;
        flickerLight.enabled = false;
        Debug.Log("ºìµÆÍ£Ö¹ÉÁË¸£¡");
    }

    void ToggleLight()
    {
        isOn = !isOn;
        flickerLight.enabled = isOn;
    }
}
