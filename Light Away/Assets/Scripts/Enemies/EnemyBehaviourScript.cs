using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyBehaviourScript : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigidBody; 
    protected Collider2D selfCollider;
    protected Collider2D groundCollider;
    protected SpriteRenderer spriteRend;    

    protected enum state {idle, moving, paralyzed, eating, dead};

    [SerializeField]
    protected GameObject lightPlayer;

    [SerializeField]
    protected GameObject ghostPlayer;

    // maximum distance to start following player
    protected float aIDistance = 5f;

    // seconds that the enemy stays dead
    protected float deadTimer = 2;

    // seconds that the enemy stays eating
    protected float eatTimer = 2;

    // seconds that the enemy stays paralyzed after the beam disappears
    protected float paralyzeTimer = 2;

    // base speed of the enemy
    protected float speed = 1;

    // indication of the direction of the sprite
    protected bool isFacingLeft;
    
    // amount of light focus on the enemy
    protected int lightsActive;

    // current state of the enemy
    protected state currentState;


    // Start is called before the first frame update
    void Start()
    {
        isFacingLeft = false;
        currentState = state.idle;
        lightsActive = 0;

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        selfCollider = GetComponent<Collider2D>();
        groundCollider = GetComponentInChildren<BoxCollider2D>();        

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
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
        lightPlayer.GetComponentInParent<PlayerGirl>().oof(transform.position.x);
        lightPlayer.GetComponentInParent<PlayerGirl>().takeDamage(33);
    }

    // Kill enemy
    public void Die(){
        if(currentState != state.dead){
            currentState = state.dead; 
            //Destroy(GetComponent<Collider2D>());
            //Destroy(GetComponentInChildren<Collider2D>());
            StopMovement();
            lightPlayer.GetComponentInParent<PlayerGirl>().getHealth(33);
            StartCoroutine("FadeOut"); 
        }
    }

    public void BumpPlayer(){
        if(currentState != state.dead){
            ghostPlayer.GetComponentInParent<PlayerGhost>().bump();
        }
    }

    public bool canAct(){
        return (currentState == state.idle || currentState == state.moving);
    }

    // Routine called to fade out enemy
    IEnumerator FadeOut()
    {
        // Disable groundcollider to interact with player
        if(groundCollider){
            Physics2D.IgnoreCollision(lightPlayer.GetComponent<Collider2D>(), groundCollider);
            Physics2D.IgnoreCollision(ghostPlayer.GetComponent<Collider2D>(), groundCollider);
        }

        for (float f = 1f; f >= -0.05f; f -= 0.05f) 
        {
            Color c = spriteRend.color;
            c.a = f;
            spriteRend.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        
        yield return new WaitForSeconds(deadTimer);
        StartCoroutine("FadeIn"); 
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f < 1f; f += 0.05f) 
        {
            Color c = spriteRend.color;
            c.a = f;
            spriteRend.color = c;
            yield return new WaitForSeconds(0.25f);
        }

        currentState = state.idle;

        // Activate groundcollider to interact with player
        if(groundCollider){
            Physics2D.IgnoreCollision(lightPlayer.GetComponent<Collider2D>(), groundCollider, false);
            Physics2D.IgnoreCollision(ghostPlayer.GetComponent<Collider2D>(), groundCollider, false);
        }
    }

    // Routine called to do eating animation
    IEnumerator Eating()
    {
        currentState = state.eating;
        yield return new WaitForSeconds(eatTimer); 

        if(currentState == state.eating)
            currentState = state.idle;
    }

}
