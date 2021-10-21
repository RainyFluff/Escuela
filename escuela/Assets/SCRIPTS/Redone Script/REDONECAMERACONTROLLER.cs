using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REDONECAMERACONTROLLER : MonoBehaviour
{
    [SerializeField] Wallrunning WallRun;


    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] Transform Cam;
    [SerializeField] Transform Orientation;
    float mouseX;
    float mouseY;

    float multiplier = 0.1f;

    float xRotation;
    float yRotation;

    //Alla variabler som anv�nds
    private void Start()
    {
        

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //l�ser min cursor s� att den inte �r i v�gen.
    }

    private void Update()
    {
        MyInput();

        Cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, WallRun.tilt);
        Orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        //H�ller koll p� v�r rotation och ser till att den f�ljer v�r Orientations rotation d�r det beh�vs, men fortfarande f�ljer v�r Kameras localrotation.
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //s�tter clamps f�r att limita Kameran och best�mmer input f�r min kamera.
    }




}
