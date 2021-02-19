using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageAvalancheSphere : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private DamageTypes damageType;
    public Rigidbody rb;
    [SerializeField] private MeshCollider mc;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        mc = gameObject.GetComponent<MeshCollider>();

        mc.isTrigger = true;


        rb.AddForce(0, -30, 0);


    }

    private void Update()
    {
        rb.AddForce(0, -.4f, 0);


        mc.isTrigger = true;



    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        //werkt niet meer voor eigen ability -> nu is het enemy ability door healthplayer
        var health = collision.collider.GetComponent<HealthPlayer>();

        if (health != null)
        {
            Debug.Log("WOOeargreageagreT");

            Debug.Log("dealt " + damage + " damage");
            health.takeDamage(damage, damageType);
            this.gameObject.SetActive(false);
        }
        else
        {
            //this.gameObject.SetActive(false);
        }

    }
    */
    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<HealthPlayer>();

        if (health != null)
        {
            Debug.Log(other.gameObject.name);

            Debug.Log("dealt " + damage + " damage");
            health.takeDamage(damage, damageType);
            Destroy(this.gameObject);
      
        }

    }
}