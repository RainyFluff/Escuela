using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;
    [SerializeField] private float WallrunningGravity;
    [SerializeField] private float WallrunningJumpForce;


    bool wallLeft = false;
    bool wallRight = true;
    RaycastHit LeftwallHit;
    RaycastHit RightwallHit;


    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float WallRunfov;
    [SerializeField] private float WallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt { get; private set; }




    private Rigidbody rb;
    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }
    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out LeftwallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out RightwallHit, wallDistance);
    }

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }
    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                print("Wall on the left");
                StartWallRun();
            }
            else if (wallRight)
            {
                print("Wall on the right");
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
            
            
            

        }
        else
        {
            StopWallRun();
        }
    }



    void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * WallrunningGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, WallRunfov, WallRunfovTime * Time.deltaTime);

        if (wallLeft)
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if (wallRight)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 WallRunJumpDirection = transform.up + LeftwallHit.normal;

                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);

                rb.AddForce(WallRunJumpDirection * WallrunningJumpForce * 100, ForceMode.Force);
            }

            if (wallRight)
            {
                Vector3 WallRunJumpDirection = transform.up + RightwallHit.normal;

                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);

                rb.AddForce(WallRunJumpDirection * WallrunningJumpForce * 100, ForceMode.Force);

            }

        }
    }

    void StopWallRun()
    {
        rb.useGravity = true;
       cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, WallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}
