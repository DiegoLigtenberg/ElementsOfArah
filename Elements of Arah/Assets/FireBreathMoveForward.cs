using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathMoveForward : MonoBehaviour
{
    public Rigidbody rb;
    public Transform tf;
    private static bool once;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      


        rb.AddForce(transform.forward * 200);
    }
}
