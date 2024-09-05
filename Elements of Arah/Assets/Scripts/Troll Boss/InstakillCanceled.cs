using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstakillCanceled : MonoBehaviour
{
    private Rigidbody rb;
    private int x = -20;
    private float y = -1;
    private Transform startpos;

    float lastStep, timeBetweenSteps = .5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Debug.Log(rb);

        startpos = this.transform;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastStep > timeBetweenSteps)
        {

            lastStep = Time.time;
            x = x - 25 - (int)y;
            y = 1.5f * -2;

        }


        if (P2_Troll_EnterP2WalkMiddle.dodgedIntakill)
        {

            //  Debug.Log("adding force");
            rb.AddForce(0, -20f + x, 0);
        }

        if (P3_Troll_EnterP3WalkMiddle.dodgedIntakill)
        {
            //  Debug.Log("adding force");
            rb.AddForce(0, -20f + x, 0);
        }
    }
}
