using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStick : MonoBehaviour
{
    public GameObject indicator;
    private GameObject attach;
    // Start is called before the first frame update
    void Start()
    {
        min_distance = Mathf.Infinity;
    }

    float distance;
    float min_distance;
    public GameObject ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

     
        foreach (var hitCollider in hitColliders)
        {
           
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("BombAttach"))
            {         
                if (hitCollider.gameObject.transform.root.name == "ENEMIES") // make  one more specific for warrior idle and wendigo etc
                {
                    attach = hitCollider.gameObject;
                    Debug.Log(attach);
                    /*
                    if (hitCollider.gameObject.name.Contains("Spine"))
                    {
                        Debug.Log("SPINEEE");
                    }
                    distance = (hitCollider.ClosestPoint(this.transform.position) - this.transform.position).magnitude;
                    
                    if (distance < min_distance)
                    {
                        Debug.Log(distance);
                        min_distance = distance;
                        attach = hitCollider.gameObject;
                    }
                    */
                }
                
            }
            
        }
        Debug.Log(attach.gameObject.name);
        return attach;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = indicator.transform.position;

       // ExplosionDamage(this.transform.position, 0.01f);
    }
}
