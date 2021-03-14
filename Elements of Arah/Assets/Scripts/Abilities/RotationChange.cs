using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChange : MonoBehaviour
{
    public Transform cameratransform;

    void Update()
    {
        this.transform.rotation = cameratransform.rotation;
    }
}
