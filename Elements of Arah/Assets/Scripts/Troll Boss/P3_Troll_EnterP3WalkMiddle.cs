using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3_Troll_EnterP3WalkMiddle : StateMachineBehaviour
{
    //THIS SCROPT RESETS ONLYONCE INSTAKILL IN UPDATE

    private float lastStep_1, timeBetweenSteps_1 = 4f;
    TrollController tc;
    public static bool dodgedIntakill;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc = animator.GetComponent<TrollController>();
        tc.startwalkingMiddle = true;

        dodgedIntakill = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time - lastStep_1 > timeBetweenSteps_1)
        {
            lastStep_1 = Time.time;
            tc.startwalkingMiddle = true;


    
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc.stopwalkingIdle = true;
        P1_Troll_Walk.sortOfAbility = 1;
    }


}
