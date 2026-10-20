using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAccelDeccel : MonoBehaviour
{
    
    public float accelerationTime = 1f;
    public float decelerationTime = 1f;
    public float maxSpeed = 1f;

    [Tooltip("Measured in degrees per second")]
    public float angularSpeed = 90f;
    private float acceleration;
    private float deceleration;


    private Vector3 velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        PlayerRotation();
    }

    public void playerMovement() 
    {
        Vector3 playerInput = Vector3.zero;
        Keyboard current = Keyboard.current;

        if (current.leftArrowKey.isPressed || current.aKey.isPressed)
        {
            playerInput += Time.deltaTime * acceleration *  Vector3.left;
        }
        if (current.rightArrowKey.isPressed || current.dKey.isPressed)
        {
            playerInput += Vector3.right;
        }
        if (current.downArrowKey.isPressed || current.sKey.isPressed)
        {
            playerInput += Vector3.down;
        }
        if (current.upArrowKey.isPressed || current.wKey.isPressed)
        {
            playerInput += Vector3.up;
        }

        if (playerInput.sqrMagnitude > 0) //some input detected
        {
            velocity += acceleration * Time.deltaTime * playerInput.normalized;

            if (velocity.sqrMagnitude > maxSpeed * maxSpeed)// going too fast
            {
             velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            }
              else // No input detected
              {
                float deltaVelocity = deceleration * Time.deltaTime; //deceleration this frame

                if (velocity.magnitude > deltaVelocity) //if we still need to slow down
                {
                    velocity -= deltaVelocity * velocity.normalized; //decelerate
                }
                else 
                {
                    velocity = Vector3.zero; //complete stop
                }
              }
        }
        transform.position += Time.deltaTime * velocity; // move relative to velocity
    }

    private void PlayerRotation() 
    {
        if (velocity.sqrMagnitude == 0) return; //not moving do nothing

        float deltaAngle = ComputeShortestAngle(transform.up, velocity);

        if (deltaAngle == 0) return; // locked on do nothing

        float rotationDirection = Mathf.Sign(deltaAngle);
        float rotationStep = angularSpeed * Time.deltaTime;

        if (rotationStep < Mathf.Abs(deltaAngle)) 
        {
            transform.Rotate(0, 0, rotationStep * rotationDirection); // rotate relative to direction
        }
        else
        {
            transform.Rotate(0, 0, deltaAngle); // lock on
        }
    }

    private float ComputeShortestAngle(Vector3 a, Vector3 b) 
    {
        float angleA = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
        float angleB = Mathf.Atan2(b.y, b.x) * Mathf.Rad2Deg;

        return Mathf.DeltaAngle(angleA, angleB);
    }
}
