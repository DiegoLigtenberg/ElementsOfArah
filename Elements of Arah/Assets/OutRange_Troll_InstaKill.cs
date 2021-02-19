using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRange_Troll_InstaKill : StateMachineBehaviour
{

    AudioManager am;




    private float lastStep_1, timeBetweenSteps_1 = 0.4f;
    private float lastStep_2, timeBetweenSteps_2 = .5f;

    private int watchonce;

    Phase01AA action;
    TrollController tc;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        action = animator.GetComponent<Phase01AA>();
        tc = animator.GetComponent<TrollController>();
      //  P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;


        tc.startwalkingMiddle = false;
        tc.startwalkingPlayer = false;
        tc.stopwalkingIdle = false;

        tc.stopwalkingIdle = true;
        am = animator.GetComponent<AudioManager>();
        am.StartPrepareToDie();
        animator.SetBool("instakilling", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (watchonce <= 1)
        {
            if (Time.time - lastStep_1 > timeBetweenSteps_1)
            {
                tc.startwalkingMiddle = false;

                lastStep_1 = Time.time;
                action.StartOutRangeInstaKill();
                watchonce++;
                tc.startwalkingPlayer = true;



            }
        }


        if (Time.time - lastStep_2 > timeBetweenSteps_2)
        {
            lastStep_2 = Time.time;
            action.StartOutRangeInstaKill();
            tc.stopwalkingIdle = true;
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      //  P2_Troll_EnterP2WalkMiddle.dodgedIntakill = true;
        tc.stopwalkingIdle = true;

    }


}
