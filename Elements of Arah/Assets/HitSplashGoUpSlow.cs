using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSplashGoUpSlow : MonoBehaviour
{

    float lastStep, timeBetweenSteps = 0.001f;
    public Transform tf;
    private float x;
    public float y;

    public static float transformmover;
    float lastStep2, timeBetweenSteps2 = 0.1f;

    public Billboard billboard;
    public Billboard bilboard2;

    // Start is called before the first frame update
    void Start()
    {
        x = 1;
        y = 0.001f;

        tf.localPosition = new Vector3(tf.localPosition.x , tf.localPosition.y  , tf.localPosition.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastStep > timeBetweenSteps)
        {
            if (billboard != null)
            {
                billboard.enabled = false;
            }
            if (bilboard2 != null)
            {
                bilboard2.enabled = false;
            }
          
       
            if (tf.localPosition.y < 2f)
            {
                lastStep = Time.time;
                float scaler;
                scaler = 0.015f - y;
                tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + scaler * (Mathf.Log(x)), tf.localPosition.z);    //  +(1.2f* scaler) * (Mathf.Log(x)));
                tf.localScale = new Vector3(tf.localScale.x * 0.9999f, tf.localScale.y * 0.9999f, tf.localScale.z * 0.9999f);
                x = x + 1f;
                if (scaler > 0.0001)
                {
                    y = y + 0.00025f;
                }


            }
        }


    }
}