using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYAxisParticleSunShine : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float amount;

    // Start is called before the first frame update
    void Start()
    {

        rb.maxAngularVelocity = 3f;

    }

    // Update is called once per frame
    void Update()
    {
        //  rb.maxAngularVelocity = 15f;
        
        if (rb.angularVelocity.y < 2)
        {
            rb.AddTorque(transform.right * amount, ForceMode.Acceleration);
        }
    }
    
}
