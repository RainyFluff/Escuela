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
    public float Gravity = -30;
    public float GravityMultiplier = 1;

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

    [Header("Dashing")]
    public float DashForceGround = 100;
    public float DashForceAir = 10;
    public float CoolDownTime;
    float NextUseTime;

    [Header("Crouching")]
    public GameObject Player;
   
    //En fukton med variabler har nämt dom så bra jag kan.

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
    //En groundcheck för slopes en vanlig groundcheck är bara rak raycast ner i marken vilket kan leda till deadzone.
    private void Start()
    {     
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //Hittar rigidbody och fryser rotation.
        
    }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance, GroundMask);

        myInput();
        ControlDrag();
        ControlSpeed();

        //Funktioner som kallas för rörelse
        //Isgrounded ovan.
       if (Time.time > NextUseTime)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded)
            {
                DashGround();
                NextUseTime = Time.time + CoolDownTime;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !IsGrounded)
            {
                DashAir();
                NextUseTime = Time.time + CoolDownTime;
            }
            //Dash funktion med tid och cooldown.
        }
        
        if (Input.GetKeyDown(jumpkey) && IsGrounded)
        {
            Jump();
            

        }

        slopemoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            StartCrouch();
        }
        else
        {
            StopCrouch();
        }
       
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
        //Kontroller min hastighet så att jag inte råkar phasea igenom verkligheten så som vi forstår den.
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

        //Bestämmer input-keys för movement
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        //Hoppar med hjälp av en addforce
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
       else if (rb.velocity.y > 0.01f)
        {
            GravityController();
        }
       //Bestämmer hastighet/ gravitation

    }

    void GravityController()
    {
        rb.AddForce(transform.up* Gravity , ForceMode.Acceleration);

    }

    void DashGround()
    {
        rb.AddForce(Orientation.transform.forward * DashForceGround, ForceMode.Impulse);
    }

    void DashAir()
    {
        rb.AddForce(Orientation.transform.forward * DashForceAir, ForceMode.Impulse);

    }
    //En för dasha i luften (mindre motstånd kräver mindre kraft)
    //En för mark (friktion kräver mer kraft)
    void StartCrouch()
    {
        //Lower height of player
        transform.localScale = new Vector3(1, 0.5f, 1);
        //Decrease moveSpeed
       
        //Decrease Drag
    }

    void StopCrouch()
    {
        //Lower height of player
        transform.localScale = new Vector3(1, 1, 1);
        //Increase moveSpeed
        
        //Increase Drag
    }
    //En crouch funktion som inte är klar ännu
}
