using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_Troll_Ranged_AutoAttack : StateMachineBehaviour
{
    Phase01AA action;
    TrollController tc;


    public static bool onlyonece;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        action = animator.GetComponent<Phase01AA>();
        
        //start this ability when instantiating
        action.StartRangedBasic_P1();
        tc = animator.GetComponent<TrollController>();
        tc.stopwalkingIdle = true;

        P1_Troll_Walk.stoneattacks++;
 

        if (phasingToMiddle.transition_contact_TC)
        {
          //  TrollController.movespeed = 5;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc.stopwalkingIdle = true;
      if (phasingToMiddle.transition_contact_TC)
        {
           // TrollController.movespeed = 5;
        }       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }

}
