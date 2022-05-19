using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
   


    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

    }


    void Die()
    {
        Destroy(gameObject);
    }

    //En simpel target, interact med Sword1, f�rst�r ett objekt efter det har tagit en viss m�ngd "skada".
}

