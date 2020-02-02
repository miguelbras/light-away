using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGem : MonoBehaviour
{
    [SerializeField]
    WorldManager worldManager;

    [SerializeField]
    PICK_UP gemType;

    [SerializeField]
    string lightPlayerTag = "LightPlayer";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        if(go.tag == lightPlayerTag)
        {
            worldManager.pickUpGem(gemType);
            Destroy(gameObject);
        }
    }
}
