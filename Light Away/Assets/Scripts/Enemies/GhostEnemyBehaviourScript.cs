using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyBehaviourScript : EnemyBehaviourScript
{
    // Update is called once per frame
    float distanceToLight;

    override
    public void FixedUpdate()
    {        
        if(!isDead && !isEating){
            distanceToLight = DistanceToLight();
            if(distanceToLight < aIDistance)
            {                     
                HandleMovement();
                FaceLight();   
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
            // Calculates direction for straight line to light player
            float directionX = Mathf.Min((lightPlayer.transform.position.x - transform.position.x) / distanceToLight, speed);
            float directionY = Mathf.Min((lightPlayer.transform.position.y - transform.position.y) / distanceToLight, speed);

            rigidBody.velocity = new Vector2(directionX * speed, directionY * speed);
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
