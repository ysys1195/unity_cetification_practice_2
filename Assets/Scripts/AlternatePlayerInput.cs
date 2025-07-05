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
    private float movementY;
    // public float speed = 50;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue movementValue)
    {
        bool onGround = Physics.Linecast(transform.position, groundCheck.position, groundLayers);
        if (onGround)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }

        float turnAngle = Mathf.Abs(180 - transform.eulerAngles.y);
        playerStats.speed += Remap(0, 90, playerStats.turnAcceleration, -playerStats.turnDeceleration, turnAngle);

        // 前進方向の速度を設定
        Vector3 velocity = transform.forward * playerStats.speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * playerStats.speed);

        ControlSpeed();

        // animator.SetFloat("playerSpeed", playerStats.speed);
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

