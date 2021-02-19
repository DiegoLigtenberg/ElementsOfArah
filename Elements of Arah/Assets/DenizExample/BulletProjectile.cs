using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletProjectile : MonoBehaviour
{
    public Rigidbody rb;
    public int bulletForce;
    public static int amnthits;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector3(0, 0, bulletForce), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When hitting Enemy - destroy object
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            amnthits++;
            Destroy(this.gameObject);
            if (amnthits >= 0)
            {
                Destroy(other.gameObject);

            }
        }
    }
}
