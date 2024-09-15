using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_Troll_StoneInAir : StateMachineBehaviour
{
    Phase01AA action;
    TrollController tc;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        action = animator.GetComponent<Phase01AA>();
        tc = animator.GetComponent<TrollController>();

        //tc.startwalkingMiddle = true;
        //start this ability when instantiating
       
        //
        action.StartStoneFromAir();
  
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            tc.tcIsStoning = true;
            tc.SetTargetPosition(tc.currentOpenLookGap);
            tc.FaceTarget();
        }
        catch
        {
            // due to stateupdate, and currentopenlookgap being set in phase01aa, this is sometimes not set yet, therefore we catch
        }
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
