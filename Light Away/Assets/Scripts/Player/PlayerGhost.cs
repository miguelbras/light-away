﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : Player
{
    
    private bool isGhost = true;

    [SerializeField]
    private GameObject camera_object;
    private Camera camera_component;

    private Collider2D col;

    private Coroutine coroutine;

    private int bumpForce = 6;

    private float deghostTime = 0.1f;

    [SerializeField]
    GameObject world;

    void Start()
    {
        facingRight = true;
        r2d = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        camera_component = camera_object.GetComponent<Camera>();
        anim = GetComponent<Animator>();
        ground = LayerMask.GetMask("Ground");
        coroutine = null;
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + id);
        movement.y = Input.GetAxisRaw("Vertical" + id);
        flip(movement.x);
        Move();
        MoveVertical();
        grounded = isGrounded();
        if (Input.GetAxisRaw("Jump" + id) != 0 && grounded && !isGhost)
            Jump();
        
        handleLayers();
        clampMeToCamera();
    }

    private void handleLayers()
    {
        if (isGhost)
        {
            //If it is grounded, we want to use the ground layer "0" 
            anim.SetLayerWeight(1, 0);
        }
        else
        {
            anim.SetLayerWeight(1, 1);
        }
    }

    private void clampMeToCamera()
    {
        Vector3 lowerLeftCorner = camera_component.ViewportToWorldPoint(Vector3.zero, 0);
        Vector3 upperRightCorner = camera_component.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0));
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, lowerLeftCorner.x, upperRightCorner.x),
            Mathf.Clamp(transform.position.y, lowerLeftCorner.y, upperRightCorner.y),
            0
            ); 
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Physics2D.OverlapCircle(transform.position, .1f, ground));
    }

    private void MoveVertical()
    {
        if (isGhost)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, movement.y * speed * Time.deltaTime);
        }
    }

    private void turnIntoGhost()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        col.isTrigger = true;
        isGhost = true;
    }

    private void turnIntoHuman()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        col.isTrigger = false;
        isGhost = false;
    }


    public void bump()
    {
        r2d.velocity = new Vector2(0, 0);
        r2d.AddForce(new Vector2(0, bumpForce), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "BeamLight" || collider.gameObject.tag == "CircleLight")
        {
            if (Physics2D.OverlapCircle(transform.position, .1f, ground) == null)
                turnIntoHuman();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.gameObject.tag == "BeamLight" || collider.gameObject.tag == "CircleLight") && isGhost)
        {
            if (Physics2D.OverlapCircle(transform.position, .1f, ground) == null)
                turnIntoHuman();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "BeamLight" || collider.gameObject.tag == "CircleLight")
        {
            turnIntoGhost();
        }
    }

    private IEnumerator LeaveGhost()
    {
        yield return new WaitForSeconds(deghostTime);
        turnIntoGhost();
    }

    public bool isGhostPlayer()
    {
        return isGhost;
    }

}
