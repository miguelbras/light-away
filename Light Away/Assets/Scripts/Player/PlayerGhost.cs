using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : Player
{
    
    private bool isGhost = true;

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
