using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class Movement : MonoBehaviour
{
    // The Rigidbody attached to the GameObject.
    public Rigidbody body;
    /// <summary>
    /// Rotation Speed scale for turning.
    /// </summary>
    public float rotationSpeed;
    /// <summary>
    /// The upwards jump force of the player.
    /// </summary>
    public float jumpForce;

    public UnityEvent resetEvent = new UnityEvent();
    // The horizontal input from input devices.
    private float horizontal;
    // Whether or not the player is on the ground.
    private bool isGrounded;

    private float isBoosting;

    private float resetTime = 0f;

    private const float jumpForceMultiplier = 1000f;

    private bool isPaused = false;

    // Initialization function
    void Start()
    {
        ResetRocket();
    }

    void Update()
    {
        if (resetTime > 0)
        {
            resetTime -= Time.deltaTime;
        } else
        {
            resetTime = 0f;
        }
    }

    void FixedUpdate() {
        if (resetTime <= 0 && !isPaused)
        {
            transform.Rotate((transform.forward * horizontal * -1f) * rotationSpeed * Time.fixedDeltaTime);
            body.AddForce(transform.up * jumpForce * isBoosting * Time.fixedDeltaTime * jumpForceMultiplier);
        }
    }

    public void OnRotate(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        horizontal = inputMovement.x;
    }

    public void OnReset(InputAction.CallbackContext value)
    {
        ResetRocket();
        
    }

    public void OnBoost(InputAction.CallbackContext value)
    {
        isBoosting = value.ReadValue<float>();
    }
    
    // This function is a callback for when an object with a collider collides with this objects collider.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
        } else
        {
            
            ResetRocket();
        }
    }
    // This function is a callback for when the collider is no longer in contact with a previously collided object.
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = false;
        } else 
        {
            ResetRocket();
        }
    }

    void ResetRocket()
    {
        Debug.Log("RESET ROCKET");
        body.position = new Vector3(0, 0.5f, 0);
        body.velocity = Vector3.zero;
        body.rotation = Quaternion.identity;
        body.angularVelocity = Vector3.zero;
        resetTime = 0.1f;
        resetEvent.Invoke();
    }

    public void OnLevelFinished()
    {
        isPaused = true;
        body.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void OnPlayAgain()
    {
        isPaused = false;
        FreezeZPositionAndXYRotation();
        ResetRocket();
    }

    private void FreezeZPositionAndXYRotation()
    {
        body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

}
