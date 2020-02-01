using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyBehaviourScript : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigidBody; 

    protected SpriteRenderer spriteRend;    

    [SerializeField]
    protected GameObject lightPlayer;

    [SerializeField]
    protected GameObject ghostPlayer;

    protected float aIDistance = 2.5f;

    // base speed of the enemy
    protected float speed = 1;
    // indication of the direction of the sprite
    protected bool isFacingLeft;
    // indication if is on base light
    protected bool onLight;
    // indication if is on focused light
    protected bool onFocusedLight;
    // indication if enemy is eating a piece of light
    protected bool isEating;
    // indication if enemy is dead or dying
    protected bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isFacingLeft = false;
        onLight = false;
        onFocusedLight = false;
        isDead = false;
        isEating = false;

        //animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        
        // Disable groundcollider to interact with player
        if(GetComponentInChildren<BoxCollider2D>()){
            Physics2D.IgnoreCollision(lightPlayer.GetComponent<Collider2D>(), GetComponentInChildren<BoxCollider2D>());
            //Physics2D.IgnoreCollision(ghostPlayer.GetComponent<Collider2D>(), GetComponentInChildren<Collider2D>());
        }
    }

    // Update is called once per frame
    abstract public void FixedUpdate();
    abstract public void HandleMovement();

    // Change the sprite to match the direction it is moving
    public void CheckOrientation()
    {
        if ((isFacingLeft && speed > 0) || (!isFacingLeft && speed < 0))
        {            
            isFacingLeft = !isFacingLeft;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }        
    }

    // Change the sprite to face the light player
    public void FaceLight()
    {
        float difference=lightPlayer.transform.position.x-transform.position.x;
        if((difference>0 && isFacingLeft) || (difference<0 && !isFacingLeft)){
            isFacingLeft = !isFacingLeft;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale=scale;
        }
    }

    // Calculate the distance to the light
    public float DistanceToLight(){
       return Vector2.Distance (lightPlayer.transform.position, transform.position);
    }

    // Stop movement of the enemy
    public void StopMovement(){
        rigidBody.velocity = new Vector2(0, 0);
    }

    // Do attack on the light player
    public void AttackLight(){
        // stop movement
        StopMovement();        
        StartCoroutine("Eating");         
    }

    // Kill enemy
    public void Die(){  
        isDead = true;
        speed = 0;        
        Destroy(GetComponent<Collider2D>());
        //Destroy(GetComponentInChildren<Collider2D>());
        StopMovement();
        StartCoroutine("Fade"); 
    }

    // Routine called to fade out enemy
    IEnumerator Fade() 
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f) 
        {
            Color c = spriteRend.color;
            c.a = f;
            spriteRend.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        
        Destroy(gameObject);
    }

    // Routine called to do eating animation
    IEnumerator Eating() 
    {
        isEating = true;
        yield return new WaitForSeconds(2);        
        isEating = false;
    }
}
