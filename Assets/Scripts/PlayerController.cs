using System.Collections;
using UnityEngine;

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
    readonly float turnSpeed = 12f, thrustForce = 12.5f, maxVelocity = 80f, defaultGravity = 1.5f, crashSpeed = 9f;
    float angularVelZ = 0;
    bool hasStarted = false;
    int dir = 0; // 1 left, -1 right

    ButtonControls buttonControls;
    Rigidbody2D rb;
    Animator anim;

    public BoostStates state;
    public TurnStates turnState;

    private void OnEnable()
    {
        GameEvents.OnGameOver += Explode;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= Explode;
    }

    void Start()
    {
        buttonControls = ButtonControls.instance;

        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        state = BoostStates.Locked;
    }

    void Update()
    {
        CheckForInput(); 

        if (turnState == TurnStates.Turning)
        {
            angularVelZ += Time.deltaTime * dir * turnSpeed * 100f;
        }

        else
        {
            if (Mathf.Abs(angularVelZ) > 0.1f)
            {
                angularVelZ = Mathf.Lerp(rb.angularVelocity, 0f, Time.deltaTime * 5f);
            }

            else
            {
                angularVelZ = 0f;
            }
        }

        rb.angularVelocity = angularVelZ;
    }

    private void FixedUpdate()
    {
        // Apply thrust force when in boosting state
        if (state == BoostStates.Boosting)
        {
            rb.gravityScale = 0.5f;

            rb.AddForce(transform.up * thrustForce);
        }

        // Reset gravity when not boosting
        else
        {
            rb.gravityScale = defaultGravity;
        }

        // Clamp the maximum velocity
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -600f, 600f);
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
                anim.Play("Idle", 0);
            }

            else if (buttonControls.thrustDetected && state == BoostStates.Idle)
            {
                state = BoostStates.Boosting;
                rb.AddForce(transform.up * thrustForce * 0.1f, ForceMode2D.Impulse);
                anim.Play("Thrust_Start", 0);
            }

            // Manages turning states
            if (buttonControls.leftDetected || buttonControls.rightDetected)
            {
                if (turnState == TurnStates.None)
                {
                    turnState = TurnStates.Turning;
                    dir = buttonControls.leftDetected ? 1 : -1;
                }
            }

            else
            {
                if (turnState == TurnStates.Turning)
                {
                    turnState = TurnStates.None;
                    dir = 0;
                }
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

    IEnumerator StartStateSwitch()
    {
        yield return new WaitForSeconds(0.5f);

        state = BoostStates.Idle;
        anim.Play("Idle", 0);
        GameEvents.InvokeLevelStarted();
    }

    void StartBoost()
    {
        rb.simulated = true;
        rb.AddForce(Vector3.up * 6f, ForceMode2D.Impulse);
        hasStarted = true;

        StartCoroutine(StartStateSwitch());
    }

    void Explode()
    {
        state = BoostStates.Locked;
        rb.simulated = false;
        anim.Play("Explode", 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If not an obstacle, checks impact speed to check for crash
        if (!other.gameObject.CompareTag("Obstacle"))
        {
            float impactSpeed = other.relativeVelocity.magnitude;

            if (impactSpeed > crashSpeed)
            {
                GameEvents.InvokeLevelFailed();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameEvents.InvokeLevelFailed();
        }
    }
}