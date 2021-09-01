using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WARP : MonoBehaviour
{

    public GameObject MainWorld;
    public GameObject SubWorld;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse3))
        {
            MainWorld.SetActive(false);
            SubWorld.SetActive(true);

        }
        else
        {
            MainWorld.SetActive(true);
            SubWorld.SetActive(false);

        }




    }
}
