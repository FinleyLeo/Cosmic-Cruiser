using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BaseStates
{
    Grounded,
    Idle,
    Boosting
}

public enum TurnStates
{
    Turning,
    None
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool leftDetected, rightDetected, thrustDetected, levelStarted;
    [SerializeField] float turnSpeed = 8f, thrustForce = 40f, maxForce = 80f;
    int dir = 0; // 1 left, -1 right

    [SerializeField] TextMeshProUGUI statesDebugText, turnStatesDebugText;
    [SerializeField] GameObject fire;

    Rigidbody2D rb;

    public BaseStates state;
    public TurnStates turnState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = BaseStates.Grounded;
    }

    void Update()
    {
        CheckForInput();
        statesDebugText.text = "State: " + state.ToString();
        turnStatesDebugText.text = "Turn State: " + turnState.ToString();

        if (state == BaseStates.Grounded)
        {
            if (thrustDetected && !levelStarted)
            {
                rb.AddForce(Vector3.up * 12.5f, ForceMode2D.Impulse);
                levelStarted = true;

                StartCoroutine(LaunchDelay());
            }
        }

        if (state == BaseStates.Boosting)
        {
            fire.SetActive(true);
        }

        else
        {
            fire.SetActive(false);
        }

        // Debug reset
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {
        if (turnState == TurnStates.Turning)
        {
            rb.angularDamping = 1f;
            rb.AddTorque(dir * turnSpeed);
        }

        else
        {
            rb.angularDamping = 15f;
        }

        if (state == BaseStates.Boosting)
        {
            rb.linearDamping = 0f;
            rb.AddForce(transform.up * thrustForce);
        }

        else
        {
            rb.linearDamping = 0.5f;
        }

        // Apply slight drag to slow down over time
        rb.AddForce(-rb.linearVelocity * 0.1f);

        // Clamp the maximum velocity
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxForce);
    }

    void CheckForInput()
    {
        if (state != BaseStates.Grounded)
        {
            if (!thrustDetected)
            {
                state = BaseStates.Idle;
            }

            else
            {
                state = BaseStates.Boosting;
            }

            if (leftDetected || rightDetected)
            {
                turnState = TurnStates.Turning;
                dir = leftDetected ? 1 : -1;
            }

            else
            {
                turnState = TurnStates.None;
                dir = 0;
            }
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

    IEnumerator LaunchDelay()
    {
        yield return new WaitForSeconds(0.5f);
        state = BaseStates.Idle;
    }
}

