using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class playerhealthcontroller : MonoBehaviour, Iisdamageable
{
    public static playerhealthcontroller instance;
    public int maxhealth, currenthealth;
    public float invlength = 1f;
    private float invcounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void healplayer(int healamount)
    {
        currenthealth = healamount;
        if (currenthealth > maxhealth)
        {
            currenthealth = maxhealth;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currenthealth = maxhealth;

        uicontroller.instance.healthslider.maxValue = maxhealth;
        uicontroller.instance.healthslider.value = currenthealth;
        uicontroller.instance.healthtext.text = "health:" + currenthealth + "/" + maxhealth;
    }
  
    public void damageplayer (int damage)
        
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (invcounter < 0)
        {
            invcounter -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage, bool attackPlayer)
    {
        if(attackPlayer)
        {
            if (invcounter <= 0)
            {
                currenthealth -= damage;
                uicontroller.instance.healthslider.value = currenthealth;
                uicontroller.instance.healthtext.text = "health:" + currenthealth + "/" + maxhealth;

                if (currenthealth <= 0)
                {
                   // transform.parent.gameObject.SetActive(false);//hide the player
                    currenthealth = 0;
                    GameManager.instance.PlayerDied();
                }
                invcounter = 0;
            }
        }
    }

}
