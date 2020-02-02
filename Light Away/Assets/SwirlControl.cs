using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwirlControl : MonoBehaviour
{

    private Animator colAnim;
    private Animator swirlAnim;

    private int state;

    private SpriteRenderer renderer;
    private Color color;

    [SerializeField]
    CapsuleCollider2D portalCollider;

    [SerializeField]
    string lightPlayerTag;

    // Start is called before the first frame update
    void Start()
    {
        Animator[] anim = GetComponentsInChildren<Animator>();
        colAnim = anim[0];
        swirlAnim = anim[1];

        state = 0;
        renderer = GetComponentsInChildren<SpriteRenderer>()[1];
        color = new Color(0, 0, 0, 1);
        renderer.color = color;

        portalCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == lightPlayerTag)
        {

            SceneManager.LoadScene(2);
        }
    }

    public void pickedRed()
    {
        color.r = 1f;
        renderer.color = color;
        nextState();
    }

    public void pickedBlue()
    {
        color.b = 1f;
        renderer.color = color;
        nextState();
    }

    public void pickedGreen()
    {
        color.g = 1f;
        renderer.color = color;
        nextState();
    }

    void nextState()
    {
        if (state == 0)
        {
            swirlAnim.SetTrigger("toA");
            state += 1;
        }
        else if (state == 1)
        {
            colAnim.SetTrigger("toB");
            state += 1;
        }
        else if (state == 2)
        {
            colAnim.SetTrigger("toC");
            state += 1;
            portalCollider.enabled = true;
        }


    }

}
