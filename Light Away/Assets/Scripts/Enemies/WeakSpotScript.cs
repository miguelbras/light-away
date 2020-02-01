using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpotScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {    
        if(other.tag == "GhostPlayer"){
            transform.GetComponentsInParent<EnemyBehaviourScript>()[0].Die();
        }            
    }
}
