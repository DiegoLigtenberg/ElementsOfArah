using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_Wendigo_BasicAttack : StateMachineBehaviour
{

    WendigoAbilities ability;
    WendigoController wc;
    //    OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ability = animator.GetComponent<WendigoAbilities>();
        wc = animator.GetComponent<WendigoController>();

        ability.startBasicAttack_P1();
        wc.FaceTarget();
    }

    //  OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wc.FaceTarget();
    }

    //   OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wc.FaceTarget();
    }


}
