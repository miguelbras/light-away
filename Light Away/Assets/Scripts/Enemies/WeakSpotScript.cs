using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpotScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {    
        if(other.tag == "GhostPlayer"){
            transform.GetComponentInParent<EnemyBehaviourScript>().BumpPlayer();
            transform.GetComponentInParent<EnemyBehaviourScript>().Die();                        
        }            
    }

    public void ToggleWeakSpot(bool active)
    {
        GetComponentInChildren<BoxCollider2D>().enabled = active;        
    }
}
