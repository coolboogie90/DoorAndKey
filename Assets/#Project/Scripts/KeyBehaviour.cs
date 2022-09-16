using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [HideInInspector] public LevelManager manager;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false); //désactive l'objet, on aurait pu aussi le détruire
            manager.KeyIsReached(); //le LevelManager signale au KeyBehaviour que la porte est ouverte
        }
    }
    
}
