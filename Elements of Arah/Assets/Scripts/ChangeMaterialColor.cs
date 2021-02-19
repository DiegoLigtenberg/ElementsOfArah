using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMaterialColor : MonoBehaviour
{
    //[SerializeField] private Renderer myMaterial;

    public Material[] material;
    Renderer rend;
    Health hp;

    //Image im;
    private void Start()
    {
        material[0].color = Color.red;

        hp = GetComponent<Health>();
        //im =  GetComponent<Image>();
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = material[0];
    }


    private void Update()
    {
        if (hp.currentHealth < 14 && hp.currentHealth > 1)
        {
            Debug.Log("SPAM");
            material[0].color = Color.black;

            // im.color = Color.white;
        }
        else if (hp.currentHealth > 20)
        {
         //   material[0].color = Color.red;
        }


    }




    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Map")
        {
            // im.color = Color.red;
        }



    }


}

