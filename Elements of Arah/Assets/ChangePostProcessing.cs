using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class ChangePostProcessing : MonoBehaviour
{
    private Volume v;

    private Vignette vg;

    private ShadowsMidtonesHighlights s;

    public float r;
    public float g;
    public float b;
    public float dark;

    public Light directionallight;


    // Start is called before the first frame update
    void Start()
    {

        v = GetComponent<Volume>();
        v.profile.TryGet(out s);     
  
    }

// Update is called once per frame
    void Update()
    {

        if (Phase01AA.startcolorchange)
        {
            if (s.shadows.value.x > 0.3) { r -= 0.0002f; }
            else { r = 0; }
            if (s.shadows.value.z <= 1.15f) { b += 0.0002f; }
            else { b = 0; }
            s.shadows.value = s.shadows.value + (new Vector4(r, g, b, dark));
            if (s.shadows.value.w > -0.57) { dark -= 0.0002f; }
            else { dark = 0; }

            if (directionallight.intensity > 0.10f)
            {
                directionallight.intensity -= 0.002f;
            }
        }
        if (directionallight.intensity > 0.85f)
        {
            directionallight.intensity -= 0.0015f;
        }



        //Debug.Log(s.shadows.value);
    }
}
