using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float i = .999f;
    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale.x > 1)
        {
            this.transform.localScale = this.transform.localScale * i;
            i -= 0.000013f;
        }
            

    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player" || other.tag == "Map" || other.tag == "BlockWall")
        {
            i = 0.992f;
        }
        
       
    }
}
