using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3_Troll_InstaKill : StateMachineBehaviour
{

    AudioManager am;
    

    private float lastStep_1, timeBetweenSteps_1 = 0.4f;
    private float lastStep_2, timeBetweenSteps_2 = .5f;

    private int watchonce;

    private bool oneinstakill;
    Phase01AA action;
    TrollController tc;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        action = animator.GetComponent<Phase01AA>();
        tc = animator.GetComponent<TrollController>();
        P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;


        tc.startwalkingMiddle = false;
        tc.startwalkingPlayer = false;
        tc.stopwalkingIdle = false;


        am = animator.GetComponent<AudioManager>();
        am.StartPrepareToDie();
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
                action.StartInstaKill();
                watchonce++;
                tc.startwalkingPlayer = true;

                tc.stopwalkingIdle = true;

            }
        }


        if (Time.time - lastStep_2 > timeBetweenSteps_2 && !oneinstakill )
        {
            lastStep_2 = Time.time;
            action.StartInstaKill();
            tc.stopwalkingIdle = true;
            oneinstakill = true;
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        P3_Troll_EnterP3WalkMiddle.dodgedIntakill = true;
        tc.stopwalkingIdle = true;

    }


}
