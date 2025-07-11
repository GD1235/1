using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    public bool shouldmove, shouldrotate;
    public float movespeed, rotatespeed;
    public float rotateoffset;
    void Start()
    {

    }
    void Update()
    {
        if (shouldmove)
        {

            transform.position += new Vector3(movespeed, 0f, 0f) * Time.deltaTime;

        }
        if (shouldrotate)
        {
             transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, rotatespeed * Time.deltaTime, 0f));
        }

    }
}
