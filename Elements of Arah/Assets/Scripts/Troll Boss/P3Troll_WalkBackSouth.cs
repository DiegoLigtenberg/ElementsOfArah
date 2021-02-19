using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3Troll_WalkBackSouth : StateMachineBehaviour
{
    Phase01AA action;
    TrollController tc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        action = animator.GetComponent<Phase01AA>();
        tc = animator.GetComponent<TrollController>();


        tc.startwalkingSouth = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
       
        if (TrollController.disttoSouth > 7) //16.8
        {
            animator.SetBool("isFarAway", true);
        }
       if (TrollController.disttoSouth > 6.5 && TrollController.disttoSouth < 7) //16.8
        {
            tc.startwalkingPlayer = true;
        }

       if (TrollController.disttoSouth <=6.5)
        {
            animator.SetBool("isFarAway", false);
        }

        tc.startwalkingPlayer = true;
        TrollController.movespeed = 7;
        if (animator.GetBool("isFarAway") == false)
        {
            animator.SetBool("CloseToSouth", true);
        }
        tc.startwalkingSouth = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc.startwalkingPlayer = true;
        animator.SetBool("CloseToSouth", false);
        animator.SetBool("P3QBD_Fire", true);
        tc.stopwalkingIdle = true;
    }
}
