using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGirl : Player
{
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

    protected void fireAction()
    {
        Debug.Log("Bruh");
    }
}
