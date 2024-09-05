using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvalancheCollisionCheckDMG : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
           // this.gameObject.SetActive(false);
        }

    }

}