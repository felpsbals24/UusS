using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 0.5f;
    public Rigidbody2D rb;
    private Vector2 input;

    Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true; //Our sprite is facing left

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame - used for inputs and timers
    void Update()
    {
        ProcessInputs();
        Animate();
        if(input.x < 0 && !facingLeft || input.x > 0 && facingLeft)
        {
            flip();
        }
        
    }

    //Called once per Physics frame - used for physics
    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
    void ProcessInputs()
    {
        float moveX = input.x = Input.GetAxisRaw("Horizontal");
        float moveY = input.y = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveY == 0 ) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }



        input.Normalize(); //Makes our diagonal movement move the same as other movements
                           //Without normalize - diagonal movement would be faster
    }
    void Animate()
    {
        anim.SetFloat("MoveX", input.x) ;
        anim.SetFloat("Movey", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX",lastMoveDirection.x);
        anim.SetFloat("LastMovey",lastMoveDirection.y);
    }
    void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }
}