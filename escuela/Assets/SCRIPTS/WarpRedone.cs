using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpRedone : MonoBehaviour
{
    public GameObject MainWorld;
    public GameObject SubWorld;

    bool CurrentState = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse3) && CurrentState == false)
        {
            CurrentState = true;



        }
       
        if (Input.GetKey(KeyCode.Mouse3) && CurrentState == true)
        {
            CurrentState = false;

        }

        if (CurrentState)
        {
            MainWorld.SetActive(true);
            SubWorld.SetActive(false);

        }

        if (CurrentState == false)
        {
            MainWorld.SetActive(false);
            SubWorld.SetActive(true);
        }






    }
}
