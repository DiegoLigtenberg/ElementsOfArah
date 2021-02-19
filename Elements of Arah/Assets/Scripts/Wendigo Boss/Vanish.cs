using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class Vanish : MonoBehaviour
{
    public Material[] material;
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    private float startTime;
    Renderer rend;
    public int x;
    public bool repeatable = false;
    float t = 0;
    private int startvanishing = 0;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void NextMaterial()
    {      
        if (x < 1) {x++;   }
        else       {x = 0; }
    }

    // Start is called before the first frame update
    void Start()
    {
        x = 0;      
        rend.enabled = true;
        rend.sharedMaterial = material[x];
        startTime = Time.time;        

    }

  
    public void startVanishing()
    {
        StartCoroutine(Vanishing());
    }

    public IEnumerator Vanishing()
    {
        t = 0;
        startvanishing = 1;

        if (!(this.gameObject.name == "g Staff_grp_adjustable Staff_Head" || 
            this.gameObject.name == "g Staff_grp_adjustable Wrabs" ||
            this.gameObject.name == "g Staff_Jaw Staff_grp_adjustable"   ))
        {
            yield return new WaitForSeconds(1.5f);
        }
        else
        {
            yield return new WaitForSeconds(2.2f);
        }

      
        startvanishing = 2;
        yield return new WaitForSeconds(1.8f);
        NextMaterial();

    }


    // Update is called once per frame
    void Update()
    {
        //decides what the current material is.
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = material[x];
        
 
        //vanishing
        rend.materials[0].color = Color.Lerp(startColor, endColor, t);

        if (startvanishing == 1)
        {
            if (t < 2)
            {
                t += 0.01f;
            }
        }
        if (startvanishing == 2)
        {
            if (t > 0)
            {
                t -= 0.01f;
            }
        }



    }
}
