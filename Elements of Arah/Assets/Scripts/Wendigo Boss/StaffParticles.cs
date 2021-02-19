using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffParticles : MonoBehaviour
{
    public ParticleSystem particlelauncher;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            particlelauncher.enableEmission = true;
        }
        else
        {
            particlelauncher.enableEmission = false;
        }
    }
}
