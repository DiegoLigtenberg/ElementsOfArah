using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningShaderColor : MonoBehaviour
{
    Renderer rend;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color begincolor;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color endcolor;
    private Color actualcolor;
    public float lerpDuration;
    private float timeElapsed;
   // public HDROutputSettings
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        lerpDuration = 2.000001f;
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log(lerpDuration);
        timeElapsed += Time.deltaTime;
        float t = timeElapsed / lerpDuration;
        t =   Mathf.Cos( 30f*t);


         //lerpDuration = Mathf.Exp(lerpDuration/100);

       // lerpDuration = Mathf.Exp(lerpDuration);
        if (lerpDuration > 10)
        {
          //  lerpDuration = 1.000001f;
        
        }
        timer -= Time.time;
        if  (timer <= 0)
        {
            var rare = Random.Range(0, 10000);
            if (rare >9000) { actualcolor = begincolor; timer = rare/2500; }
            else { actualcolor = Color.Lerp(begincolor, endcolor, t); timer = 100 + rare / 15f; }

            //actualcolor = Color.Lerp(begincolor, endcolor, t);
            rend.material.SetColor("_TintColor", actualcolor);
            //timer = 150f;
        }

   
       

    }
}
