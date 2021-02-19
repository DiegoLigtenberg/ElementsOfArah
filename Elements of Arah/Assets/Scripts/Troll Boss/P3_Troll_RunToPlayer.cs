using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3_Troll_RunToPlayer : StateMachineBehaviour
{
    Phase01AA action;
    TrollController tc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        action = animator.GetComponent<Phase01AA>();
        tc = animator.GetComponent<TrollController>();
        TrollController.movespeed = 10;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrollController.movespeed = 10;

        if (TrollController.distToAgent > 15.5) //16.8
        {
            animator.SetBool("isFarAway", true);
        }
        else
        {
            animator.SetBool("isFarAway", false);
        }

        tc.startwalkingPlayer = true;

        if (animator.GetBool("isFarAway") == false)
        {
            animator.SetBool("CloseCanDunk", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("CloseCanDunk", false);
        tc.stopwalkingIdle = true;
    }


}
