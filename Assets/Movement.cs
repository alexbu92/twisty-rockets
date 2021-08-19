using UnityEngine;
public class Movement : MonoBehaviour
{
    // The Rigidbody attached to the GameObject.
    public Rigidbody body;
    /// <summary>
    /// Speed scale for the velocity of the Rigidbody.
    /// </summary>
    public float speed;
    /// <summary>
    /// Rotation Speed scale for turning.
    /// </summary>
    public float rotationSpeed;
    /// <summary>
    /// The upwards jump force of the player.
    /// </summary>
    public float jumpForce;
    // The horizontal input from input devices.
    private float horizontal;
    // Whether or not the player is on the ground.
    private bool isGrounded;

    private float resetTime = 0f;
    // Initialization function
    void Start()
    {
        
 
    }

    void Update()
    {
        if (resetTime > 0)
        {
            resetTime -= Time.deltaTime;
        } else
        {
            resetTime = 0f;
            horizontal = Input.GetAxis("Horizontal");
            bool reset = Input.GetButtonDown("Reset");
            if (reset)
            {
                ResetRocket();
            }
            Debug.Log(Input.GetAxis("Jump"));
            if (Input.GetAxis("Jump") > 0)
            {
                body.AddForce(transform.up * jumpForce);
            }
            
        }
    }

    void FixedUpdate() {
        if (resetTime <= 0)
        {
            transform.Rotate((transform.forward * horizontal * -1f) * rotationSpeed * Time.fixedDeltaTime);
        }
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
        } else {
            ResetRocket();
        }
    }

    void ResetRocket()
    {
        body.position = new Vector3(0, 0.5f, 0);
        body.velocity = Vector3.zero;
        body.rotation = Quaternion.identity;
        body.angularVelocity = Vector3.zero;
        resetTime = 0.5f;
    }

}
