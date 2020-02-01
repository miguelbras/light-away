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

    protected float speed = 200;

    [SerializeField]
    protected GameObject[] groundPoints;

    [SerializeField]
    protected string id;

    protected Rigidbody2D r2d;
    protected Vector2 movement;


    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + id);
        movement.y = Input.GetAxisRaw("Vertical" + id);

        Move();
        if (Input.GetAxisRaw("Jump" + id) != 0 && isGrounded())
            Jump();
        if (Input.GetAxisRaw("Fire1_" + id) != 0)
            fireAction();
    }

    protected bool isGrounded()
    {
        foreach (GameObject groundPoint in groundPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.transform.position, .1f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.tag != "Player")
                    return true;
            }
        }
        return false;
    }

    protected void Jump()
    {
        //Debug.Log(transform.name + " : " + Input.GetAxisRaw("Jump" + id));
        r2d.velocity = new Vector2(r2d.velocity.x, 0);
        r2d.AddForce(new Vector2(0,250));
    }

    protected void Move()
    {
        r2d.velocity = new Vector2(movement.x * speed * Time.deltaTime, r2d.velocity.y);
    }

    protected void fireAction()
    {
        Debug.Log("Testing firing");
    }
}
