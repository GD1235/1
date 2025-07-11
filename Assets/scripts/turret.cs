using UnityEngine;

public class turret : MonoBehaviour
{

    public GameObject bullet;
    public float rrangetotargetplayer, timebetweenshots=0f;
    private float shotcounter,rotationspeed;
    public Transform gun, firepoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shotcounter = timebetweenshots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PLAYERCONTROLLER.instance.transform.position) < rrangetotargetplayer)
        {
            gun.LookAt(PLAYERCONTROLLER.instance.transform.position+new Vector3(0f,0.3f,0f));
            shotcounter -= Time.deltaTime;
            if (shotcounter <= 0f)
            {
                Instantiate(bullet, firepoint.position, firepoint.rotation);
                shotcounter = timebetweenshots;
            }
        }
        else
        {
            shotcounter = timebetweenshots;
            gun.rotation = Quaternion.Lerp(gun.rotation,Quaternion.Euler(0f,gun.rotation.eulerAngles.y +10f,0),rotationspeed * Time.deltaTime);
        }
    }
}
