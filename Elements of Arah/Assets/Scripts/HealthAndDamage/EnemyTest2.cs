using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest2 : MonoBehaviour
{

    public GameObject[] effect;
    public Transform[] effectTransform;

    private bool usingBeam = false;

    float lastStep, timeBetweenSteps = 3.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastStep > timeBetweenSteps)
        {
            lastStep = Time.time;

            Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);
        }
    }
}
