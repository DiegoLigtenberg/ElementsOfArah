using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFX4_ReplaceModelOnCollisio_Destroy : MonoBehaviour
{
    public GameObject PhysicsObjects;
    public GameObject me;

    private bool isCollided = false;
    Transform t;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
        if (collision.collider.tag != "Portal")
        {

            Debug.Log("COLLIS32re32ION");

            if (!isCollided)
            {
                me.SetActive(false);

                /*
                isCollided = true;
                PhysicsObjects.SetActive(true);
                var mesh = GetComponent<MeshRenderer>();
                if (mesh != null)
                    mesh.enabled = false;
                var rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.detectCollisions = false;
                */
            }
        }
    }

    void OnEnable()
    {
        /*
        isCollided = false;
        PhysicsObjects.SetActive(false);
        var mesh = GetComponent<MeshRenderer>();
        if (mesh != null)
            mesh.enabled = true;
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
        */
    }
}
