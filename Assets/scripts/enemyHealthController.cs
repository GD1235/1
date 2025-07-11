using UnityEngine;

public class enemyHealthController : MonoBehaviour,Iisdamageable

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int currentHealth = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void damageEnemy(int damage) {
       
    }

    public void TakeDamage(int damage, bool attackplayer)
    {
        if (!attackplayer)
        {
            currentHealth = currentHealth - damage;
            if (currentHealth <= 0)
            {
                Destroy(transform.parent.gameObject);
            }

        }
    }
}
