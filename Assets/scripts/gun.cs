using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject bullet;
    public bool canAutoFire;
    public float fireRate;
    public float fireCounter;
    public float maxammo;
    public float ammo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCounter > 0) 
        { 
        fireCounter -= Time.deltaTime;
        }
    }
}
