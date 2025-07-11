using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;


public class bulletcontroller : MonoBehaviour
{
    // Start is called once before the f
    // irst execution of Update after the MonoBehaviour is created

    public float bulletmoveSpeed,livetime;
    private Rigidbody rb;
    private TrailRenderer trailRenderer;
    public GameObject Laserimpact;
    public GameObject target;
    public int damage = 2;
    public bool attackplayer;
    
    //public bool damageEnemy, damagePlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

        rb.linearVelocity = transform.forward * bulletmoveSpeed;
        livetime -=Time.deltaTime;
        if(livetime < 0)
        {
            Destroy(gameObject);    
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        /*
        if (other.CompareTag( "destoryable"))
        {
             Destroy(other.transform.parent.gameObject);
        }
        if(other.CompareTag("enemy") && damageEnemy)
        {
            other.gameObject.GetComponent< enemyHealthController > ().damageEnemy(damage);
        }
        if (other.CompareTag("headshot") &&damageEnemy)
        {
            other.gameObject.GetComponentInParent<enemyHealthController > ().damageEnemy(damage*2);
        }
           
        if(other.CompareTag("Player" )&& damagePlayer)
        {
            Debug.Log("hit player");
            playerhealthcontroller.instance.damageplayer(damage);
        }*/
        Iisdamageable iiddamageable = other.gameObject.GetComponent<Iisdamageable>();
        if (iiddamageable != null)
        {
            iiddamageable.TakeDamage(damage, attackplayer);
        }
        float offset = 0.7f;
        Vector3 newPosition = transform.position - transform.forward * offset;
        
        Instantiate(Laserimpact,newPosition,transform.rotation);
        
        Destroy(gameObject);
       
    }
    


}



