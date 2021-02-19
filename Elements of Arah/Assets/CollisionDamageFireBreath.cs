using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;

public class CollisionDamageFireBreath : MonoBehaviour
{


    [SerializeField] private int damage = 1;
    [SerializeField] private DamageTypes damageType;
    public Rigidbody rb;
    [SerializeField] private BoxCollider bc;



    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        bc = gameObject.GetComponent<BoxCollider>();
        bc.isTrigger = false;



        if (RFX1_TransformMotion.turnoff)
        {
            rb.AddForce(0, -30, 0);
        }
    }

    private void Update()
    {
        rb.AddForce(0, -0.7f, 0);

        if (RFX1_TransformMotion.turnoff)
        {
            bc.isTrigger = true;
            rb.AddForce(0, .85f, 0);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //FuriousHit.damageonce = true;
        //werkt niet meer voor eigen ability -> nu is het enemy ability door healthplayer
        var health = collision.collider.GetComponent<Health>();

        if (health != null)
        {

            Debug.Log("dealt " + damage + " damage");
            health.takeDamage(damage, damageType);

            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }
    /*
    private void OnTriggerEnter(Collider other)
    {
       
        if (!FuriousHit.damageonce)
        {
           
          //  checkforfirsthit = true;
            var health = other.GetComponent<Health>();

            if (health != null)
            {
                Debug.Log("WOOeargreageagreT");

                Debug.Log("dealt " + damage + " damage");
                health.takeDamage(damage, damageType);
                
            }

         
        }
     
    }
       */
}
