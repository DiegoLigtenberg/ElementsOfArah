using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrollController : MonoBehaviour
{
    //instantiated variables
    public float lookRadius = 50f;
    Transform target;
    Transform targetPlayer;
    Transform targetMiddle;
    Transform targetNorth;
    Transform targetSouth;
    Transform targetSouthEast;
    Transform targetSouthWest;
    Transform targetNorthWest;
    public Animator anim;
    public GameObject checkrange;


    NavMeshAgent agent;

    //variables that determine behavior of boss
    [HideInInspector] public bool isFacingtarget;
    [HideInInspector] private float closetoTarget = 7f;
    public static bool OUTRANGE;


    private void InstantiateTargetPosition()
    {

        targetPlayer = GameObject.Find(ActivePlayerManager.ActivePlayerName + "/PlayerTarget_Singleton").gameObject.transform;  //used to be playermaneger.instance.player.transform

        targetMiddle = PlayerManager.instance.middle.transform;
        targetNorth = PlayerManager.instance.north.transform;
        targetSouth = PlayerManager.instance.south.transform;
        targetSouthEast = PlayerManager.instance.southeast.transform;
        targetSouthWest = PlayerManager.instance.southwest.transform;
        targetNorthWest = PlayerManager.instance.northwest.transform;

    }

    //walkPlayer, walkMiddle, walkNorth, walkSouth, walkSouthEast, walkSouthWest, walkNorthWest
    public string walkDirection;

    public bool startwalkingPlayer;
    public bool startwalkingMiddle;
    public bool startwalkingNorth;
    public bool startwalkingSouth;
    public bool startwalkingSouthEast;
    public bool startwalkingSouthWest;
    public bool startwalkingNorthWest;
    public bool stopwalkingIdle;
    public bool onMiddleFacingPlayer = false; //in midden charge je speler
    //how long the turnspeed is after walking
    [Range(0.5f, 5f)] public float keeplookinginDirection = .5f;

    public static int movespeed = 5;


    float lastStep, timeBetweenSteps = 3f;

    public static bool isaaing = false;
    public static float CooldownBetweenAttack = 4f;
    public static float distToAgent;


    private bool hasstartedcoroutine = false;
    private bool hasstartedcoroutine2 = false;

    private bool standstil;

    private bool idleswitch;
    Transform player;
    // Start is called before the first frame update
    void Awake()
    {
        InstantiateTargetPosition();
        P1_Troll_Walk.stoneattacks = 0;
        if (P1_Troll_Walk.fixbug)
        {

            P1_Troll_Walk.sortOfAbility = 1;
            P1_Troll_Walk.fixbug = false;

        }

        hasstartedcoroutine2 = false;
        isTransitioned = false;


        //we kunnen hier allerlei posities aanwijzen! en statements maken voor waar die heen moet lopen + condities geven voor hoe lang etc
        // target = targetPlayer;
        agent = GetComponent<NavMeshAgent>();
        //SetWalkDirection("walkMiddle");
        agent.stoppingDistance = 10;
        player = GameObject.Find(ActivePlayerManager.ActivePlayerName).gameObject.transform;
        IdleOnPlace();

        stopdistplayer = 10;

        AnimTriggerReset();


    }


    //resetting animator triggers
    void AnimTriggerReset()
    {
        foreach (AnimatorControllerParameter p in anim.parameters)
            if (p.type == AnimatorControllerParameterType.Trigger)
                anim.ResetTrigger(p.name);
    }



    //this couroutine happens only once
    public IEnumerator P3cooldown()
    {
        if (TrollController.isaaing && !hasstartedcoroutine)
        {
            //   if (P1_Troll_Walk.sortOfAbility == 1)
            {
                hasstartedcoroutine = true;

                //hoe lang je de tijd hebt voor auto attack   //dit getal is hoe lang attack aan staat
                yield return new WaitForSeconds(CooldownBetweenAttack);
                anim.SetBool("P3BasicAttack", false);
                anim.SetBool("P3StoneEdge", false);
                anim.SetBool("P3QBD_Fire", false);
                anim.SetBool("P3HealMinion", false);

                if (P3_TrollHome.P3_cur_Ability_Iteration == 5) //een bepaalde iteratie reset weer bij begin
                {
                    //dit werkt -> gaat daarna weer naar 1
                    P3_TrollHome.P3_cur_Ability_Iteration = 0;
                }

                TrollController.isaaing = false;
                //Debug.Log("WE PUT IT FALSE");
                P3_TrollHome.P3_cur_Ability_Iteration++;

                if (P3_TrollHome.P3_cur_Ability_Iteration == 5)
                {
                    if (anim.GetBool("P2BlockMinionSpawn"))
                    {
                        P3_TrollHome.P3_cur_Ability_Iteration = 1;
                    }

                }
                hasstartedcoroutine = false;
            }
        }
    }


    //this couroutine happens only once
    public IEnumerator P2cooldown()
    {
        if (TrollController.isaaing && !hasstartedcoroutine)
        {
            //   if (P1_Troll_Walk.sortOfAbility == 1)
            {
                hasstartedcoroutine = true;

                //hoe lang je de tijd hebt voor auto attack   //dit getal is hoe lang attack aan staat
                yield return new WaitForSeconds(CooldownBetweenAttack);
                anim.SetBool("P2BasicAttack", false);
                anim.SetBool("P2StoneInAir", false);
                anim.SetBool("P2FissureOnGround", false);
                anim.SetBool("P2Idle", false);

                if (P2_Troll_Idle.P2_cur_Ability_Iteration_ == 5)
                {
                    P2_Troll_Idle.P2_cur_Ability_Iteration_ = 0;
                }

                TrollController.isaaing = false;
                P2_Troll_Idle.P2_cur_Ability_Iteration_++;

                if (P2_Troll_Idle.P2_cur_Ability_Iteration_ == 5)
                {
                    if (anim.GetBool("P2BlockMinionSpawn"))
                    {
                        P2_Troll_Idle.P2_cur_Ability_Iteration_ = 1;
                    }

                }
                hasstartedcoroutine = false;
            }
        }
    }


    //this couroutine happens only once
    public IEnumerator cooldown()
    {
        if (TrollController.isaaing && !hasstartedcoroutine)
        {
            //   if (P1_Troll_Walk.sortOfAbility == 1)
            {
                hasstartedcoroutine = true;


                //hoe lang je de tijd hebt voor auto attack
                yield return new WaitForSeconds(CooldownBetweenAttack);
                //   anim.SetBool("Attack", false);
                // anim.SetBool("JumpAttack", false);

                TrollController.isaaing = false;

                P1_Troll_Walk.sortOfAbility++;


                hasstartedcoroutine = false;
            }


        }

    }

    public static bool isTransitioned;

    private float instakillwaittime;
    private float phasetwoextra;

    public IEnumerator TransitionPhasing()
    {
        if (!hasstartedcoroutine2)
        {
            movespeed = 5;
            hasstartedcoroutine2 = true;
            isTransitioned = false;

            if (anim.GetInteger("Phase") == 2)
            {
                phasetwoextra = 1.8f;
            }




            yield return new WaitForSeconds(7.0f + phasetwoextra); //was 5.5f



            isTransitioned = true;
            anim.SetBool("P2CantPhaseToP3", false);
            phasetwoextra = 0f;

            yield return new WaitForSeconds(0.7f);

            yield return new WaitForSeconds(0.5f);


            yield return new WaitForSeconds(5f);

            //klaarzetten voor volgende phhase
            // isTransitioned = false;
            hasstartedcoroutine2 = false;
        }
    }


    public static float stopdistplayer;

    public void SetWalkDirection(string walkdirection)
    {
        walkDirection = walkdirection;

        switch (walkDirection)
        {
            case "walkPlayer":
                agent.speed = movespeed;
                target = targetPlayer;
                agent.stoppingDistance = stopdistplayer;
                //stopwalkingIdle = false;
                startwalkingPlayer = true;
                //startwalkingMiddle = false;
                onMiddleFacingPlayer = true;
                WalkPlayer();
                break;

            case "walkMiddle":

                agent.speed = movespeed;
                target = targetMiddle;

                agent.stoppingDistance = 1;

                startwalkingPlayer = false; //stop met achter speler aanlopen 
                startwalkingMiddle = true;
                WalkMiddle();

                onMiddleFacingPlayer = false;

                break;

            case "walkNorth":
                agent.speed = movespeed;
                target = targetNorth;
                agent.stoppingDistance = 3;
                startwalkingNorth = true;
                startwalkingPlayer = false;
                startwalkingMiddle = false;
                onMiddleFacingPlayer = false;

                WalkNorth();
                break;

            case "walkSouth":
                agent.speed = movespeed;
                target = targetSouth;
                agent.stoppingDistance = 3;
                startwalkingSouth = true;
                startwalkingPlayer = false;
                startwalkingMiddle = false;
                onMiddleFacingPlayer = false;

                WalkSouth();
                break;

            case "walkSouthEast":
                agent.speed = movespeed;
                target = targetSouthEast;
                agent.stoppingDistance = 3;
                startwalkingSouthEast = true;
                startwalkingPlayer = false;
                startwalkingMiddle = false;
                onMiddleFacingPlayer = false;
                // Debug.Log(walkDirection);
                WalkSouthEast();
                break;

            case "walkSouthWest":
                agent.speed = movespeed;
                target = targetSouthWest;
                agent.stoppingDistance = 3;
                startwalkingSouthWest = true;
                startwalkingPlayer = false;
                startwalkingMiddle = false;
                onMiddleFacingPlayer = false;

                WalkSouthWest();
                break;

            case "walkNorthWest":
                agent.speed = movespeed;
                target = targetNorthWest;
                agent.stoppingDistance = 3;
                startwalkingNorthWest = true;
                startwalkingPlayer = false;
                startwalkingMiddle = false;
                onMiddleFacingPlayer = false;

                WalkNorthWest();
                break;

            default:

                Debug.Log("defaulting");
                agent.stoppingDistance = 3;
                // onMiddleFacingPlayer = false;
                //startwalkingMiddle = false;
                //startwalkingPlayer = false;
                IdleOnPlace();

                break;
        }
    }

    public static float disttoSouth;
    /// /////////////////////////////////////////////////////////////////

    private bool highonce;
    private bool lowonce;
    public IEnumerator TooHigh()
    {
        highonce = true;
        lowonce = false;
        yield return new WaitForSeconds(7f);

        anim.SetBool("P1TooHigh", true);


    }

    public IEnumerator TooLow()
    {
        lowonce = true;
        highonce = false;
        yield return new WaitForSeconds(2f);


        if (player.transform.position.y <= 67.9)
        {
            anim.SetBool("P1TooHigh", false);
        }



    }

    // Update is called once per frame
    void Update()
    {
        //target player
        player = GameObject.Find(ActivePlayerManager.ActivePlayerName).gameObject.transform;
        targetPlayer = GameObject.Find(ActivePlayerManager.ActivePlayerName + "/PlayerTarget_Singleton").gameObject.transform;  //used to be playermaneger.instance.player.transform

        //otherwise outrange
        if (!CheckRangeArea1.OutRange)
        {
            OUTRANGE = false;
            anim.SetBool("outofrange", false);
            //  agent.speed = movespeed;
            distToAgent = (new Vector3(this.transform.position.x, 0, this.transform.position.z) - new Vector3(targetPlayer.transform.position.x, 0, targetPlayer.transform.position.z)).magnitude; //  agent.remainingDistance;

            disttoSouth = (this.transform.position - targetSouth.position).magnitude;
            //Debug.Log(disttoSouth);


            if (player.transform.position.y > 67.9 && !highonce)
            {
                StartCoroutine(TooHigh());
            }

            if (player.transform.position.y <= 67.9)
            {
                //  anim.SetBool("P1TooHigh", false);

                StartCoroutine(TooLow());
            }

            //p1 start cooldown
            if (isaaing && !hasstartedcoroutine && anim.GetInteger("Phase") == 0)
            {
                StartCoroutine(cooldown());

            }

            //p2 start cooldown
            if (isaaing && !hasstartedcoroutine && anim.GetInteger("Phase") == 1)
            {
                StartCoroutine(P2cooldown());

            }
            if (isaaing && !hasstartedcoroutine && anim.GetInteger("Phase") == 2)
            {
                StartCoroutine(P3cooldown());

            }
            if (phasingToMiddle.transition_contact_TC)
            {
                movespeed = 5;
                StartCoroutine(TransitionPhasing());

            }

            if (P2_Troll_Idle.lookAtplayer)
            {

                FaceTarget();


            }


            //Debug.Log(target);
            //Debug.Log("start walking player: " + startwalkingPlayer + "\n " + "start walking middle: " + startwalkingMiddle);

            //Debug.Log("stop walking Idle: " + stopwalkingIdle);


            if (startwalkingPlayer) { SetWalkDirection("walkPlayer"); }
            if (startwalkingMiddle) { SetWalkDirection("walkMiddle"); }
            if (startwalkingNorth) { SetWalkDirection("walkNorth"); }
            if (startwalkingSouth) { SetWalkDirection("walkSouth"); }
            if (startwalkingSouthEast) { SetWalkDirection("walkSouthEast"); }
            if (startwalkingSouthWest) { SetWalkDirection("walkSouthWest"); }
            if (startwalkingNorthWest) { SetWalkDirection("walkNorthWest"); }


            if (stopwalkingIdle) { IdleOnPlace(); } //{SetWalkDirection("idle_null"); }

            if (!stopwalkingIdle)
            {
                //   agent.speed = 5;
            }



            if (onMiddleFacingPlayer) { target = targetPlayer; FaceTarget(); }
            if (hasstartedcoroutine)
            {
                WalkPlayer();

            }


            if (Input.GetKey(KeyCode.Tab))
            {
                if (anim.GetInteger("Phase") > 0)
                {
                    Debug.Log(anim.GetInteger("Phase") + " THIS IS PHASE");
                    anim.SetInteger("Phase", 0);
                    P1_Troll_Walk.fixbug = true;
                }
                else { P1_Troll_Walk.fixbug = false; }
            }

            /*
            if (Input.GetKey(KeyCode.K))
             {
                 anim.SetTrigger("P2_Enter");
             }
             */

            /*
            if (Input.GetKeyDown(KeyCode.M))
             {
                 startwalkingMiddle = true; 
             }
             if (Input.GetKeyDown(KeyCode.N))
             {
                 startwalkingNorth= true;
             }
             if (Input.GetKeyDown(KeyCode.S))
             {
                 startwalkingSouthWest = true;
             }
             if (Input.GetKeyDown(KeyCode.P))
             {
                 startwalkingPlayer= true;
             }
             if (Input.GetKeyDown(KeyCode.I))
             {
                 stopwalkingIdle = true;
             }
             if (Input.GetKeyDown(KeyCode.E))
             {
                 startwalkingSouthEast = true;
             }

            if (Input.GetKeyDown(KeyCode.E))
            {
                startwalkingSouth = true;
            }
             */
        }
        else
        {
            agent.speed = 0;
            OUTRANGE = true;

            if (!outrangeonce)
            {
                outrangeonce = true;
                anim.SetTrigger("Outrange");

            }

            anim.SetBool("outofrange", true);

        }
    }
    private bool outrangeonce;

    public void SetTargetPosition(Transform newTarget)
    {
        target = newTarget;
    }


    public void IdleOnPlace()
    {
        SetTargetPosition(this.transform);
        agent.SetDestination(this.transform.position);

        if (!phasingToMiddle.transition_contact_TC)
        {
            agent.speed = 0;

        }



        startwalkingPlayer = false;
        startwalkingMiddle = false;
        startwalkingNorth = false;
        startwalkingSouth = false;
        startwalkingSouthEast = false;
        startwalkingSouthWest = false;
        startwalkingNorthWest = false;
        stopwalkingIdle = false;
    }

    public void WalkPlayer()
    {
        if (!anim.GetBool("Phasing"))
        {
            SetTargetPosition(targetPlayer);
            agent.SetDestination(target.position);


            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= agent.stoppingDistance)
            {

                if (startwalkingMiddle)
                {
                    startwalkingPlayer = false;
                    //hasstartedcoroutine = true;
                    //  StartCoroutine(FacePlayer());
                }
            }
        }
    }

    public void WalkMiddle()
    {
        SetTargetPosition(targetMiddle);
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= agent.stoppingDistance)
        {
            onMiddleFacingPlayer = true;
            if (!hasstartedcoroutine)
            {
                Debug.Log("started");
                hasstartedcoroutine = true;
                StartCoroutine(FacePlayer());
            }

        }
    }

    public void WalkNorth()
    {

        SetTargetPosition(targetNorth);
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= agent.stoppingDistance)
        {
            StartCoroutine(FacePlayer());
        }
    }

    public void WalkSouth()
    {
        SetTargetPosition(targetSouth);
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= agent.stoppingDistance)
        {
            StartCoroutine(FacePlayer());
        }
    }

    public void WalkSouthEast()
    {
        SetTargetPosition(targetSouthEast);
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= agent.stoppingDistance)
        {
            StartCoroutine(FacePlayer());
        }
    }

    public void WalkSouthWest()
    {
        SetTargetPosition(targetSouthWest);
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= agent.stoppingDistance)
        {
            StartCoroutine(FacePlayer());
        }
    }

    public void WalkNorthWest()
    {
        SetTargetPosition(targetNorthWest);
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= agent.stoppingDistance)
        {
            StartCoroutine(FacePlayer());
        }
    }


    public IEnumerator FacePlayer()
    {
        yield return new WaitForSeconds(keeplookinginDirection); //hoe lang je blijf still staan voor je naar midden draait (hier kan je core dmg doen)
        if (!startwalkingMiddle)
        {
            WalkMiddle();
            yield return new WaitForSeconds(0.7f); //hoe lang je erover doet voor het daadwerkelijk draaien en teruglopen naar midden -> lang = verder midden lopen -> wil je niet
            startwalkingPlayer = false;
            startwalkingMiddle = false;
            startwalkingNorth = false;
            startwalkingSouth = false;
            startwalkingSouthEast = false;
            startwalkingSouthWest = false;
            startwalkingNorthWest = false;
            IdleOnPlace();
            hasstartedcoroutine = false;
            stopwalkingIdle = false;
        }

        /*
        if (startwalkingPlayer)
        {

            startwalkingMiddle = false;
            startwalkingNorth = false;
            startwalkingSouth = false;
            startwalkingSouthEast = false;
            startwalkingSouthWest = false;
            startwalkingNorthWest = false;
            onMiddleFacingPlayer = true;
            yield return new WaitForSeconds(.6f);
            hasstartedcoroutine = false;
        }

        */

        if (startwalkingMiddle)
        {


            //target player and aa on 20 range   
            agent.stoppingDistance = 20;

            Debug.Log("walking to player");


            yield return new WaitForSeconds(.6f);

            startwalkingPlayer = false;
            startwalkingMiddle = false;
            startwalkingNorth = false;
            startwalkingSouth = false;
            startwalkingSouthEast = false;
            startwalkingSouthWest = false;
            startwalkingNorthWest = false;
            if (!phasingToMiddle.transition_contact_TC)
            {
                IdleOnPlace();
            }
            //zet facetarget aan
            onMiddleFacingPlayer = true;


            hasstartedcoroutine = false;
            stopwalkingIdle = false;


            yield return null;


        }
        else
        {

            startwalkingMiddle = false;
            yield return null;
        }



    }

    //deze method kunnen we uit zetten als we zn rug moeten raken!!!
    public void FaceTarget()
    {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        transform.rotation = lookRotation;
    }






    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, lookRadius);
    }

}
