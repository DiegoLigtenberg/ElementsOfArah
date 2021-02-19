using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{

    public GameObject[] effect;
    public Transform[] effectTransform;

    private bool usingBeam = false;

    float lastStep, timeBetweenSteps = 3.5f;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //snare als je geraakt wordt  door basic attack
        //doe dit eig met switch statement!
        Debug.Log(other.tag);
        if (other.tag == "BasicAttack")
        {
            //Instantiate(effect[1], this.transform.position, effectTransform[0].rotation);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastStep > timeBetweenSteps)
        {
            lastStep = Time.time;

            Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);
        }
        rb.AddForce(.3f, 0.15f, 0f);
    }


}