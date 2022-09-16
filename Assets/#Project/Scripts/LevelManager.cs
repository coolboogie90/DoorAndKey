using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public KeyBehaviour key;
    public Door door;
    public string nextLevel;

    void Start()
    {
        if(key==null)
        {
            Debug.LogError("LevelManager needs a key.", gameObject); //en rajoutant va pointer à l'objet qui pose problème dans Unity
        }
        else
        {
            key.manager = this; //le manager s'assigne lui-même à la variable key, "s'il y a une clé, j'en suis le manager"
        }

        if(door==null)
        {
            Debug.LogError("LevelManager needs a door.", gameObject); 
        }
        else
        {
            door.manager = this; 
        }
    }
    
    public void KeyIsReached()
        {
            //Debug.Log("Door open!");
            door.Opening();
        }

    public void DoorIsReached()
    {
        SceneManager.LoadScene(nextLevel);
    }
}

