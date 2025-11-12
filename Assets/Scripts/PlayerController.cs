using TMPro;
using UnityEngine;

public enum States
{
    Idle,
    Turning,
    Boosting,
    TurnBoosting
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool leftDetected, rightDetected, thrustDetected;
    [SerializeField] float turnSpeed = 5f, maxTurnSpeed = 250f;
    [SerializeField] TextMeshProUGUI statesDebugMenu;
    int dir = 0; // 1 left, -1 right

    Rigidbody2D rb;

    public States state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = States.Idle;
    }

    void Update()
    {
        statesDebugMenu.text = "State: " + state.ToString();
        CheckForInput();
    }

    private void FixedUpdate()
    {
        if (state == States.Turning || state == States.TurnBoosting)
        {
            // Only add torque if the angular velocity is below the max turn speed
            if (rb.angularVelocity < maxTurnSpeed && rb.angularVelocity > -maxTurnSpeed)
            {
                rb.AddTorque(dir * turnSpeed);
            }

            else
            {
                // Clamp the angular velocity to the max turn speed
                rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxTurnSpeed, maxTurnSpeed);
            }
        }
    }

    void CheckForInput()
    {
        //Checks if pressing any button, and sets sate accordingly
        if (Input.touchCount > 0)
        {
            if (leftDetected)
            {
                dir = 1;
                state = States.Turning;
            }

            if (rightDetected)
            {
                dir = -1;
                state = States.Turning;
            }

            if (thrustDetected)
            {
                if (state == States.Turning)
                {
                    state = States.TurnBoosting;
                }

                else
                {
                    state = States.Boosting;
                }
            }
        }

        else
        {
            state = States.Idle;

            leftDetected = false;
            rightDetected = false;
            thrustDetected = false;
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

