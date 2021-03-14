using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFadeOut : MonoBehaviour
{
    Renderer rend;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color begincolor;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color endcolor;
    private Color actualcolor;
    public float lerpDuration;
    private float timeElapsed;
    public float delaytimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        lerpDuration = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        delaytimer -= Time.deltaTime;
        float t = timeElapsed / lerpDuration;


        if (delaytimer <= 0)
        {
            actualcolor = Color.Lerp(begincolor, endcolor, t);
        }
        else
        {
            actualcolor = begincolor;
        }

        rend.material.SetColor("_TintColor", actualcolor);
    }

}
