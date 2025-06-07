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
        // FuriousHit.damageonce = true;
        // werkt niet meer voor eigen ability -> nu is het enemy ability door healthplayer

        if (collision.collider.TryGetComponent<Health>(out var health))
        {
            Debug.Log("dealt " + damage + " damage");
            CallTakeDamage(health, damage, damageType);
            this.gameObject.SetActive(false);
        }
        else if (collision.collider.TryGetComponent<HealthWendigo>(out var healthWendigo))
        {
            Debug.Log("dealt " + damage + " damage");
            CallTakeDamage(healthWendigo, damage, damageType);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    void CallTakeDamage(object health, int dmg, DamageTypes dmgType)
    {
        if (health is Health h)
            h.takeDamage(dmg, dmgType);
        else if (health is HealthWendigo hw)
            hw.takeDamage(dmg, dmgType);
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
