using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShotLocChanger : MonoBehaviour
{
    private float timer;
    private float increments = 0.01f;
    public Transform tf;
    private Vector3 startpos;
    public float offset;
    private Vector3 new_pos;
    // Start is called before the first frame update
    void Start()
    {

        startpos = tf.transform.localPosition;
        
        transform.position =  new Vector3(transform.position.x,transform.position.y- offset,transform.position.z);

        new_pos = tf.transform.localPosition;
        offset = (startpos.y - new_pos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (offset > 0)
        {
            offset -= 3.5f *Time.deltaTime;
            transform.localPosition += new Vector3(0, 3.5f* Time.deltaTime, 0);
        }

    }
}
