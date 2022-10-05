using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class HeroInputManager : MonoBehaviour
{
    // !!! doit avoir le même nom que le script Csharp généré dans Unity pour la map
    private InputManager actions; 

    public float moveSpeed = 4f;
    public float jumpForce = 2f;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Awake()
    {
        actions = new InputManager();
        moveAction = actions.Player.Move; //on va chercher la composante du mouvement, contenu dans l'action map Player de notre InputManager
        moveAction.Enable();
        jumpAction = actions.Player.Jump;
        jumpAction.Enable();
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        actions.Player.Jump.started += JumpStart; //on l'ajoute au listener
        actions.Player.Jump.canceled += JumpEnd; //on l'ajoute au listener

    }
    void OnDisable()
    {
        actions.Player.Jump.started -= JumpStart; //on retire l'event' du listener
        actions.Player.Jump.canceled -= JumpEnd; //on retire l'event' du listener
    }
    void JumpStart(InputAction.CallbackContext context)
    {
        isJumping = true;
        print("Start of the jump...");
    }
    
    void JumpEnd(InputAction.CallbackContext context)
    {
        isJumping = false;
        print("...End of the jump.");
    }
    void Update()
    {

        if(isJumping)
        {
            print("...Jumping...");
            rb.AddForce(Vector2.up * 400);
        }
    }
    void FixedUpdate() //sert à faire les calculs physiques de manière homogène
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>() * moveSpeed * Time.fixedDeltaTime;
        print("Moving.");
        rb.MovePosition(transform.position + (Vector3)moveDirection);
        
        float velocity = Mathf.Abs(moveDirection.x); //on regarde via la variable absolue si notre player va à gauche ou à droite sur l'axe x (-1 = droite; +1 = gauche)
        animator.SetFloat("speed", velocity); //on transmet notre vitesse à l'animator

        if(moveDirection.x != 0) //expression ternaire en deux lignes, plus court à écrire que les ifs ici
        {
            sprite.flipX = moveDirection.x < 0;
        }
    
    /* #region Flip direction with if conditions */
        
        // if(moveDirection.x > 0) //"si je vais vers la droite, tu ne flip pas le sprite"
        // {
        //     sprite.flipX = false;
        // }

        // else if(moveDirection.x < 0) //"si je vais vers la gauche, tu flip le sprite"
        // {
        //     sprite.flipX = true;
        // }
        // "sinon, tu ne fais rien"
    
    /* #endregion */

    }
}
