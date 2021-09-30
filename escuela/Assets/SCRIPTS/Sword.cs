using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public float SlashRange = 5f;

    Vector3 fwd = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }


    void Attack()
    {
        Physics.Raycast(transform.position, Vector3.forward, 5);
        print(RaycastHit);
    }

}


