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
    bool wallRight = false;
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

    //Alla mina variablar, den första och andra segmenten är för att raycasta, hitta väggar samt att bestämma hoppkraft.
    //Den sista är helt runtom kamera tilt när man wallrunnar.


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
    // checkar om man kan wallwunna eller inte. med en raycast.

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        //Hämtar rigidbody från spelare
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
    //Checkar för väggar och kör en funktion baserat på om det finns en.


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
    //Själva wallrunfunktionen, om ett föremål är nära nog spelare åt antingen höger eller vänster så saktas dess gravitations kraft ner drastiskt och de kan gå längs med väggen.
    //Hoppet beräknas med hjälp av vektor-geometri dvs att väggen är vänster och hoppet ska upp då blir vektorn snett uppåt åt vänster, efteråt så invertas detta hoppet och hoppet blir åt motsatt håll jämfört med väggen.
    //Tiltar kameran.

    void StopWallRun()
    {
        rb.useGravity = true;
       cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, WallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
        
    }
    //Stoppar wallrun och skickar tillbaka kameran till sin ursprungliga position.
    //Fun fact om man har på interpolation på karaktären så får kameran en cp/ptsd/epilepsi attack och skakar när man når en viss hastighet (vet inte vilken)
    //Detta är eftersom att funktionen använder en Lerp (Linear interpolation) vilket leder till en clash med interpolation och linjär interpolation.
}
