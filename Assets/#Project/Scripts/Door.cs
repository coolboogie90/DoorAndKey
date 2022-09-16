using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))] //va forcer notre objet door à aller chercher un animator
public class Door : MonoBehaviour
{
    [HideInInspector] public LevelManager manager;
    public bool isOpen = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Opening()
    {
        isOpen = true;
        animator.SetBool("doorIsOpen", true);  //va mettre le bool doorIsOpen placé dans l'animation à true 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(isOpen)
            {
                manager.DoorIsReached();
            }
        }
    }
}
