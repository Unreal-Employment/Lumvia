using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{           
    private Rigidbody2D rb;

    private Animator anim;

    private enum State {idle, run, jump}; //Set a INT to idle, run and jump

    private State state = state.idle;

    int xDir = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if(hDirection > 0){
            rb.linearVelocity = new Vector2(xDir,0);
            transform.localScale = new Vector2(1,1); //Sprite schaut nach rechts
            anim.SetBool("running", true); 
        }
        else if(hDirection < 0){

        
            rb.linearVelocity = new Vector2(-5,0); 
            transform.localScale = new Vector2(-1,1); //Sprite schaut nach links
            anim.SetBool("running", true);
        }
        else{
            anim.SetBool("running", false); 
        }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.linearVelocity= new Vector2(rb.linearVelocity.x, 10f);
        }
    }
}
