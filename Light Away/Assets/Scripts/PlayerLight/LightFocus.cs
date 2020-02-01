using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;
public class LightFocus : MonoBehaviour
{
    [SerializeField]
    private Transform marker;

    private Light2D light;
    private PolygonCollider2D poly;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        poly = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (marker.localPosition != Vector3.zero)
        {
            light.enabled = true;
            poly.enabled = true;

            var angle = Mathf.Atan2(marker.localPosition.y, marker.localPosition.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        else
        {

            light.enabled = false;
            poly.enabled = false;
        }
    }
}
