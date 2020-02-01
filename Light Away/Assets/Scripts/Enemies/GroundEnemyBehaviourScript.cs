using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehaviourScript : EnemyBehaviourScript
{
    // Update is called once per frame
    override
    public void FixedUpdate()
    {        
        if(canAct()){
            if( DistanceToLight() < aIDistance)
            {       
                currentState = state.moving;              
                HandleMovement();
                CheckOrientation(); 
            }
            else if(currentState == state.moving)
            {                     
                currentState = state.idle;
                StopMovement();
            }
        }
    }

    override
    public void HandleMovement(){   
        if(lightPlayer.transform.position.x > transform.position.x && speed < 0 ||
            lightPlayer.transform.position.x < transform.position.x && speed > 0)
            speed = -speed;

        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {                
        if(other.tag == "BeamLight")
        {
            StopMovement();
            currentState = state.paralyzed;
            transform.GetComponentsInChildren<WeakSpotScript>()[0].ToggleWeakSpot(true);
            lightsActive++;
        }
        else if(other.tag == "CircleLight")
        {
            transform.GetComponentsInChildren<WeakSpotScript>()[0].ToggleWeakSpot(true);
            lightsActive++;
        }
        else if(other.tag == "LightPlayer" && canAct())
        {
            AttackLight();
        }
        
    }

    void OnTriggerStay2D(Collider2D other)
    {           
        if(other.tag == "LightPlayer" && canAct()){
            AttackLight();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {           
        if(other.tag == "BeamLight" && currentState == state.paralyzed)
        {
            currentState = state.idle;
            lightsActive--;
        }
        else if(other.tag=="CircleLight"){
            lightsActive--;            
        }

        if(lightsActive == 0)
        {
            transform.GetComponentsInChildren<WeakSpotScript>()[0].ToggleWeakSpot(false);
        }

    }
}
