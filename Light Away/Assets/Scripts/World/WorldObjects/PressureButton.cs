using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    bool isActivated = false;

    [SerializeField]
    List<string> physicalObjectTag = new List<string>();

    [SerializeField]
    GameObject[] linkedObject;

    [SerializeField]
    List<string> objectsOnPressure = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject go = collision.gameObject;
        if(physicalObjectTag.Contains(go.tag))
        {
            if(!isActivated)
            {
                foreach (GameObject lo in linkedObject)
                {
                    //TODO: FLIP SPRITE of LO
                    lo.GetComponent<Door>().performAction();
                }
            }    
            if (!objectsOnPressure.Contains(go.name))
            {
                objectsOnPressure.Add(go.name);
            }
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if(physicalObjectTag.Contains(go.tag))
        {
            objectsOnPressure.Remove(go.name);
            if(objectsOnPressure.Count <= 0)
            {
                foreach (GameObject lo in linkedObject)
                {
                    lo.GetComponent<Door>().openDoor();
                    isActivated = false;
                }
            }
        }
    }
}
