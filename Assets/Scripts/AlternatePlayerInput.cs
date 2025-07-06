using UnityEngine;
using UnityEngine.InputSystem;

public class AlternatePlayerInput : MonoBehaviour
{
    /// <summary>
    /// Contains tunable parameters to tweak the player's movement.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Header("Movement Settings")]
        [Tooltip("The player's current speed.")]
        public float speed;

        [Tooltip("The fastest speed the player can go.")]
        public float speedMaximum;

        [Tooltip("The slowest speed the player can drop to.")]
        public float speedMinimum;

        [Tooltip("How fast the player turns left and right.")]
        public float turnSpeed;


        [Tooltip("How much speed the player picks up as they're turning towrds the center.")]
        public float turnAcceleration;

        [Tooltip("How much speed the player drops as they're turning towards the sides.")]
        public float turnDeceleration;

    }
    public Stats playerStats;

    [Tooltip("Child GameObject to check if we are on the ground")]
    public Transform groundCheck;

    [Tooltip("Layermask to hold layers to check for being grounded")]
    public LayerMask groundLayers;
    private Rigidbody rb;
    private float movementX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
    }

    private void FixedUpdate()
    {
        float turnAngle = Mathf.Abs(180 - transform.eulerAngles.y);
        playerStats.speed += Remap(0, 90, playerStats.turnAcceleration, -playerStats.turnDeceleration, turnAngle);

        ControlSpeed();

        // 左右移動（Input System）
        Vector3 movement = new Vector3(movementX, 0.0f, 0.0f);
        rb.AddForce(movement * playerStats.speed * 0.1f);

        // 自動前進（スロープに沿って滑る）
        bool onGround = Physics.Linecast(transform.position, groundCheck.position, groundLayers);
        if (onGround)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f, groundLayers))
            {
                Vector3 groundNormal = hit.normal;
                float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);
                if (slopeAngle > 0.1f)
                {
                    Vector3 slopeDirection = Vector3.Cross(Vector3.Cross(groundNormal, Vector3.down), groundNormal).normalized;
                    rb.AddForce(slopeDirection * playerStats.speed * 0.1f);
                }
            }
        }
    }

    private void ControlSpeed()
    {

        // limits the player's speed when reaching past the speed maximum
        if (playerStats.speed > playerStats.speedMaximum)
        {
            playerStats.speed = playerStats.speedMaximum;
        }

        // limits the player from moving any slower than the speed minimum
        if (playerStats.speed < playerStats.speedMinimum)
        {
            playerStats.speed = playerStats.speedMinimum;
            
        }

    }

    // remaps a number from a given range into a new range
    private float Remap(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        return (NewValue);
    }
}
