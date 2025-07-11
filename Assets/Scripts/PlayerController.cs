using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, gravityModifier, jumpPower, runSpeed = 12f;
    public CharacterController charCon;

    private Vector3 moveInput;

    public Transform camTrans;

    public float mouseSensitivity;

    public int maxJumpCount = 2;
    public int jumpCount = 0;
    private Animator anim;

    public GameObject bullet;
    public Transform firePoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();//get access
    }

    // Update is called once per frame
    void Update()
    {
        // moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        // moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        float yStore = moveInput.y;//save the initial moveinput y

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");//involve transform forward direction or z axis
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");//involve transform right direction or x axis

        moveInput = vertMove + horiMove;
        moveInput.Normalize();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * moveSpeed;//nomal speed
        }


        moveInput.y = yStore;//conntinue the moveinput y right away

        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charCon.isGrounded)//detect the ground, true when touch the ground
        {
            jumpCount = 0;
            moveInput.y = 0f;//normalize the ground y axis input
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;//apply gravity

        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            moveInput.y = jumpPower;//make jump effect
            jumpCount++;
        }

        charCon.Move(moveInput * Time.deltaTime);

        //Control Rotation

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        //Rotate the player body right and left
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles.x - mouseInput.y, camTrans.rotation.eulerAngles.y + mouseInput.x, camTrans.rotation.eulerAngles.z);

        //Handle shooting
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(camTrans.position, camTrans.forward, out hit, 500f))
            {
                firePoint.LookAt(hit.point);//will make fire point looking at the hitting point of RayCast

            }
            else
            {
                firePoint.LookAt(camTrans.position + (transform.forward));
                //if not looking an object , just look at the cerntre of the camera
            }

                Instantiate(bullet, firePoint.position, firePoint.rotation);
        }

        anim.SetFloat("moveSpeed", moveInput.magnitude);//control the animation transition

    }
}
