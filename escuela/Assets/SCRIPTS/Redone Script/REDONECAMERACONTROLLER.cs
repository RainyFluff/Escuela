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
    private void Start()
    {
        

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MyInput();

        Cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, WallRun.tilt);
        Orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }




}
