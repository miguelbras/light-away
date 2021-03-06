﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyBehaviourScript : EnemyBehaviourScript
{
    // Update is called once per frame
    float distanceToLight;

    override
    public void FixedUpdate()
    {        
        if(canAct()){
            distanceToLight = DistanceToLight();
            if(distanceToLight < aIDistance)
            {   
                currentState = state.moving;                  
                HandleMovement();
                FaceLight();   
            }
            else
            {       
                currentState = state.idle;              
                StopMovement();
            }
        }
        animator.SetBool("isMoving", currentState == state.moving); 
    }

    override
    public void HandleMovement(){        
        
        // Calculates direction for straight line to light player
        float directionX = Mathf.Min((lightPlayer.transform.position.x - transform.position.x) / distanceToLight, speed);
        float directionY = Mathf.Min((lightPlayer.transform.position.y - transform.position.y) / distanceToLight, speed);
        rigidBody.velocity = new Vector2(directionX * speed, directionY * speed);             
    }

    void OnTriggerEnter2D(Collider2D other)
    {    
        if(other.tag == "GhostPlayer" && lightsActive == 0){
            Die();
        }
        else if(other.tag == "BeamLight")
        {
            if(currentState != state.dead){
                StopMovement();
                currentState = state.paralyzed;
            }
            
            lightsActive++;
        }
        else if(other.tag == "CircleLight")
        {
            lightsActive++;
        }
        else if(other.tag == "LightPlayer" && canAct()){
            AttackLight();
        }        
    }

    void OnTriggerStay2D(Collider2D other)
    {           
        if(other.tag == "GhostPlayer" && lightsActive == 0){
            Die();
        }
        else if(canAct() && other.tag == "LightPlayer"){
            AttackLight();        
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {           
        if(other.tag == "BeamLight")
        {
            lightsActive--;

            if(currentState == state.paralyzed)
            {
                currentState = state.idle;
            }                        
        }
        else if(other.tag=="CircleLight"){
            lightsActive--;            
        }
    }    
}
