using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField]
    string playerTeleportedTag = "LightPlayer";

    [SerializeField]
    GameObject targetPosition; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if(go.tag == playerTeleportedTag)
        {
            go.transform.position = targetPosition.transform.position;
        }
    }

}
