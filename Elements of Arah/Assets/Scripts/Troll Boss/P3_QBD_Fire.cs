using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3_QBD_Fire : StateMachineBehaviour
{
    Phase01AA action;
    TrollController tc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
            action = animator.GetComponent<Phase01AA>();
            tc = animator.GetComponent<TrollController>();
            //start this ability when instantiating
            
            action.StartP3QBDFire();


        tc.startwalkingPlayer = true;
        tc.stopwalkingIdle = true;

      
        P2_Troll_Idle.lookAtplayer = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        P2_Troll_Idle.lookAtplayer = true;
        tc.SetTargetPosition(tc.targetPlayer);
        tc.FaceTarget();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
