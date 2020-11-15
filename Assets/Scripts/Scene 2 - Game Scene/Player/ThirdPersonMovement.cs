using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] public PlayerStats player;
    [SerializeField] private CharacterController controller;
    [Header("Camera settings")]
    [SerializeField] public Transform cam; //-------------
    [SerializeField] public float turnSmoothTime = 0.1f;
    [SerializeField] public float gravity = -9.81f;
    float turnSmoothVelocity;

    private bool isGrounded;

    [SerializeField] private Vector3 playerVelocity;

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
    }
    void Update()
    {
        Jump();
        Movement();
    }
    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;//-------------
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;//-------------

            float movementSpeed = player.speed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movementSpeed = player.sprintSpeed;
            }
            else
            {
                movementSpeed = player.crouchSpeed;
            }

            controller.Move(moveDir * movementSpeed * Time.deltaTime);
        }
    }
    private void Jump()
    {
        if(controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(player.jumpHeight * -3.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    bool IsGrounded()
    {
        //debug raycast
        Debug.DrawRay(transform.position, -Vector3.up * ((controller.height * 0.5f) * 1.1f), Color.red);

        // bit shift the index of the layer x8 to get a bit mask
        int layerMask = 1 << 8; //8 would be the layer we are ignoring
        //this would cat rays only against colliders in layer 8
        //collide against everything except layer 8
        layerMask = ~layerMask;

        RaycastHit hit;
        //ignoring layer 8 which is the player layer
        if (Physics.SphereCast(transform.position, controller.radius, -Vector3.up, out hit, controller.bounds.extents.y * 0.1f - controller.bounds.extents.x, layerMask))
        {
            return true;
        }
        return false;
    }   
}
