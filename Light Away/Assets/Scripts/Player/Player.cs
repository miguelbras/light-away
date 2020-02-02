using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /**
     * 2 players:
     * Keyboard
     * 1 : WASD, Space, Left Ctrl
     * 2 : Arrow keys, left ctrl, left shift
     * 
     * 1-2
     * Joystick
     *  Left joystick, Triangle, X.
     * 
     */

    protected Animator anim;

    protected float speed = 200;
    protected float jumpForce = 250;

    [SerializeField]
    protected LayerMask ground;

    [SerializeField]
    protected GameObject[] groundPoints;

    [SerializeField]
    protected string id;

    protected Rigidbody2D r2d;
    protected Vector2 movement;

    protected bool facingRight;
    protected bool grounded;

    void Start()
    {
        facingRight = true;
        ground = LayerMask.GetMask("Ground");
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + id);
        movement.y = Input.GetAxisRaw("Vertical" + id);

        flip(movement.x);

        Move();
        grounded = isGrounded();
        if (Input.GetAxisRaw("Jump" + id) != 0 && grounded)
            Jump();
        if (Input.GetAxisRaw("Fire1_" + id) != 0)
            fireAction();
    }

    protected void flip(float direction)
    {
        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            facingRight = !facingRight;

            if(id == "1")
                Debug.Log(facingRight);

            //Vector3 theScale = transform.localScale;

            //theScale.x *= -1;
            //transform.localScale = theScale;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }

    protected bool isGrounded()
    {
        foreach (GameObject groundPoint in groundPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.transform.position, .1f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject && colliders[i].tag != "BeamLight" && colliders[i].tag != "CircleLight"){
                    anim.SetBool("jumping", false);
                    return true;
                }
            }
        }
        anim.SetBool("jumping", true);
        return false;
    }

    protected void Jump()
    {
        r2d.velocity = new Vector2(r2d.velocity.x, 0);
        r2d.AddForce(new Vector2(0,jumpForce));
    }

    protected void Move()
    {
        anim.SetFloat("speed", Math.Abs(movement.x));
        r2d.velocity = new Vector2(movement.x * speed * Time.deltaTime, r2d.velocity.y);
    }

    protected void fireAction()
    {
        Debug.Log("Testing firing");
    }
}
