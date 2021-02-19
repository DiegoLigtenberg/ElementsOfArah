using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionIdle : StateMachineBehaviour
{
       

    TrollController tc;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc = animator.GetComponent<TrollController>();
        TrollController.movespeed = 1;
        tc.startwalkingMiddle = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //   tc.stopwalkingIdle = true;
        animator.SetBool("isRangedAttacking", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isTransitioned", false);

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("JumpAttack");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc.stopwalkingIdle = true;

    }

 

}