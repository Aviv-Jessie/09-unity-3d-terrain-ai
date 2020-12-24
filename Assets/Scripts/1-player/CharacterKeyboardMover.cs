using System.Collections;
using UnityEngine;


/**
 * This component moves a player controlled with a CharacterController using the keyboard.
 */
[RequireComponent(typeof(CharacterController))]

public class CharacterKeyboardMover : MonoBehaviour
{
    // the speed and gravity of the character
    [Tooltip("Speed of player keyboard-movement, in meters/second")]
    [SerializeField] float _speed = 3.5f;
    [SerializeField] float _gravity = 9.81f;

    // the jump force of the character
    [Tooltip("Jump of player keyboard-movement, in meters/second")]
    [SerializeField] public float jumpForce = 6.0f;

    // the run speed
    [Tooltip("Run of player keyboard-movement, in meters/second")]
    [SerializeField] public float Run = 10.0f;

    // the crawl speed
    [Tooltip("Crawl of player keyboard-movement, in meters/second")]
    [SerializeField] public float Crawl = 2.0f;

    // groundedpPlayer = character controller -> isGrounded.
    private bool groundedPlayer;
    private CharacterController _cc;
    public Vector3 velocity;

    void Start()
    {
        // init the _cc. and velocity.
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // the movement factor
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // update the movement..
        velocity.x = x * _speed;
        velocity.z = z * _speed;

        groundedPlayer = _cc.isGrounded;

        // gravity takes the player down
        if (!groundedPlayer)
        {
            velocity.y -= _gravity * Time.deltaTime;
        }

        // the player is on the ground and we clicked "Space" meaning jump
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            velocity.y += jumpForce;
        }

        // the player is on the ground and we clicked "Shift" meaning run
        if ((Input.GetButton("Run")) && groundedPlayer)
        {
            velocity.x = x * Run;
            velocity.z = z * Run;
        }


        var rotation = Quaternion.identity;
        // the player is on the ground and we clicked "Ctrl" meaning crawl
        if ((Input.GetButton("Crawl")) && groundedPlayer)
        {
            velocity.x = x * Crawl;
            velocity.z = z * Crawl;

            // make the player crawl
            _cc.height = 0.5f;
            rotation *= Quaternion.Euler(90, 0, 0); // this add a 90 degrees Y rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }
        else
        {
            // back to stand up..
            _cc.height = 2f;
            rotation *= Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }


        // update the transform from local transform to the "world space transform"
        velocity = transform.TransformDirection(velocity);
        _cc.Move(velocity * Time.deltaTime);
    }
}