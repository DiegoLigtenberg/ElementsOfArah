using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phasingToMiddle : StateMachineBehaviour
{
    TrollController tc;
    public static bool transition_contact_TC;

    float lastStep, timeBetweenSteps = .01f;
    float lastStepw, timeBetweenStepsw = 5f; //hoe lang die heeft om naar midden te lopen

    private bool stopmiddle;

    public static bool SetNewMaxHp;
    public static int Phasecount;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc = animator.GetComponent<TrollController>();
        tc.startwalkingMiddle = true;

        Phase01AA.onlyonceInstaKill = false;

        transition_contact_TC = true;
        tc.TransitionPhasing();

        animator.GetComponent<Health>().isinvulnerable = true;

        //remove all attack triggers
        animator.SetBool("isRangedAttacking", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isTransitioned", false);
        animator.SetBool("Phasing", true);

      //  animator.ResetTrigger("Attack");
       // animator.ResetTrigger("JumpAttack");
        if (animator.GetInteger("Phase") == 2)
        {
            Health.phasedKillMinion = true;
        }
        SetNewMaxHp = true;
        Phasecount++;

        //reset instakils rock 
        P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
        P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
    }

    private bool idling;

    private bool hasreached;
    private int hasreachedcount = 0;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!stopmiddle)
        {
            if (Time.time - lastStep > timeBetweenSteps && !hasreached)
            {

                lastStep = Time.time;

                if (animator.GetInteger("Phase") == 1)
                {
                    tc.startwalkingMiddle = true;
                }

                if (animator.GetInteger("Phase") == 2)
                {
                    if (hasreachedcount == 0)
                    {
                        tc.startwalkingNorth = true;
                    }
                    if (hasreachedcount >= 1)
                    {
                        //tc.startwalkingSouth = true;
                        tc.startwalkingSouthWest = true;
                    }
                 
                    idling = true;

                }
            }

            if (Time.time - lastStepw > timeBetweenStepsw)
            {

                if (hasreachedcount == 1)
                {
                    lastStep = 1f;
                    timeBetweenSteps = 1f;
                }
                    if (hasreachedcount == 2)
                {
                    hasreached = true;
                }
             
             //   if (animator.GetInteger("Phase") == 1)
                {
                    tc.startwalkingPlayer = true;
                }




                lastStepw = Time.time;
                tc.stopwalkingIdle = true;
                hasreachedcount++;
            }
        }

        if (idling)
        {
           // tc.stopwalkingIdle = true;
        }

            if (TrollController.isTransitioned)
        {
            animator.SetBool("isTransitioned", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        stopmiddle = true;
      //  tc.stopwalkingIdle = true;
        transition_contact_TC = false;
        animator.SetBool("isTransitioned", false);

    }


}
