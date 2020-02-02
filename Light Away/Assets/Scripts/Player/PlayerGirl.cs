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
    bool beamActive = false;

    private float health;
    private float maxHealth;

    Vector3 lastCheckpoint = new Vector3(0, 0, 0);

    void Start()
    {
        facingRight = true;
        ground = LayerMask.GetMask("Ground");
        r2d = GetComponent<Rigidbody2D>();
        healthBar = obj.GetComponent<HealthBar>();
        anim = GetComponent<Animator>();
        health = 100;
        maxHealth = 100;
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + id);
        movement.y = Input.GetAxisRaw("Vertical" + id);
        
        if (!isDead)
        {
            flip(movement.x);
            Move();
            grounded = isGrounded();
            if (Input.GetAxisRaw("Jump" + id) != 0 && grounded)
                Jump();
            if (Input.GetAxisRaw("Fire1_" + id) != 0)
                fireAction();
            if (Input.GetAxis("BeamVertical1") != 0 || Input.GetAxis("BeamHorizontal1") != 0)
                beamActive = true;
            else
                beamActive = false;
        }

        if (beamActive)
        {
            takeDamage(Time.deltaTime * 2);
        }

    }

    protected void fireAction()
    {
        Debug.Log("Bruh");
        //healthBar.setSize(0.4f);
    }

    private void Death()
    {
        isDead = true;
        StartCoroutine(Restart());
    }

    public void takeDamage(float dmg)
    {
        if( (health - dmg) > 0)
        {
            health = health - dmg;
        }
        else
        {
            health = 0;
        }
        healthBar.setSize( health / 100);
        if(health <= 0)
        {
            Death();
        }
    }

    public void getHealth(float hp)
    {
        if(health + hp >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += hp;
        }
        healthBar.setSize(health / 100);
    }

    private void restoreHP()
    {
        health = maxHealth;
        healthBar.setSize(1f);
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        transform.position = lastCheckpoint;
        isDead = false;
        anim.SetBool("isDying", false);

        restoreHP();
    }

    
    public void oof(float otherX)
    {
        Vector2 diff = new Vector2(10, 15);
        diff.Normalize();
        r2d.AddForce(diff * 2, ForceMode2D.Impulse);
    }
    
}
