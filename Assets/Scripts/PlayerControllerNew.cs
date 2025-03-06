using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerControllerNew : MonoBehaviour
{           
    private Rigidbody2D rb;

    private float faceDirectionX;
    public float moveDirectionX;

    private float minWalkSpeedX = .25f;
    private float walkAccelarationX = .014f;
    private float currentSpeedX;
    private float maxWalkSpeedX = 10.5f;

    private bool isChangingDirection;

    //int xDir = 5;
/*
    private Animator anim;
    private enum State {idle, run, jump, falling}; //Set a INT to idle, run and jump
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask Ground; */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float faceDirectionX = Input.GetAxisRaw("Horizontal"); 

        if (faceDirectionX != 0){ //if player is moving
            if(currentSpeedX == 0){ //if player is moving but they stood still before

                currentSpeedX = minWalkSpeedX;   

            }else if(currentSpeedX < maxWalkSpeedX){ //player is moving but less than their max walk speed-up

                currentSpeedX = IncreaseWithinBound(currentSpeedX, walkAccelarationX, maxWalkSpeedX); //keep at max speed

            }
        }

        isChangingDirection = currentSpeedX > 0 && faceDirectionX * moveDirectionX < 0; //if player is moving + in negative direction

        if(isChangingDirection){

            moveDirectionX = -faceDirectionX;

        }else{

            moveDirectionX = faceDirectionX;

        }

        rb.linearVelocity = new Vector2(moveDirectionX * currentSpeedX, rb.linearVelocity.y);

        if(faceDirectionX > 0){

            transform.localScale = new Vector2(1, 1); //sprite facing right
        }
        else if(faceDirectionX < 0){
            transform.localScale = new Vector2(-1, 1); //sprite facing left
              
        }

        

    }

    float IncreaseWithinBound(float val, float delta, float maxval){

        val += delta; //speed increases

        if(val > maxval){ //speed limit

            val = maxval;

        }

        return val;
    }

    /*private void VelocityState(){

            if(state == State.jump){

                if(rb.linearVelocity.y < .1f){
                    state = State.falling;
                }
            }   
                else if(state == State.falling){
                    if(coll.IsTouchingLayers(Ground)){
                        state = State.idle;
                    }
                }
            
            else if(Mathf.Abs(rb.linearVelocity.x) > 2f){

                state = State.run;
            }
            else{

                state = State.idle;
            }

    }*/

}