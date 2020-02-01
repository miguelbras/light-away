using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{ 
    public void performAction()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0f);
    }

    public void openDoor()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0f);
    }
}
