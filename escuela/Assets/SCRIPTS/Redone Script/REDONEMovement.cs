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

    float horizontalMovement;
    float verticalMovement;

    [SerializeField] Transform Orientation;

    Vector3 moveDirection;

    Rigidbody rb;

    bool IsGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.1f);

        myInput();
        ControlDrag();

        if (Input.GetKeyDown(jumpkey) && IsGrounded)
        {
            Jump();
            

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
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);

    }
    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    void MovePlayer()
    {
       if (IsGrounded)
        {
            rb.AddForce(moveDirection.normalized * MoveSpeed * SpeedMultiplier, ForceMode.Acceleration);
        }
        else if (!IsGrounded)
        {
            rb.AddForce(moveDirection.normalized * MoveSpeed * SpeedMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        

    }


}
