using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPassive : MonoBehaviour
{
    public GameObject[] effect;
    public Transform[] effectTransform;
    public RFX1_TransformMotion tm;
    public BasicAttackSpin bs;

    float lastStep, timeBetweenSteps = 1f;
    // Start is called before the first frame update
    void Start()
    {

      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            bs.enabled = false;
            tm.enabled = true;
      
           // Instantiate(effect[0], effectTransform[0].position, this.transform.rotation);
        }
    }
}
