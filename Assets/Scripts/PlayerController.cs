using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BoostStates
{
    Locked,
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
    readonly float turnSpeed = 5f, thrustForce = 20f, maxVelocity = 80f, defaultGravity = 1.5f, crashSpeed = 8f;
    bool hasStarted = false;
    int dir = 0; // 1 left, -1 right

    [SerializeField] TextMeshProUGUI statesDebugText, turnStatesDebugText;
    [SerializeField] GameObject fire;

    ButtonControls buttonControls;
    Rigidbody2D rb;

    public BoostStates state;
    public TurnStates turnState;

    void Start()
    {
        buttonControls = ButtonControls.instance;

        rb = GetComponent<Rigidbody2D>();
        state = BoostStates.Locked;
    }

    void Update()
    {
        CheckForInput();

        statesDebugText.text = "State: " + state.ToString();
        turnStatesDebugText.text = "Turn State: " + turnState.ToString();

        // temp fire effect
        if (state == BoostStates.Boosting)
        {
            fire.SetActive(true);
        }

        else
        {
            fire.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // Apply turning torque when in turning state
        if (turnState == TurnStates.Turning)
        {
            rb.angularDamping = 1f;
            rb.AddTorque(dir * turnSpeed);
        }

        // Apply angular damping when not turning
        else
        {
            rb.angularDamping = 15f;
        }

        // Apply thrust force when in boosting state
        if (state == BoostStates.Boosting)
        {
            rb.gravityScale = 0.25f;

            rb.AddForce(transform.up * thrustForce);
        }

        // Reset gravity when not boosting
        else
        {
            rb.gravityScale = defaultGravity;
        }

        // Clamp the maximum velocity
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
    }

    void CheckForInput()
    {
        if (state != BoostStates.Locked)
        {
            // Manages boosting states
            if (!buttonControls.thrustDetected && state == BoostStates.Boosting)
            {
                state = BoostStates.Idle;
                Debug.Log("Switching to Idle");
            }

            else if (buttonControls.thrustDetected && state == BoostStates.Idle)
            {
                state = BoostStates.Boosting;
                rb.AddForce(transform.up * thrustForce * 0.05f, ForceMode2D.Impulse);
                Debug.Log("Switching to Boosting");
            }

            // Manages turning states
            if (buttonControls.leftDetected || buttonControls.rightDetected)
            {
                turnState = TurnStates.Turning;
                dir = buttonControls.leftDetected ? 1 : -1;
            }

            else
            {
                turnState = TurnStates.None;
                dir = 0;
            }
        }

        else
        {
            if (buttonControls.thrustDetected && !hasStarted)
            {
                StartBoost();
            }
        }
    }

    IEnumerator StateSwitch()
    {
        yield return new WaitForSeconds(0.5f);

        state = BoostStates.Idle;
        GameEvents.InvokeLevelStarted();
    }

    void StartBoost()
    {
        rb.AddForce(Vector3.up * 8f, ForceMode2D.Impulse);
        hasStarted = true;

        StartCoroutine(StateSwitch());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Only explode if hitting wall at high enough speed
        if (other.gameObject.CompareTag("Wall"))
        {
            float impactSpeed = other.relativeVelocity.magnitude;

            if (impactSpeed > crashSpeed)
            {
                GameEvents.InvokeLevelFailed();
            }
        }
    }
}

