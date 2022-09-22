using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public Rigidbody2D rb;
    private float delay = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            rb.gravityScale = 1f;    
        }
    }
}
