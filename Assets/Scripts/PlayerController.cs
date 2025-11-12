using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool leftActive, rightActive, dualActive;
    [SerializeField] float turnSpeed = 5f, maxTurnSpeed = 250f;
    int dir = 0; // -1 left, 1 right

    Rigidbody2D rb;

    private void Awake()
    {
        Input.multiTouchEnabled = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
    }

    private void FixedUpdate()
    {
        if (dualActive)
        {
            // Do super pogo
        }

        else
        {
            if (rightActive || leftActive)
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
    }

    void CheckForInput()
    {
        leftActive = false;
        rightActive = false;


        //Checks if there are any touch inputs
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.touches[i];

                if (touch.position.x > (Screen.width / 2))
                {
                    rightActive = true;
                    dir = -1;
                }

                else
                {
                    leftActive = true;
                    dir = 1;
                }
            }
        }

        if (rightActive && leftActive)
        {
            dualActive = true;
            Debug.Log("Both active");
        }

        else
        {
            dualActive = false;
        }
    }
}

