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
    private float maxWalkSpeedX = 5.5f;
    private float releaseDecelerationX = .25f;
    private float skidDecelerationX = .5f;
    private float skidTurnaroundX = 3.5f;


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

                currentSpeedX = IncreaseWithinBound(currentSpeedX, walkAccelarationX, maxWalkSpeedX); //accelerate, then keep at max speed

            }
        }else if(currentSpeedX > 0){

            currentSpeedX = DecreaseWithinBound(currentSpeedX, releaseDecelerationX, 0);

        }

        isChangingDirection = currentSpeedX > 0 && faceDirectionX * moveDirectionX < 0; //if player is moving + in negative direction
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
       if (isChangingDirection)
        {
            if (currentSpeedX > skidTurnaroundX)
            {
                moveDirectionX = -moveDirectionX;  // instead of -faceDirectionX (avoids 0-problem)
                currentSpeedX = DecreaseWithinBound(currentSpeedX, skidDecelerationX, 0);
                Debug.Log("Richtungswechsel: Geschwindigkeit wird abgebremst. currentSpeedX: " + currentSpeedX);
            }else
            {
                moveDirectionX = faceDirectionX;
                currentSpeedX = minWalkSpeedX;  // 🚀 instant reset to min-speed after turnaround
                Debug.Log("Richtungswechsel: Geschwindigkeit wird auf minWalkSpeedX gesetzt. currentSpeedX: " + currentSpeedX);
            }
        }
        else
        {
            if (faceDirectionX != 0)
            {
                moveDirectionX = faceDirectionX;
            }

            // 🚀 instant turnaroundif we stand still an change direction
            if (currentSpeedX < minWalkSpeedX) 
            {
                moveDirectionX = minWalkSpeedX;  
                Debug.Log("Aktualisierung: Geschwindigkeit auf minWalkSpeedX gesetzt. currentSpeedX: " + currentSpeedX);
            }
        }

         rb.linearVelocity = new Vector2(moveDirectionX * currentSpeedX, rb.linearVelocity.y);

        if(faceDirectionX > 0){

            transform.localScale = new Vector2(1, 1); //sprite facing right
        }
        else if(faceDirectionX < 0){
            transform.localScale = new Vector2(-1, 1); //sprite facing left
              
        }

        

    }

    float IncreaseWithinBound(float val, float delta, float maxVal){

        val += delta; //speed increases

        if(val > maxVal){ //speed limit

            val = maxVal;

        }

        return val;
    }

    float DecreaseWithinBound(float val, float delta, float minVal = 0){

        val -= delta * 1.5f; //decrease speed more heavily to make it fluid
        if(val < minVal){ //speed limit

            val = minVal;

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