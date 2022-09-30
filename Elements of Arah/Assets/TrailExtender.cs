using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailExtender : MonoBehaviour
{

    private TrailRenderer tr;
    public float speed;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        tr.time = -delay;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (tr.time < 2.5f)
        {
            tr.time += speed * Time.deltaTime;
         //   Debug.Log(tr.time);
        }
    
    }
}
