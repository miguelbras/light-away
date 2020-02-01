using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;
public class ElipseRenderer : MonoBehaviour
{

    public int segments;
    public float xradius;
    public float yradius;
    LineRenderer line;

    [SerializeField]
    Light2D theLight;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }


    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * theLight.pointLightOuterRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * theLight.pointLightOuterRadius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
