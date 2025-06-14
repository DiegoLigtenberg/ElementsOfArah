﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3_Troll_StoneEdge : StateMachineBehaviour
{

    Phase01AA action;
    TrollController tc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        {
            action = animator.GetComponent<Phase01AA>();
            tc = animator.GetComponent<TrollController>();
            //start this ability when instantiating


            action.StartP3StoneEdge();
            tc.startwalkingPlayer = true;
            tc.stopwalkingIdle = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetInteger("Phase") ==0)
        {
            tc.stopwalkingIdle = true;
        }
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
