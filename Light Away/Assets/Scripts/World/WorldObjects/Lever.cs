using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    PICK_UP required = PICK_UP.NONE;

    bool isActivated = false;

    [SerializeField]
    GameObject worldManager;

    [SerializeField]
    GameObject requiredBubble;

    [SerializeField]
    GameObject [] linkedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //TODO: DELETE ME
        if(Input.GetKey(KeyCode.Return))
        {
            interact();
        }
    }

    public void interact()
    {
        if(!isActivated)
        {
            if(required != PICK_UP.NONE)
            {
                Debug.Log(required);
                WorldMaanger worldManagerScript = worldManager.GetComponent<WorldMaanger>();
                if (worldManagerScript.hasRequiredObject(required))
                {
                    worldManagerScript.removeItem(required);
                }
                else
                {
                    return;
                }
            }
            foreach(GameObject lo in linkedObject)
            {
                //TODO: FLIP SPRITE of LO
                lo.GetComponent<Door>().performAction();
            }
            isActivated = true;
        }
    }

}
