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

    [SerializeField]
    string ghostPlayerTag = "GhostPlayer";

    [SerializeField]
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //TODO: DELETE ME
        if(Input.GetKey(KeyCode.Return))
        {
            interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if(go.tag == ghostPlayerTag)
        {
            if(go.GetComponent<PlayerGhost>().isGhostPlayer())
            {
                interact();
            }
        }
    }

    public void interact()
    {
        if(!isActivated)
        {
            if(required != PICK_UP.NONE)
            {
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
            spriteRenderer.flipX = true;
            isActivated = true;
        }
    }
}
