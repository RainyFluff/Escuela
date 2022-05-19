using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword1 : MonoBehaviour
{

    //Animator animator;

    RaycastHit SwordHit;

    public GameObject Orientation;

    public float Range = 40f;

    public float Damage = 10f;

    bool IsBlocking = false;

    //Variabler

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attack();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            print("Blocking");
            blocking();
        }
        else
        {
            unblock();
           
        }
    }

    void attack()
    {
        
        
        
        
        if (Physics.Raycast(Orientation.transform.position, Orientation.transform.TransformDirection(Vector3.forward), out SwordHit, 40f))
        {
            Target target = SwordHit.transform.GetComponent<Target>();

            Debug.Log(SwordHit.transform.name);

            if (target != null)
            {
                target.TakeDamage(Damage);
            }

        }

        
        //Attacken i sig självt enbart nummer och raycasten inga animationer.
        

    }

    void blocking()
    {
        IsBlocking = true;

       
    }
    void unblock()
    {
        IsBlocking = false;
    }
    //Framework för en blocking funktion
}
