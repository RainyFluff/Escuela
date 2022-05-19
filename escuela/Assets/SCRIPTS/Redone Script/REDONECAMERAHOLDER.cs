using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REDONECAMERAHOLDER : MonoBehaviour
{
   
    [SerializeField] Transform CameraPosition;


    void Update()
    {
        transform.position = CameraPosition.position;
    }
    //Ser till att kameran är vid en "empty" position
    //behövde göra dettta för unity magin tillåter då kameran att inte få en spasm attack när jag rör mig diagonalt.
}
