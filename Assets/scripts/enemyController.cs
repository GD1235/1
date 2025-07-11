using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Vector3 targetPoint, startPoint;
    // public float moveSpeed;
    //public Rigidbody rb;
    private bool chasing;
    public float distanceToCHase = 10f, distanceToLose = 15f, distanceToStop = 2f;

    private NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    [Header("Bullet Section")]

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate, waitBetweenShots = 1f, timeToShoot = 2f;
    private float fireCount, shootWaitCounter, shootTimeCounter;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;//store his first starting position

        shootTimeCounter = timeToShoot;
        shootWaitCounter = waitBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PLAYERCONTROLLER.instance.transform.position;//store Vector3: x,y,z
        targetPoint.y = transform.position.y;//this will no longer depend on player y axis, instead it is now depending on enemy y axis itself

        if (!chasing)//chasing is false
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToCHase)
            {
                chasing = true;//we are within the range
            }
            //make Enemy chase us for certain time before deciding to go back to the startPoint
            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }
        }
        else//chasing is true
        {
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint;
               anim.SetBool("ismoving", false);
            }
            else
            {
                //stop him here
                agent.destination = transform.position;
                 anim.SetBool("ismoving", true);
            }

            // transform.LookAt(targetPoint);
            // rb.linearVelocity = transform.forward * moveSpeed;


            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;//we are out of the range
                //agent.destination = startPoint;
                chaseCounter = keepChasingTime;//to reset the chaseCounter
            }

            if (shootWaitCounter > 0)
            {
                shootWaitCounter -= Time.deltaTime;


                if (shootWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
                anim.SetBool("ismoving", true);
            }
            else if(PLAYERCONTROLLER.instance.gameObject.activeInHierarchy) 
            {
                shootTimeCounter -= Time.deltaTime;

                 if (shootTimeCounter > 0)
                    {
                    fireCount -= Time.deltaTime;
                    if (fireCount <= 0)
                    {
                    fireCount = fireRate;
                       
                    firePoint.LookAt(targetPoint + new Vector3(0f,0.70f,0f));
                    Vector3 targetDir = PLAYERCONTROLLER.instance.transform.position - transform.position;//GETDIRECTION
                    float angle = Vector3.SignedAngle(targetDir,transform.forward,Vector3.up);//LOOK UP TO CALCULATE DEGREE
                    if (Mathf.Abs(angle)<30f)
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);

                            anim.SetTrigger("fireshot");
                    }
                        else
                        {
                            shootWaitCounter = waitBetweenShots;
                        }
                        
                    }
                    agent.destination = transform.position;//stopwhile shooting
                }
                else
                {
                    shootWaitCounter = waitBetweenShots;

                }

                anim.SetBool("ismoving", false);
            }



        }
    }
}
