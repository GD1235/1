
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    float waitafterdying = 2f;
    void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void PlayerDied()
    {
        StartCoroutine(playerdiedco());
    }

    public IEnumerator playerdiedco()
    {
        yield return new WaitForSeconds(waitafterdying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

