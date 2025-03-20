using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private Rigidbody2D rb; //private variable and methods because we dont want to access them from somwhere else
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, run, jump, fall }  //own datatype that manages all different animation states bc we cannot have 2 animations at the same time
                                                          // idle = 0, run=1, jump=2, fall=3

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //better runtime
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>(); //swp direction in which the player is facing
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        //getAxisRaw = stop immediately upon button release
        directionX = Input.GetAxisRaw("Horizontal"); //left = -1, right= +1, when joystick its a number in between
        rb.linearVelocity = new Vector2(directionX * moveSpeed, rb.linearVelocity.y); //multiply with directionX instead of if clause so if the directionX is negative the outcome will be negative aswell = move left + controller support (moving slower)

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //vector3 is a data holder for 3 values XYZ, velocity = what speed in which direction
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovementState   State;

        if (directionX > 0f)
        {
            State = MovementState.run;
            sprite.flipX = false;
        }
        else if (directionX < 0f)
        {
            State = MovementState.run;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.idle;
        }

        //jumping or falling has a higher priority than the other animations cause u can jump while moving in a direction thats why this if clause comes 2nd

        if ( rb.linearVelocity.y > .1f) //as long as the y velocity is greater than .1 we know that we are jumping
        {
            State = MovementState.jump;
        }
        else if( rb.linearVelocity.y < -.1f) //downward force
        {
            State = MovementState.fall;
        }

        anim.SetInteger("State", (int) State); //(int) turns enum into an int)
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); //boxcast return bool, this method layers a box over the collider of the sprite just a bit lower to the ground to check if it overlaps with ground
    }
}
