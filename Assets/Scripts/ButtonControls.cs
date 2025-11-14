using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    public static ButtonControls instance;

    public bool leftDetected, rightDetected, thrustDetected;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
}
