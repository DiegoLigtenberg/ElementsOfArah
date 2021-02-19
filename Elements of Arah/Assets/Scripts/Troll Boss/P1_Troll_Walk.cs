using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class P1_Troll_Walk : StateMachineBehaviour
{
    Transform player;

    TrollController tc;

    float lastStepRA, timeBetweenStepsRA = .2f;

    float lastStep, timeBetweenSteps = 10.5f;

    public static int sortOfAbility = 1;

    //fixes bug that he only aa twice after being past first phase and dieing or restarting
    public static bool fixbug = true;
    public static int stoneattacks;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("heraklios_a_dizon@Jumping (2)").transform;
        tc = animator.GetComponent<TrollController>();
        TrollController.stopdistplayer = 10f;

        P1_Troll_Ranged_AutoAttack.onlyonece = true;
        if (!tc.startwalkingPlayer)
        {
            
            tc.startwalkingPlayer = true;
        }


        //resetting bool variables of phases
        animator.SetBool("P2CantPhaseToP3", false);

        Phase01AA.onlyonceInstaKill = false;

    }
    
    private bool stop;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       

        if (TrollController.distToAgent > 6) //16.8
        {
            animator.SetBool("isFarAway", true);
        }
        else
        {
            animator.SetBool("isFarAway", false);
        }

        //every x seconds set basic attack true => when basic attack play animation basic attaclk -> then continue walking -> if basic attacking then idle = true
        //als hoger is dan 5 kunnen we bijv andere timer zetten
        if (Time.fixedTime - lastStepRA > timeBetweenStepsRA && sortOfAbility < 5)
        {
            lastStepRA = Time.fixedTime;


            tc.stopwalkingIdle = true;
            TrollController.isaaing = true;

        }

        if ( stoneattacks <3) //sortOfAbility == 1 )
        {

            #region
            //aa ing is false dus loop naar speler
            //  if (!TrollController.isaaing)
            {
               // if (TrollController.distToAgent < 10) { TrollController.movespeed = 5; }
                //else if (TrollController.distToAgent >= 10 && TrollController.distToAgent < 25) { TrollController.movespeed = 8; }
                //else { TrollController.movespeed = 10; }
                // TrollController.CooldownBetweenAttack = 5.5f; //was 4
            //    tc.startwalkingPlayer = true;
             //   animator.SetBool("Attack", false);
            }

            //val aan voor 3 seconden tot isaaing weer false is
            // if (TrollController.isaaing)
            #endregion old mechanics with sort of ability
            {
                tc.stopwalkingIdle = true;

                TrollController.CooldownBetweenAttack = 5.5f;
                animator.SetBool("Attack", true);
                animator.SetBool("JumpAttack", false);
                
            }
        }
  
        if (stoneattacks ==3)  //sortOfAbility == 2)
        {
            animator.SetBool("Attack", false);
        //   if (!TrollController.isaaing)
        {
            if (TrollController.distToAgent < 10) { TrollController.movespeed = 5; }
            else if (TrollController.distToAgent >= 10 && TrollController.distToAgent < 25) { TrollController.movespeed = 8; }
            else { TrollController.movespeed = 10; }

                tc.startwalkingPlayer = true;

             #region old stuff
                //    animator.SetBool("JumpAttack", false);        

            }
            // if (TrollController.isaaing)
            #endregion
            {
            tc.stopwalkingIdle = true;
            // tc.startwalkingPlayer = false;
            TrollController.CooldownBetweenAttack = .3f;
            animator.SetBool("JumpAttack", true);
            //stoneattacks = 0;
            }
        }
        
        if (sortOfAbility > 2)
        {
            sortOfAbility = 1;
        }
    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (TrollController.distToAgent < 10) { TrollController.movespeed = 5; }
        else if (TrollController.distToAgent >= 10 && TrollController.distToAgent < 25) { TrollController.movespeed = 8; }
        else { TrollController.movespeed = 10; }


    }
}
