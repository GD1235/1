using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERCONTROLLER : MonoBehaviour
{
    public static PLAYERCONTROLLER instance;
    public float MOVESPEED;
    public CharacterController CHARCON;
    private Vector3 moveinput;
    public Transform camtrans;
    public float mouseSensitivity;
    public float gravityModifier;
    public float runspeed;
    public float jumpPower;private float movespeedsaver =0;public int joumpcount;
    private Animator anim;
    //public GameObject bullet;
    public GameObject Laserimpact;
    public Transform firepoint;
    public gun activeGun;
    public List<gun> allguns = new List<gun>();
    public int currentgun;
    void Start()
    {
        activeGun = allguns[currentgun];
        activeGun.gameObject.SetActive(true);
        activeGun.maxammo = 114;
        activeGun.ammo = 114;
      anim=GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
   
        uicontroller.instance.ammotext.text = "ammo" +instance.activeGun.ammo  + "/" +instance.activeGun.maxammo ;
    }
    private void Awake()
    {
        instance = this;
    }


    void Update()
    {
        //  moveinput.x = Input.GetAxis("Horizontal")*MOVESPEED*Time.deltaTime;
        //moveinput.y = Input.GetAxis("Verital")* MOVESPEED*Time.deltaTime;
        float yStore = moveinput.y;//fix slow drop by save the initial
        
        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");
       if (Input.GetKeyDown(KeyCode.C))
       
        {
            if (movespeedsaver == 0){
                MOVESPEED = MOVESPEED * 2;
                movespeedsaver += 1;
            }
            else
            {
                MOVESPEED = MOVESPEED * 0.5f;
                movespeedsaver = 0;
            }//myrun
             
        }
        /* if (Input.GetKey(KeyCode.C))
        {//teacher'srun
            moveinput = moveinput * runspeed;
        }*/
        moveinput = vertMove + horiMove;
        moveinput.Normalize();//after the move combine, it need normalize
        moveinput = moveinput * MOVESPEED;
        moveinput.y = yStore;//cintinue the move,fix slow dropping
     
        moveinput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;//gravity
       
        
        if (CHARCON.isGrounded)
        {
            moveinput.y = 0f;
            moveinput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;//apply grav and it is just in this line
        }
        if (CHARCON.isGrounded) {
            joumpcount= 2; //get double jump
        };
        if ( Input.GetKeyDown(KeyCode.Space)&&joumpcount>0) {
            moveinput.y = jumpPower;
            joumpcount -=1; //jump and comsume
        }
        if (Input.GetKey(KeyCode.Tab)) {
            //switch gun
            switchGun();
        }
        CHARCON.Move(moveinput*Time.deltaTime);
        //rotate right and left
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"))*mouseSensitivity;
        //the upper line is mouse to look up and right
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        
        camtrans.rotation = Quaternion.Euler(camtrans.rotation.eulerAngles.x-mouseInput.y, camtrans.rotation.eulerAngles.y, camtrans.rotation.eulerAngles.z);
        //the upper line is camera fallow the character
        //Debug.Log("speed = " + moveinput.magnitude);//show the speed



        //handle shooting-singleshot
        if (Input.GetMouseButtonDown(0)&&activeGun.fireCounter<=0) {
            RaycastHit hit;
            if(Physics.Raycast(camtrans.position,camtrans.forward,out hit, 500f))
            {
                firepoint.LookAt(hit.point);//make firepoint lookat itto synconize the hitpoint and the crosshair
            }
            else
            {
                firepoint.LookAt(camtrans.position + camtrans.forward*300f);//if face nothing, look at the center of the camera.
            }
            //Instantiate(bullet, firepoint.position, firepoint.rotation);
            fireshot();
        }
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0)
            {
                fireshot();
                
               

            }
        }


        anim.SetFloat("runspeed", moveinput.magnitude);//control the ani transition




        
    }
public void fireshot()//autoshot
    {
        if (activeGun.ammo > 0)
        {
            activeGun.ammo -= 1;
            Instantiate(activeGun.bullet, firepoint.position, firepoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;//reset the value.
        
                uicontroller.instance.ammotext.text = "ammo" + instance.activeGun.ammo + "/" + instance.activeGun.maxammo;}
    }

    public void switchGun()
    {
        allguns[currentgun].gameObject.SetActive(false);
        currentgun = (currentgun + 1) % allguns.Count;
        activeGun = allguns[currentgun];
        activeGun.gameObject.SetActive(true);
    }
}
