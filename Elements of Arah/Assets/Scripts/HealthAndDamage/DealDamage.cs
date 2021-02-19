using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;

public class DealDamage : MonoBehaviour
{
    public int damage = 1;
    

    private void OnCollisionEnter(Collision collision)
    {
        /*
        Debug.Log("we hit somethuing");
        // nu doen we als raycast raakt take dmg 
        //maar straks hebben we nodig -> oncolission hit van particle take damage! -> max dmg!
        var health = collision.gameObject.GetComponent<Health>();

        if (collision.gameObject.tag == "Enemy")
        {
            if (health != null)
            {
                Debug.Log("dealt 1 damage");
               // health.takeDamage(damage);
            }
        }
        */

    }
}
