using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerNew : MonoBehaviour
{           
    private Rigidbody2D rb;

    //public LayerMask GroundLayers;
    //private Transform groundCheck1, groundCheck2;
    private float faceDirX; //direction player is facing
    public float moveDirX; //direction of movement input
    private float currentSpdX;
    private float minSpdX = .25f; //minimum walk speed
    private float maxSpdX = 10.5f;
    private float accelX = .014f; 
    private float decelX = .25f;
    private float skidDecelX = .5f;
    private float skidTurnX = 4f;
    private bool isChangingDirection;

    //private bool isGrounded,isJumping,jumpHeld,jumpRelease;
    //[SerializeField] private int Collectible = 0;
    //[SerializeField] private Text CollectibleText;
    //[SerializeField] private float jumpSpdY;
    /*
    private Animator anim;
    private enum State {idle, run, jump, falling}; //Set a INT to idle, run and jump
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask Ground; */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){ 
        rb = GetComponent<Rigidbody2D>();
        //groundCheck1 = transform.Find("groundCheck1");
        //groundCheck2 = transform.Find("groundCheck2");
        //normalGravity = rb.gravityScale;
        //anim = GetComponent<Animator>();
        //coll = GetComponent<Collider2D>();
    }

   /* void setJumpParams(){
        jumpSpdY = 15f;
    }*/
    // Update is called once per frame
    void Update(){
        Debug.Log("Current Speed:"+currentSpdX);
        float faceDirX = Input.GetAxisRaw("Horizontal");
    //    isGrounded = Physics2D.OverlapPoint(groundCheck1.position,GroundLayers) || Physics2D.OverlapPoint(groundCheck2.position,GroundLayers);

        if (faceDirX != 0){ //if player is moving
            if(currentSpdX == 0){ //if player starts movement from a halt
                currentSpdX = minSpdX;
            }else if(currentSpdX < maxSpdX){ //player is moving but less than their max walk speed-up
                currentSpdX = increaseVelocity(currentSpdX,accelX,maxSpdX); //keep at max speed
            }
        }else if(currentSpdX > 0){
            currentSpdX = decreaseVelocity(currentSpdX,decelX);
        }
        
    
        isChangingDirection = currentSpdX > 0 && faceDirX * moveDirX < 0; //if player is moving in opposite direction

        if(isChangingDirection){
            if(currentSpdX > skidTurnX){   
                moveDirX = -faceDirX;
                currentSpdX = decreaseVelocity(currentSpdX,skidDecelX,0);
            }else{
                moveDirX = faceDirX;
            }
        }

        rb.linearVelocity = new Vector2(moveDirX*currentSpdX,rb.linearVelocity.y);

        if(faceDirX > 0){
            transform.localScale = new Vector2(1,1); //sprite facing right
        }
        else if(faceDirX < 0){
            transform.localScale = new Vector2(-1,1); //sprite facing left           
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Collectible"){
            Destroy(collision.gameObject);
            Collectible += 1;
            CollectibleText.text = Collectible.ToString();
        }    
    }*/

    float increaseVelocity(float val, float delta, float maxVal){
        val += delta; //speed increases

        if(val > maxVal){ //speed limit
            val = maxVal;
        }
        return val;
    }

    float decreaseVelocity(float val, float delta, float minVal = 0){
        val -= delta; //speed decreases

        if(val < minVal){ //minimum speed
            val = minVal;
        }
        return val;
    }
}