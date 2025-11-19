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
    readonly float turnSpeed = 6f, thrustForce = 12.5f, maxVelocity = 80f, defaultGravity = 1.5f, crashSpeed = 9f;
    bool hasStarted = false;
    int dir = 0; // 1 left, -1 right

    ButtonControls buttonControls;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    public BoostStates state;
    public TurnStates turnState;

    void Start()
    {
        //GameEvents.OnGameOver += Explode;

        buttonControls = ButtonControls.instance;

        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        state = BoostStates.Locked;
    }

    void Update()
    {
        CheckForInput();
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
            rb.gravityScale = 0.5f;

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

    //void Explode()
    //{
    //    state = BoostStates.Locked;
    //    rb.simulated = false;
    //    anim.Play("Explode", 0);
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameEvents.InvokeLevelFailed();
        }

        // If not an obstacle, checks impact speed to check for crash
        else
        {
            float impactSpeed = other.relativeVelocity.magnitude;

            if (impactSpeed > crashSpeed)
            {
                GameEvents.InvokeLevelFailed();
            }
        }
    }
}

