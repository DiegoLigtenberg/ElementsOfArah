using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidScaleChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float c = 0.00008f;
    // Update is called once per frame
    void Update()
    {

        if (this.transform.localScale.x < 1)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + 0.002f +c, this.transform.localScale.y+0.002f +c, this.transform.localScale.z+0.0015f +c);
            c +=0.00006f;
           // Debug.Log(this.transform.localScale.x);
        }
      
    }
}
