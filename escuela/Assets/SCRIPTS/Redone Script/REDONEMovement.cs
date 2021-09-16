using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REDONEMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;
    public float SpeedMultiplier = 10;

    [Header("Drag")]
    public float rbDrag = 6;
    

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        myInput();
        ControlDrag();
    }

    void ControlDrag()
    {
        rb.drag = rbDrag;

    }

    void myInput()
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
        rb.AddForce(moveDirection.normalized * MoveSpeed * SpeedMultiplier, ForceMode.Acceleration);

    }


}
