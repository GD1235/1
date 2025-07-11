using UnityEngine;

public class healthpickup : MonoBehaviour
{
    public int healamount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            {
            playerhealthcontroller.instance .healplayer(healamount);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
