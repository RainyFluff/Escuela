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
    //Ser till att kameran �r vid en "empty" position
    //beh�vde g�ra dettta f�r unity magin till�ter d� kameran att inte f� en spasm attack n�r jag r�r mig diagonalt.
}
