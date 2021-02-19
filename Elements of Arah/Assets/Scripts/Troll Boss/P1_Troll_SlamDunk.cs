using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_Troll_SlamDunk : StateMachineBehaviour
{
    Phase01AA action;
    TrollController trollmovement;
    TrollController tc;

    private bool onlyonce;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        action = animator.GetComponent<Phase01AA>();
        onlyonce = false;
        //start this ability when instantiating
        action.StartSlamAttack();

        tc = animator.GetComponent<TrollController>();
        P1_Troll_Walk.stoneattacks = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("P3SlamDunk", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (!onlyonce)
        {
            animator.SetBool("JumpAttack", false);
            animator.SetBool("Attack", true);
            P1_Troll_Walk.sortOfAbility = 1;
            tc.stopwalkingIdle = true;
            onlyonce = true;
        }

    }

}


