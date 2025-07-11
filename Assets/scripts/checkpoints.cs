using UnityEngine;
using UnityEngine.SceneManagement;

public class checkpoints : MonoBehaviour
{
    public string cpName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name+"_cp"))
            {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp")==cpName) {
            PLAYERCONTROLLER.instance.transform.position = transform.position;

            }
        }
    }

    // Update is called once per frame
  
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name +"cp",cpName);
        Debug.Log ("touching "+cpName);
    }
}
