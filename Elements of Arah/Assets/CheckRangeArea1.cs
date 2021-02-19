using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRangeArea1 : MonoBehaviour
{

    public static bool OutRange;
    public Animator anim;

    private int i = 7;
    private bool staying;
    private float timeleft;
    private float left;
 
    private void Awake()
    {
        i = 7;
        OutRange = false;
        staying = false;
        anim.ResetTrigger("Outrange");
        anim.SetBool("outofrange", false);
        anim.SetBool("instakilling", false);
        timeleft = 5;
        left = 5;
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (!TriggerCheatSpot.CheatSpot)
        {
            if (anim.GetBool("StartFight"))
            {
                if (other.tag == "PlayerTrigger")
                {

                    OutRange = false;
                    staying = true;
                    timeleft = left;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerTrigger" && !anim.GetBool("Phasing"))
        {
            left -= 0.5f;
        }
    }

    private void LateUpdate()
    {
       
        if (staying)
        {
            timeleft = left;
        }
        staying = false;
        
    }

    private void Update()
    {
       
        if (timeleft > 0 && anim.GetBool("StartFight"))
        {
            timeleft -= Time.deltaTime;
            
        }
        if (timeleft <=0 && anim.GetBool("StartFight"))
          { 
            OutRange = true;
        }

        if (anim.GetBool("Phasing"))
        {
            timeleft = 5;
            left = 5;
        }

    }


}


