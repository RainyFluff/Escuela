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

    //En simpel target, interact med Sword1, förstör ett objekt efter det har tagit en viss mängd "skada".
}

