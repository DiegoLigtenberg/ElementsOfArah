using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightIntensity : MonoBehaviour
{
    public Light light;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (light.innerSpotAngle < 117 && light.spotAngle > 30)
        {
            light.innerSpotAngle += 2.35f;
        }

        if (light.spotAngle < 135.7)
        {
            light.spotAngle += 2.5f;
        }
    }
}