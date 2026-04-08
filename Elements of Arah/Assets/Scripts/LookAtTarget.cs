using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{

    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GameObject found = GameObject.Find("Warrior Idle/CaveTroll_Pants_low_Mesh.002/Cube");
        if (found != null)
        {
            target = found.transform;
        }
        else
        {
            found = GameObject.Find("Wendigo@Ninja Idle 1/LookAtTargetCubw");
            if (found != null)
            {
                target = found.transform;
            }
            else
            {
                target = null; // or handle the case where neither is found
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
