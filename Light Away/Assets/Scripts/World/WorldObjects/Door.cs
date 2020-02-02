using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    BoxCollider2D boxCollider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void performAction()
    {
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
    }

    public void openDoor()
    {
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
    }
}
