using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{           
    private Rigidbody2D rb;
    private Animator anim;
    private enum State {idle, run, jump, falling}; //Set a INT to idle, run and jump
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask Ground;

    int xDir = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if(hDirection > 0){
            rb.linearVelocity = new Vector2(xDir,0);
            transform.localScale = new Vector2(1,1); //sprite looks right
             
        }
        else if(hDirection < 0){

        
            rb.linearVelocity = new Vector2(-5,0); 
            transform.localScale = new Vector2(-1,1); //sprite looks left
            
        }
        else{
            
        }
        
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground)){
            rb.linearVelocity= new Vector2(rb.linearVelocity.x, 10f);
            state = State.jump;
        }

        VelocityState();
        anim.SetInteger("State", (int)state);

    }

    private void VelocityState(){

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

    }

}
