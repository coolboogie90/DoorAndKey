using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 translation = Vector2.right; //mouvement de la plateforme
    [Range(0, 10f)] public float timeToMove = 1f;
    private bool reverse = false;

    private Vector3 start; //utile pour le style 2.5D
    private Vector3 end;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = transform.position + (Vector3)translation; //casting de la translation qui est au départ un Vector2
        StartCoroutine(Move());
    }

    private IEnumerator Move() 
    {
        float time = 0f;
        float ratio = 0f;
        while(true)
        {
            while (ratio < 1)
            {
                time += Time.deltaTime;
                ratio = time/timeToMove; //permet de calculer la proportion de chemin que l'on a faite
                
                if(reverse) transform.position = Vector3.Lerp(end, start, ratio);
                else transform.position = Vector3.Lerp(start, end, ratio);

                yield return null; //avec le null = "attends la fin de la frame avant de recommencer"
            }
            time = 0f;
            ratio = 0f;
            reverse = !reverse;
        }
    }
    //Si on veut bouger un objet avec un autre, il faut parenter/déparenter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = transform; //le transform de ma plateforme devient le parent de ce qui vient d'entrer dans le second box collider2D de ma plateforme (il y a 2 boxcoll : un sans trigger, un avec pour le parentage)
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = null; //on déparente
        }
    }

    void OnDrawGizmos()
    {
        Collider2D collider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawCube(collider.bounds.center + (Vector3) translation, collider.bounds.size);
    }

}
