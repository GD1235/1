using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uicontroller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static uicontroller instance;
    public Slider healthslider;
    public Text healthtext;
    public Text ammotext;
  
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
