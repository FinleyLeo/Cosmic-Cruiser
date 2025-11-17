using UnityEngine;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour
{
    public static ButtonControls instance;

    public bool leftDetected, rightDetected, thrustDetected;
    [SerializeField] Sprite leftOn, leftOff, rightOn, rightOff, thrustOn, thrustOff;
    Image left, right, thrust;

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
    }

    private void Start()
    {
        left = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        right = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        thrust = transform.GetChild(1).GetComponent<Image>();
    }

    public void CheckForLeft(bool toggle)
    {
        leftDetected = toggle;

        left.sprite = toggle ? leftOn : leftOff;
    }

    public void CheckForRight(bool toggle)
    {
        rightDetected = toggle;

        right.sprite = toggle ? rightOn : rightOff;
    }

    public void CheckForThrust(bool toggle)
    {
        thrustDetected = toggle;

        thrust.sprite = toggle ? thrustOn : thrustOff;
    }
}
