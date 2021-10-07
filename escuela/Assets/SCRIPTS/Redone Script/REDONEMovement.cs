using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REDONEMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;
    public float SpeedMultiplier = 10;
    float playerHeight = 1.5f;
    public float JumpForce = 5f;

    [Header("Drag")]
    public float groundDrag = 6;
    public float airDrag = 2;
    [SerializeField] float airMultiplier = 0.4f;

    [Header("Inputs")]
    [SerializeField] KeyCode jumpkey = KeyCode.Space;
    [SerializeField] KeyCode sprintkey = KeyCode.LeftShift;
    
    
    float horizontalMovement;
    float verticalMovement;

    [Header("Sprint")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;


    [SerializeField] Transform Orientation;

    Vector3 moveDirection;
    Vector3 slopemoveDirection;
    Rigidbody rb;

    [Header("Ground")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask GroundMask;
    bool IsGrounded;
    public float GroundDistance = 0.4f;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        return false;

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance, GroundMask);

        myInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpkey) && IsGrounded)
        {
            Jump();
            

        }

        slopemoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
       
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintkey) && IsGrounded)
        {

            MoveSpeed = Mathf.Lerp(MoveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            MoveSpeed = Mathf.Lerp(MoveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }
    void ControlDrag()
    {
        if (IsGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }

    }


    
    void myInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = Orientation.transform.forward * verticalMovement + Orientation.transform.right * horizontalMovement;

    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);

    }
    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    void MovePlayer()
    {
       if (IsGrounded && !OnSlope())
       {
            rb.AddForce(moveDirection.normalized * MoveSpeed * SpeedMultiplier, ForceMode.Acceleration);
       }
       else if (IsGrounded && OnSlope())
       {
            rb.AddForce(slopemoveDirection.normalized * MoveSpeed * SpeedMultiplier, ForceMode.Acceleration);
        }
       else if (!IsGrounded)
       {
            rb.AddForce(moveDirection.normalized * MoveSpeed * SpeedMultiplier * airMultiplier, ForceMode.Acceleration);
       }
        

    }


}
