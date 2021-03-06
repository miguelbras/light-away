﻿using System;
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

    private bool isInjured;

    private float injuredTime = 0.5f;

    Vector3 lastCheckpoint = new Vector3(0, 0, 0);

    [SerializeField]
    GameObject spawnPoint;

    void Start()
    {
        facingRight = true;
        ground = LayerMask.GetMask("Ground");
        r2d = GetComponent<Rigidbody2D>();
        healthBar = obj.GetComponent<HealthBar>();
        anim = GetComponent<Animator>();
        health = 100;
        maxHealth = 100;
        isInjured = false;
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + id);
        movement.y = Input.GetAxisRaw("Vertical" + id);
        
        if (!isDead && !isInjured)
        {
            flip(movement.x);
            Move();
            grounded = isGrounded();
            if (Input.GetAxisRaw("Jump" + id) != 0 && grounded)
                Jump();
            if (Input.GetAxis("BeamVertical1") != 0 || Input.GetAxis("BeamHorizontal1") != 0)
                beamActive = true;
            else
                beamActive = false;
        }

        if (beamActive)
        {
            takeDamage(Time.deltaTime * 6);
        }

    }


    private void Death()
    {
        isDead = true;
        anim.SetBool("isDying", true);
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
        transform.position = spawnPoint.transform.position;
        isDead = false;
        anim.SetBool("isDying", false);

        restoreHP();
    }

    
    public void oof(Transform enemy)
    {
        /*Vector2 diff = transform.position - enemy.position;
        diff.Normalize();
        r2d.AddForce(diff * 20, ForceMode2D.Impulse);*/
        float bumpForce = 2;

        if(enemy.position.x > transform.position.x)
            bumpForce *= -1;

        r2d.velocity = new Vector2(bumpForce, 2);
        StartCoroutine("Injured"); 
        //r2d.AddForce(new Vector2(bumpForce, 1), ForceMode2D.Impulse);
    }

    IEnumerator Injured()
    {
        isInjured = true;
        yield return new WaitForSeconds(injuredTime); 

        isInjured = false;
    }
    
}
