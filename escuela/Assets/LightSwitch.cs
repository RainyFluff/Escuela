using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject Light;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Light.SetActive(false);
           
        }
        else
        {
            Light.SetActive(true);
        }

      
        //Medans du trycker E så sätts en lampa på eller av.
       
    }


    





}
