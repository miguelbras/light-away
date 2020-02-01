using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehaviourScript : EnemyBehaviourScript
{
    // Update is called once per frame
    override
    public void FixedUpdate()
    {
         if(!isDead && !isEating){
            if( DistanceToLight() < aIDistance)
            {                     
                HandleMovement();
                CheckOrientation(); 
            }
            else
            {                     
                StopMovement();
            }
        }
    }

    override
    public void HandleMovement(){   
        if(!onFocusedLight){
            if(lightPlayer.transform.position.x > transform.position.x && speed < 0 ||
                lightPlayer.transform.position.x < transform.position.x && speed > 0)
                speed = -speed;

            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if(!isDead){
            if(other.tag=="GhostPlayer"){
                Die();
            }
            else if(other.tag=="LightPlayer"){
                AttackLight();
            }
        }
    }

    
}
