using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{

    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Warrior Idle/CaveTroll_Pants_low_Mesh.002/Cube").transform;
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
