using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_Troll_Idle : StateMachineBehaviour
{

    //THIS SCROPT RESETS ONLYONCE INSTAKILL IN UPDATE
    Transform player;

    TrollController tc;

    //AttackCooldown
    private float lastStep_1, timeBetweenSteps_1 = 2f;

    private float lastStep_2, timeBetweenSteps_2 = .2f;

    public static int P2_cur_Ability_Iteration_ = 1;

    private bool firsttime;

    public static bool lookAtplayer;


    public static bool onceficure = false;




    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponent<Health>().isinvulnerable = false;

        player = GameObject.Find("heraklios_a_dizon@Jumping (2)").transform;
        tc = animator.GetComponent<TrollController>();
        GameObject.Find("checkinplayrange").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("cheatcube1").GetComponent<BoxCollider>().enabled=true;
        GameObject.Find("cheatcube2").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("cheatcube3").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("cheatcube4").GetComponent<BoxCollider>().enabled = true;

        //  Debug.Log(firsttime);

        //tc.stopwalkingIdle = true;

        if (!firsttime)
        {
            animator.ResetTrigger("P2_Enter");
            P2_cur_Ability_Iteration_ = 1;
            //  tc.startwalkingMiddle = true;
            firsttime = true;
        }

        else if (firsttime && P2_cur_Ability_Iteration_ == 1)
        {
            Debug.Log("long cd");

        }
        animator.SetBool("Phasing", false);

        onceficure = false;

        
        if (P2_cur_Ability_Iteration_ == 3)
        {

        }

        //resets instakill for next phase
        Phase01AA.onlyonceInstaKill = false;
       // Debug.Log(Phase01AA.onlyonceInstaKill);

    }



    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tc.FaceTarget();
        P2_Troll_Idle.lookAtplayer = true;
        if (animator.GetInteger("Phase") != 2)
            {
            animator.SetBool("Phasing", false);
        }
      
        // Debug.Log(P2_cur_Ability_Iteration_);


        // tc.stopwalkingIdle = true;
        //  Debug.Log(tc.stopwalkingIdle + " - middle  " + tc.startwalkingMiddle + " player " + tc.startwalkingPlayer);

        //om de 7 sec een aanval doen -> aanval duurt ~4 sec
        if (Time.time - lastStep_1 > timeBetweenSteps_1)
        {

            if (P2_cur_Ability_Iteration_ != 3 || P2_cur_Ability_Iteration_ != 4)
            {
                lastStep_1 = Time.time;
            }
            else
            {
                lastStep_1 = 0.01f * Time.time;
            }




            tc.stopwalkingIdle = true;
            TrollController.isaaing = true;


            if (P2_cur_Ability_Iteration_ == 2)
            {
                Debug.Log("ADDED @ SEC DELAY");
                lastStep_1 = lastStep_1 + 2f;
            }

        }


        if (P2_cur_Ability_Iteration_ == 1)
        {

            //aa ing is false dus loop naar speler
            if (!TrollController.isaaing)
            {




                animator.SetBool("P2BasicAttack", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {
                TrollController.CooldownBetweenAttack = 5f;
                animator.SetBool("P2BasicAttack", true);

            }
        }


        if (P2_cur_Ability_Iteration_ == 2)
        {

            //aa ing is false dus loop naar speler
            if (!TrollController.isaaing)
            {

                TrollController.CooldownBetweenAttack = 4f;

                animator.SetBool("P2StoneInAir", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {

                animator.SetBool("P2StoneInAir", true);

            }
        }

        if (P2_cur_Ability_Iteration_ == 3)
        {

            //aa ing is false dus loop naar speler
            if (!TrollController.isaaing)
            {

                TrollController.CooldownBetweenAttack = 4.7f;



            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {



            }
        }



        if (P2_cur_Ability_Iteration_ == 4)
        {

            //aa ing is false dus loop naar speler
            if (!TrollController.isaaing)
            {

                TrollController.CooldownBetweenAttack = .3f;

                animator.SetBool("P2FissureOnGround", false);

            }

            //val aan voor 3 seconden tot isaaing weer false is


            // if (Time.time - lastStep_2 > timeBetweenSteps_2)
            {

                if (TrollController.isaaing)
                {
                    if (!onceficure)
                    {
                        animator.SetBool("P2FissureOnGround", true);
                        onceficure = true;
                        // lastStep_2 = Time.time;
                    }
                    if (onceficure)
                    {
                        //  animator.SetBool("P2FissureOnGround", false);
                        //onceficure = false;
                        //  lastStep_2 = Time.time;
                    }


                }
            }
        }


        if (P2_cur_Ability_Iteration_ == 5)
        {

            //aa ing is false dus loop naar speler
            if (!TrollController.isaaing)
            {

                TrollController.CooldownBetweenAttack = 0f;



            }

            //val aan voor 3 seconden tot isaaing weer false is
            if (TrollController.isaaing)
            {

                animator.SetBool("P2Idle", true);

            }
        }


        if (P2_cur_Ability_Iteration_ > 5)
        {
            P2_cur_Ability_Iteration_ = 1;
        }










        //hecked hoever je weg bent.
        if (TrollController.distToAgent > 16.8)
        {
            animator.SetBool("isFarAway", true);
        }
        else
        {
            animator.SetBool("isFarAway", false);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
