using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    [SerializeField] private bool spawned = false;

    public Rigidbody rb;


    void check()
    {
        spawned = false;
    }

    void delay()
    {
        spawned = true;
        Invoke("check", 1f);
    }
    // Update is called once per frame
    void Update()
    {


        

        if (Input.GetKey("1"))
        {
            if (spawned == false)
            {

               
                Invoke("delay", 0.03f);
                
               
               

                Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
                rb = spawnee.GetComponent<Rigidbody>();
                rb.AddForce(0, 0, 10f);

            }
        } 
       
    }
}
