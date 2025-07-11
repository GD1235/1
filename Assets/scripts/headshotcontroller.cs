using UnityEngine;

public class headshotcontroller : MonoBehaviour, Iisdamageable

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TakeDamage(int damage, bool attackPlayer)
    {
        
        transform.parent.gameObject.GetComponent<enemyHealthController>().TakeDamage(damage * 3, attackPlayer);
    }


}
