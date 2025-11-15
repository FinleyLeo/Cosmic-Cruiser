using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControls : MonoBehaviour
{
    public static ButtonControls instance;

    public bool leftDetected, rightDetected, thrustDetected;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameEvents.OnGameOver += ResetLevel;
    }
    public void CheckForLeft(bool toggle)
    {
        leftDetected = toggle;
    }

    public void CheckForRight(bool toggle)
    {
        rightDetected = toggle;
    }

    public void CheckForThrust(bool toggle)
    {
        thrustDetected = toggle;
    }

    // temp
    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
