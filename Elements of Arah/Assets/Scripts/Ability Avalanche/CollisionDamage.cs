using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private DamageTypes damageType;
    public Rigidbody rb;
    private void Update()
    {
        rb.AddForce(0, -7, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {

   
        var health = collision.collider.GetComponent<Health>();

    
        if (health != null)
        {
            Debug.Log("WOOeargreageagreT");
          
            Debug.Log("dealt " + damage + " damage");
            health.takeDamage(damage, damageType);
        }
    }
}

