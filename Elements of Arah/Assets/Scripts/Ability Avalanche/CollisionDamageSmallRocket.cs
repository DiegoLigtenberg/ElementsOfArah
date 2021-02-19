using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// DIT SCRIPT IS NU VOOR ENEMY AVALANCHE ROCK P2 TROLL

public class CollisionDamageSmallRocket : MonoBehaviour
{
    [SerializeField] public int damage = 1;
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

        if (!(collision.gameObject.name.Contains("Cube")))
        {
            //werkt niet meer voor eigen ability -> nu is het enemy ability door healthplayer
            var health = collision.collider.GetComponent<Health>();

            if (health != null)
            {

                Debug.Log(collision.collider.gameObject.name);
                Debug.Log("dealt " + damage + " damage");
                health.takeDamage(damage, damageType);
                this.gameObject.SetActive(false);
            }
            else
            {
                if (collision.collider.tag != "BlockWall")
                {
                    this.gameObject.SetActive(false);
                }

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!(other.gameObject.name.Contains("Cube")))
        {
            var health = other.GetComponent<Health>();

            if (health != null)
            {
                Debug.Log("WOOeargreageagreT");

                Debug.Log("dealt " + damage + " damage");
                health.takeDamage(damage, damageType);
            }
        }

   

    }
}

