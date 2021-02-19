using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawnLocChanger : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody rb2;
    public Rigidbody rb3;
    public Rigidbody rb4;
    public Rigidbody rb5;
    // Start is called before the first frame update
    void Start()
    {
    
        rb.AddForce(100, 10, 0);
        rb2.AddForce(100, 10, 0);
        rb3.AddForce(100,10, 0);
        rb4.AddForce(100, 10, 0);
      

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
