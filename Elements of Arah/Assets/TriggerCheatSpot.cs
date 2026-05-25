using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheatSpot : MonoBehaviour
{
 
    private int i = 7;
    private bool staying;
    private float timeleft;
    private float left;
    public Animator anim;
    public static bool CheatSpot;

    private bool IsWarriorIdleBoss()
    {
        return ActiveBossManager.ActiveBossNum == 0
            && ActiveBossManager.ActiveBossGameObj != null;
    }

    private bool TryRefreshAnim()
    {
        if (!IsWarriorIdleBoss())
            return false;

        anim = ActiveBossManager.ActiveBossGameObj.GetComponent<Animator>();
        return anim != null && anim.runtimeAnimatorController != null;
    }

    private void Awake()
    {
        i = 7;
        CheatSpot = false;
        staying = false;

        timeleft = 3;
        left = 3;
        CheatSpot = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!TryRefreshAnim())
            return;

        if (anim.GetBool("StartFight"))
        {
            if (other.tag == "PlayerTrigger")
            {

                CheatSpot = true;
              
                staying = true;
                //timeleft = left;
            }
        }
    }
  
    private void OnTriggerExit(Collider other)
    {
        if (!TryRefreshAnim())
            return;

        if (other.tag == "PlayerTrigger" && !anim.GetBool("Phasing"))
        {
         
         //   left -= 0.5f;
            timeleft = left;
            staying = false;
            CheatSpot = false;
        }
    }

    
    private void LateUpdate()
    {
      
        if (!staying)
        {
         
            timeleft = left;
        }
        

    }

    private void Update()
    {
        if (!TryRefreshAnim())
        {
            CheatSpot = false;
            return;
        }

        if (timeleft > 0 && anim.GetBool("StartFight"))
        {
            timeleft -= Time.deltaTime;
           // CheatSpot = false;
        }
        if (timeleft <= 0 && anim.GetBool("StartFight") )
        {
           CheatSpot = true;
        }

        if (anim.GetBool("Phasing"))
        {
         
            timeleft = 3f;
            left = 3f;
        }

    }

}

