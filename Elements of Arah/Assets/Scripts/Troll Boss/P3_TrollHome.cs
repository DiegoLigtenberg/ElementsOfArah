using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3_TrollHome : StateMachineBehaviour
{
    //THIS SCROPT RESETS ONLYONCE INSTAKILL IN UPDATE
    Transform player;
    TrollController tc;
    public static int P3_cur_Ability_Iteration = 1;
    public static bool lookAtplayer;

    private float lastStep_1, timeBetweenSteps_1 = 1f;
    public static bool onlyfirsttime;
    private bool firsttime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find(ActivePlayerManager.ActivePlayerName).transform;
        tc = animator.GetComponent<TrollController>();

        P2_Troll_Idle.lookAtplayer = true;

        //iteration of attacks in p3
        if (!firsttime)
        {
            animator.ResetTrigger("P3_Enter");
            animator.ResetTrigger("P2_Enter");
            // tc.startwalkingSouth = true;
            P3_cur_Ability_Iteration = 1;
            //  tc.startwalkingMiddle = true;
            firsttime = true;
        }
        animator.SetBool("Phasing", false);

        tc.stopwalkingIdle = true;


        //can get hit by hp
        animator.GetComponent<Health>().isinvulnerable = false;

        //resets instakill for next phase
        Phase01AA.onlyonceInstaKill = false;
        GameObject.Find("checkinplayrange").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("cheatcube1").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("cheatcube2").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("cheatcube3").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("cheatcube4").GetComponent<BoxCollider>().enabled = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        /*
        if (P3_cur_Ability_Iteration > 2)
        {
            P3_cur_Ability_Iteration = 0;
        }
        */
        Phase01AA.onlyonceInstaKill = false;

        // Debug.Log(P3_cur_Ability_Iteration);

        if (Time.time - lastStep_1 > timeBetweenSteps_1)
        {
            lastStep_1 = Time.time;

            tc.stopwalkingIdle = true;
            TrollController.isaaing = true;

        }

        if (P3_cur_Ability_Iteration == 1)
        {
            //  tc.startwalkingSouth = true;
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 5f;
            }

            //aa ing is false dus loop naar speler
            if (!TrollController.isaaing)
            {
                animator.SetBool("P3BasicAttack", false);

            }


            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 5.5f;
                animator.SetBool("P3BasicAttack", true);

            }

        }

        /*
        if (P3_cur_Ability_Iteration == 2)
        {

            if (!TrollController.isaaing)
            {
       

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 1.5f;
          
            }
            
        }
        */
        if (P3_cur_Ability_Iteration == 2)
        {

            if (!TrollController.isaaing)
            {
                animator.SetBool("P3QBD_Fire", false);
                TrollController.CooldownBetweenAttack = .4f;

                //   animator.SetBool("P2StoneInAir", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 1f;
                animator.SetBool("P3QBD_Fire", true);
                // animator.SetBool("P2StoneInAir", true);
            }



        }


        if (P3_cur_Ability_Iteration == 3)
        {

            if (!TrollController.isaaing)
            {
                animator.SetBool("P3QBD_Fire", false);
                TrollController.CooldownBetweenAttack = .4f;

                //   animator.SetBool("P2StoneInAir", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 1f;
                animator.SetBool("P3QBD_Fire", true);
                // animator.SetBool("P2StoneInAir", true);
            }



        }



        if (P3_cur_Ability_Iteration == 4)
        {

            if (!TrollController.isaaing)
            {
                animator.SetBool("P3QBD_Fire", false);
                TrollController.CooldownBetweenAttack = .4f;

                //   animator.SetBool("P2StoneInAir", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 1f;
                animator.SetBool("P3QBD_Fire", true);
                // animator.SetBool("P2StoneInAir", true);
            }



        }

        if (P3_cur_Ability_Iteration == 5)
        {

            if (!TrollController.isaaing)
            {


            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 2;
                animator.SetBool("P3HealMinion", true);


            }

        }

        /*
        if (P3_cur_Ability_Iteration == 7)
        {

            if (!TrollController.isaaing)
            {
                animator.SetBool("P3StoneEdge", false);
                TrollController.CooldownBetweenAttack = 0f;

                //   animator.SetBool("P2StoneInAir", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 2;
                animator.SetBool("P3StoneEdge", true);
                // animator.SetBool("P2StoneInAir", true);
            }

        }
        */




        /*
        if (P3_cur_Ability_Iteration == 4)
        {

            if (!TrollController.isaaing)
            {
                animator.SetBool("P3StoneEdge", false);
                TrollController.CooldownBetweenAttack = 0f;

                //   animator.SetBool("P2StoneInAir", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 2;
                animator.SetBool("P3StoneEdge", true);
                // animator.SetBool("P2StoneInAir", true);
            }

        }
        */


        /*
        if (P3_cur_Ability_Iteration == 5)
        {

            if (!TrollController.isaaing)
            {
           

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 2;
                animator.SetBool("P3HealMinion", true);
        
      
            }

        }
        */



        /*
        if (P3_cur_Ability_Iteration == 5)
        {

            if (!TrollController.isaaing)
            {
                animator.SetBool("P3HealMinion", false);
                TrollController.CooldownBetweenAttack = 4f;

                //   animator.SetBool("P2StoneInAir", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 1f;
                animator.SetBool("P3HealMinion", true);
                // animator.SetBool("P2StoneInAir", true);
            }



        }
        */
    }




    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("P3BasicAttack", false);
        animator.SetBool("Phasing", false);
    }

}
