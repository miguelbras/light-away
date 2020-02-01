using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(Input.GetAxis("BeamHorizontal1"),
            Input.GetAxis("BeamVertical1"));

        transform.localPosition = new Vector3(
            movement.x, movement.y,
            0
            );
    }

}
