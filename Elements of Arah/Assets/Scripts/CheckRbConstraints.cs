using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRbConstraints : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool onlyonce;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A ) || Input.GetKey(KeyCode.D))
        {
            if (!onlyonce)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.constraints = RigidbodyConstraints.FreezeRotation;

            }
    
            
        }
        else
        {
        

            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
