using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // temp
        SceneManager.LoadScene("Prototyping");
    }
}
