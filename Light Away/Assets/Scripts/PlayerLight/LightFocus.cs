using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;
public class LightFocus : MonoBehaviour
{

    [SerializeField]
    private Transform marker;

    [SerializeField]
    private GameObject circleLightBig;
    [SerializeField]
    private GameObject circleLightSmall;

    private bool beamActive;

    private Light2D light;
    private PolygonCollider2D poly;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
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

            circleLightBig.GetComponent<Light2D>().enabled = false;
            circleLightBig.GetComponent<CircleCollider2D>().enabled = false;
            circleLightSmall.GetComponent<Light2D>().enabled = true;
            circleLightSmall.GetComponent<CircleCollider2D>().enabled = true;


            var angle = Mathf.Atan2(marker.localPosition.y, marker.localPosition.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            beamActive = true;
        }
        else
        {

            light.enabled = false;
            poly.enabled = false;
            beamActive = false;

            circleLightBig.GetComponent<Light2D>().enabled = true;
            circleLightBig.GetComponent<CircleCollider2D>().enabled = true;
            circleLightSmall.GetComponent<Light2D>().enabled = false;
            circleLightSmall.GetComponent<CircleCollider2D>().enabled = false;
        }
        
    }

    public bool BeamActive
    {
        get
        {
            return BeamActive;
        }
    }

}
