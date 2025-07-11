using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    private Rigidbody rb;

    public GameObject laserImpact;
    public int damage = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.forward * moveSpeed;//linear velocity means constant speed

        lifeTime -= Time.deltaTime;//counting down

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
            //Destroy(other.transform.parent.gameObject);
        }

        float offset = 0.7f;
        Vector3 newPosition = transform.position - transform.forward * offset;

        Instantiate(laserImpact, newPosition, transform.rotation);
        Destroy(gameObject);
    }
}
