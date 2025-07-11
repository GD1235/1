using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()//LateUpdate means the last update tobe called afer the normal update in PlayerController class
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
