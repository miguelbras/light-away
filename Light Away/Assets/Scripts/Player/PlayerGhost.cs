using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : Player
{
    
    private bool isGhost = true;

    [SerializeField]
    private GameObject camera_object;
    private Camera camera_component;

    private Coroutine coroutine;

    void Start()
    {
        ground = LayerMask.GetMask("Ground");

        r2d = GetComponent<Rigidbody2D>();
        camera_component = camera_object.GetComponent<Camera>();

        coroutine = null;   
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + id);
        movement.y = Input.GetAxisRaw("Vertical" + id);

        Move();
        MoveVertical();
        if (Input.GetAxisRaw("Jump" + id) != 0 && isGrounded() && !isGhost)
            Jump();
        if (Input.GetAxisRaw("Debug Reset") != 0)
            updateBody();
        if (Input.GetAxis("Fire1_" + id) != 0)
            fireAction();

        checkIfIsInLight();
        clampMeToCamera();
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

    private void updateBody()
    {
        if (isGhost)
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        else
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        isGhost = !isGhost;
    }

    private void fireAction()
    {
        Debug.Log("Ghost pressing things");
    }

}
