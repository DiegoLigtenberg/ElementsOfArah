using CreatingCharacters.Abilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// DIT SCRIPT IS NU VOOR ENEMY AVALANCHE ROCK P2 TROLL

public class ArrowRainDamage : MonoBehaviour
{
    [SerializeField] public int damage = 1;
    [SerializeField] private DamageTypes damageType;
    public Rigidbody rb;
    [SerializeField] private BoxCollider bc;

    public static List<GameObject> uniqueTargets = new List<GameObject>();

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        bc = gameObject.GetComponent<BoxCollider>();
        bc.isTrigger = false;

        damage = DamageManager.arrowRainMarcoDMG;
    }

    private void Update()
    {

        bc.isTrigger = true;

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
                //this.gameObject.SetActive(false);
            }
            else
            {
                if (collision.collider.tag != "BlockWall")
                {
                    // this.gameObject.SetActive(false);
                }

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (!(other.gameObject.name.Contains("Cube") || other.gameObject.name.Contains("extended hitbox")))
        {
            if (other.gameObject.tag == "Enemy")
            {
                var health = other.GetComponent<Health>();

                if (health != null)
                {
                    if (!uniqueTargets.Contains(other.gameObject))
                    {
                        uniqueTargets.Add(other.gameObject);

                        if (FrictionMarco.friction_active)
                        {
                            FrictionMarco.friction_stacks += 0;
                        }
                    }





                   // Debug.Log("WOOeargreageagreT");

                    Debug.Log("dealt " + damage + " damage on " + other.gameObject.name );
                    health.takeDamage(damage, damageType);
                }
            }

        }



    }
}

