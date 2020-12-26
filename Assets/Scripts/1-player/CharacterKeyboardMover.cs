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
    [SerializeField] public float jumpForce = 10.0f;

    // the run speed
    [Tooltip("Run of player keyboard-movement, in meters/second")]
    [SerializeField] public float Run = 10.0f;

    // groundedpPlayer = character controller -> isGrounded.
    private bool groundedPlayer;
    private CharacterController _cc;
    public Vector3 velocity;

    void Start()
    {
        // init the _cc- character controller
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // the movement factor to update the movement..
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // walking
        velocity.x = x * _speed;
        velocity.z = z * _speed;

        // update the "groundedPlayer"- if touching the ground during the last move ?
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

        // update the transform from local transform to the "world space transform"
        velocity = transform.TransformDirection(velocity);
        _cc.Move(velocity * Time.deltaTime);
    }
}