using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGirl : Player
{
    [SerializeField]
    private GameObject obj;
    private HealthBar healthBar;
    bool isDead = false;

    Animator anim;

    Vector3 lastCheckpoint = new Vector3(0, 0, 0);

    void Start()
    {
        ground = LayerMask.GetMask("Ground");
        r2d = GetComponent<Rigidbody2D>();
        healthBar = obj.GetComponent<HealthBar>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + id);
        movement.y = Input.GetAxisRaw("Vertical" + id);

        if (!isDead)
        {
            Move();
            if (Input.GetAxisRaw("Jump" + id) != 0 && isGrounded())
                Jump();
            if (Input.GetAxisRaw("Fire1_" + id) != 0)
                fireAction();
        }


    }

    protected void fireAction()
    {
        Debug.Log("Bruh");
        healthBar.setSize(0.4f);
    }

    private void Death()
    {
        Debug.Log("Death..");
        isDead = true;
        anim.SetBool("isDying",true);
        StartCoroutine(Restart());

    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        transform.position = lastCheckpoint;
        isDead = false;
        anim.SetBool("isDying", false);
    }
}
