using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_Troll_Run : StateMachineBehaviour
{

    TrollController tc;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc = animator.GetComponent<TrollController>();
        //  animator.SetBool("JumpAttack", true);


        
     
   

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (TrollController.distToAgent < 10) { TrollController.movespeed = 5; }
        else if (TrollController.distToAgent >= 10 && TrollController.distToAgent < 25) { TrollController.movespeed = 8;}
        else {TrollController.movespeed = 10; }
        if (!animator.GetBool("Phasing"))
        {
            tc.startwalkingPlayer = true;
        }
       // animator.SetBool("JumpAttack", true);



        if (TrollController.distToAgent > 10) //16.8
        {
            animator.SetBool("isFarAway", true);
        }
        else
        {
            animator.SetBool("isFarAway", false);
        }


        if (!animator.GetBool("isFarAway"))
        {
            TrollController.stopdistplayer = 7.5f;
        }
        else
        {
            TrollController.stopdistplayer = 10f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    animator.SetBool("JumpAttack", false);
        TrollController.movespeed = 5;
   
    }

}
