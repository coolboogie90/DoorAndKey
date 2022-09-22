using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CollapsingPlatform : MonoBehaviour
{
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
        rb.constraints = RigidbodyConstraints2D.FreezeAll; //bouge plus du tout
    }

    //Expliquer sans expliquer, montrer mais ne pas expliquer, forcer le joueur à passer dessus pour qu'il comprenne la mécanique
        private void AddGravity()
    {
        rb.gravityScale = 2f; //tombe très vite
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        
        foreach(Collider2D col in GetComponents<BoxCollider2D>())
        {
            if(col.isTrigger == false)
            {
                col.enabled = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Invoke("AddGravity", 1f); //"commence à tomber après 1sec  
        }
    }
}
