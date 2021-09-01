using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 6f;
    public float Drag = 6f;
    public float AirDrag = 2f;
    public float moveSpeedMult = 10f;
    public float PlayerHeight = 2f;

    float horizontalMovement;
    float verticalMovement;

    public float JumpForce = 10f;

    bool IsGrounded;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight / 2 + 0.1f);
        print(IsGrounded);
        
        MyInput();
        ControlDrag();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }

    void ControlDrag()
    {
        

        if (IsGrounded)
        {
            rb.drag = Drag;
        }

        if (!IsGrounded)
        {
            rb.drag = AirDrag;
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }
    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * moveSpeedMult, ForceMode.Acceleration);
    }
}
