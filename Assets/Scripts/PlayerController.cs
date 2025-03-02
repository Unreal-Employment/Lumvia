using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{           
    public Rigidbody2D rb;

    int xDir = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.A)){
            rb.linearVelocity = new Vector2(-5,0); 
        }
        if(Input.GetKey(KeyCode.D)){
            rb.linearVelocity = new Vector2(xDir,0); 
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            rb.linearVelocity= new Vector2(rb.linearVelocity.x, 10f);
        }
    }
}
