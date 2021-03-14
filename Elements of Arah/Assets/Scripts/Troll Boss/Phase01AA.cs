using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase01AA : MonoBehaviour
{
    //INSTANTIATE
    private TrollController trollMovement;
    public Animator anim;
    public GameObject[] effect;
    public Transform[] effectTransform;

    public GameObject[] effectP2;
    public Transform[] effectTransformP2;

    public GameObject[] effectP22;
    public Transform[] effectTransformP22;

    public GameObject[] effectP3;
    public Transform[] effectTransformP3;

    public GameObject[] effectMagnitude;
    public Transform[] effectTransformMagnitude;

    public GameObject[] effectTransition;
    public Transform[] effectTransformTransition;

    public GameObject[] p3ParticleFireColor;
    public GameObject[] p3ParticleLightColor;
    public GameObject[] p3LastShadowLights;

    public GameObject[] dragonhead;
    public AudioSource[] PhaseAudio;

    public GameObject lightningp3;

    public Rigidbody rb;
    public Vector3 PlayerPositionSpawnPosition;

    public static bool isaaing = false;

    [HideInInspector] public HealthPlayer hp;
    public Health hpm;
    public static int amntMinions;
    private bool minionhasspawned;

    // Start is called before the first frame update
    void Start()
    {
        trollMovement = GetComponent<TrollController>();
        ParticleSystem ps = GetComponent<ParticleSystem>();

        qbdspawned = false;

        startcolorchange = false;
        TrollPhasingLaser.isbeaming = false;
        alreadygot = new List<int>();
        alreadygot.Clear();
        dontadd = false;

        //active player specific
        hp = GameObject.Find("hp check").GetComponent<HealthPlayer>();
        effectTransformP3[0] = GameObject.Find(ActivePlayerManager.ActivePlayerName + "HitAbove StoneEdge").gameObject.transform;
    }

    /////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////
    // FUNCTIONS THAT START COROUTINE FOR ATTACK

    //starting attacks through other script
    public void StartRangedBasic_P1()
    {
        StartCoroutine(BasicAttack());
        // anim.SetBool("isRangedAttacking", false);
    }
    public void StartRangedBasic_P2()
    {
        StartCoroutine(P2BasicAttack());
        // anim.SetBool("isRangedAttacking", false);
    }
    public void StartRangedBasic_P3()
    {
        StartCoroutine(P3BasicAttack());
        // anim.SetBool("isRangedAttacking", false);
    }

    public void StartSlamAttack()
    {
        StartCoroutine(SlamAttack());
    }
    public void StartStoneFromAir()
    {
        StartCoroutine(P2StoneFromAir());
    }

    public void StartGroundFissure()
    {
        StartCoroutine(P2GroundFissure());
    }

    public void StartMinionHeal()
    {
        StartCoroutine(MinionHeal());
    }

    public void StartInstaKill()
    {

        StartCoroutine(InstaKill());
    }
    public void StartOutRangeInstaKill()
    {
        StartCoroutine(OutRangeInstaKill());
    }

    public void StartMinionPhasingDeath()
    {
        StartCoroutine(MinionPhasinhDeath());
    }

    public void StartP3EnrageEffect()
    {
        StartCoroutine(P3EnrageEffect());
    }

    public void StartP3StoneEdge()
    {
        StartCoroutine(P3StoneEdge());
    }

    public void StartP3QBDFire()
    {
        StartCoroutine(P3QBDFire());
    }

    public void startStopPhasing()
    {
        StartCoroutine(stopPhasing());
    }

    public IEnumerator stopPhasing()
    {
        //wait 5 sec before youl need to go back after instakill
        yield return new WaitForSeconds(1);
        anim.SetBool("Phasing", false);
        anim.SetBool("instakilling", false);
    }

    //the attacks ////////////////////////////////////
    private bool waitforaa;

    public static bool onlyonceInstaKill;
    public ParticleSystem ps;

    private float lastStep_1, timeBetweenSteps_1 = .1f;



    GameObject pathfind;
    public static bool startcolorchange;
    private float timerlampdelay = 1f;
    private void Update()
    {
        //  Debug.Log(Phase01AA.amntMinions);

        if (Time.time - lastStep_1 > timeBetweenSteps_1 && qbdspawned)
        {
            lastStep_1 = Time.time;
            Instantiate(effectMagnitude[2], effectTransformMagnitude[4].position, effectTransformMagnitude[4].rotation); //sound
        }


        if (minionhasspawned)
        {
            if (amntMinions < maxminions) { anim.SetBool("P2BlockMinionSpawn", false); }

            if (amntMinions >= maxminions) { anim.SetBool("P2BlockMinionSpawn", true); }
        }

        if (startcolorlerp)
        {
            for (int i = 0; i < p3ParticleFireColor.Length; i++)
            {
                ParticleSystem.MainModule settings = p3ParticleFireColor[i].GetComponent<ParticleSystem>().main;
                settings.startColor = Color.Lerp(begincolor, firecolor, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                if (i >= 0 && i <= 5)
                {
                    p3ParticleFireColor[i].SetActive(false);
                }
            }


            //last 3 lights in p3
            for (int i = 0; i < p3LastShadowLights.Length; i++)
            {

                //intensity last lights
                Light light = p3LastShadowLights[i].GetComponent<Light>();

                if (light.innerSpotAngle > 45)
                {
                    light.innerSpotAngle -= 0.2f;
                }
                if (i == 0)
                {
                    if (light.spotAngle > 60)
                    {
                        light.spotAngle -= 0.35f;
                    }
                }
                else
                {
                    if (light.spotAngle > 105)
                    {
                        light.spotAngle -= 0.35f;
                    }
                }


                //color last lights
                if (i == 0)
                {
                    light.color = Color.Lerp(begincolor, endshadowlightcolor, timeElapsed / lerpDuration);
                }
                else
                {
                    light.color = Color.Lerp(beginshadowlightcolor, endshadowlightcolor, timeElapsed / lerpDuration);
                }


            }



            if (timerlampdelay >= 0)
            {
                timerlampdelay -= Time.time;
            }
            else
            {
                for (int i = 0; i < p3ParticleLightColor.Length; i++)
                {
                    //  ParticleSystem.MainModule settings = p3ParticleLightColor[i].GetComponent<ParticleSystem>().main;
                    //settings.startColor = Color.Lerp(begincolor, firecolor, timeElapsed / lerpDuration);
                    //p3ParticleLightColor[i].GetComponent<Light>().color = Color.Lerp(beginlightcolor, firecolor, timeElapsed2 / lerpDuration);
                    //p3ParticleFireColor[i].GetComponent<Light>().enabled = false;
                    if (i >= 6 && i <= 12)
                    {

                        p3ParticleLightColor[i].gameObject.SetActive(false);
                    }
                    timeElapsed2 += Time.deltaTime;
                }
            }
        }


        /*
        if (qbdspawned)
        {


            if (ReadPathFindingPosition.distancetoPlayer != 0)
            {
                if (ReadPathFindingPosition.distancetoPlayer > 0.8f)
                {
                    if (Time.time - lastStep_1 > timeBetweenSteps_1)
                    {
                        lastStep_1 = Time.time;

                        pathfind = Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[0].rotation);

               //
                    }
                }


            }
        }
        */

    }
    public static bool qbdspawned;
    private IEnumerator P3QBDFire()
    {
        yield return new WaitForSeconds(1f);

        qbdspawned = true;
        Instantiate(effectMagnitude[1], effectTransformMagnitude[0].position, effectTransformMagnitude[0].rotation);


        Instantiate(effectMagnitude[1], effectTransformMagnitude[1].position, effectTransformMagnitude[1].rotation);

        Instantiate(effectMagnitude[1], effectTransformMagnitude[2].position, effectTransformMagnitude[2].rotation);

        Instantiate(effectMagnitude[1], effectTransformMagnitude[3].position, effectTransformMagnitude[3].rotation);

        Instantiate(effectMagnitude[1], effectTransformMagnitude[4].position, effectTransformMagnitude[4].rotation);


        Instantiate(effectMagnitude[1], effectTransformMagnitude[5].position, effectTransformMagnitude[5].rotation);

        Instantiate(effectMagnitude[1], effectTransformMagnitude[6].position, effectTransformMagnitude[6].rotation);

        Instantiate(effectMagnitude[1], effectTransformMagnitude[7].position, effectTransformMagnitude[7].rotation);

        Instantiate(effectMagnitude[1], effectTransformMagnitude[8].position, effectTransformMagnitude[8].rotation);

        yield return new WaitForSeconds(2.5f);
        qbdspawned = false;





        //  while (ReadPathFindingPosition.distancetoPlayer > 1)
        {
            // yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[0].rotation);

        }


        /*
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[1].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[2].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[3].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[4].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[5].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[6].rotation);
                                                                          
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[7].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[8].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[9].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[10].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[11].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[12].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[13].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[14].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[15].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[16].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[17].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[18].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[19].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[20].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[21].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, effectTransformMagnitude[22].rotation);
        */
        /*
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[0].position, effectTransformMagnitude[0].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[1].position, effectTransformMagnitude[1].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[2].position, effectTransformMagnitude[2].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[3].position, effectTransformMagnitude[3].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[4].position, effectTransformMagnitude[4].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[5].position, effectTransformMagnitude[5].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[6].position, effectTransformMagnitude[6].rotation);

        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[7].position, effectTransformMagnitude[7].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[8].position, effectTransformMagnitude[8].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[9].position, effectTransformMagnitude[9].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[10].position, effectTransformMagnitude[10].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[11].position, effectTransformMagnitude[11].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[12].position, effectTransformMagnitude[12].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[13].position, effectTransformMagnitude[13].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[14].position, effectTransformMagnitude[14].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[15].position, effectTransformMagnitude[15].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[16].position, effectTransformMagnitude[16].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[17].position, effectTransformMagnitude[17].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[18].position, effectTransformMagnitude[18].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[19].position, effectTransformMagnitude[19].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[20].position, effectTransformMagnitude[20].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[21].position, effectTransformMagnitude[21].rotation);
        yield return new WaitForSeconds(.1f); Instantiate(effectMagnitude[0], effectTransformMagnitude[22].position, effectTransformMagnitude[22].rotation);
        */

        yield return null;
    }


    private IEnumerator MinionPhasinhDeath()
    {
        hpm.takeDamage(1, DamageTypes.Water);
        yield return null;
    }

    public GameObject takedamagepanel;
    private bool takedmgonce;
    public IEnumerator TakedmgPanel()
    {

        takedamagepanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        takedamagepanel.SetActive(false);
        takedmgonce = false;


    }



    private IEnumerator OutRangeInstaKill()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log(onlyonceInstaKill + "  this should be false? ");
        if (!onlyonceInstaKill)
        {

            Instantiate(effectTransition[0], effectTransformTransition[0].position, effectTransformTransition[0].rotation);
            onlyonceInstaKill = true;
        }
        yield return new WaitForSeconds(1.5f);


        hp.takeDamage(1, DamageTypes.Water);
        P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;

        if (!takedmgonce)
        {
            takedmgonce = true;
            StartCoroutine(TakedmgPanel());
        }

        yield return new WaitForSeconds(0.2f);

        if (!P2_Troll_EnterP2WalkMiddle.dodgedIntakill)
        {
            hp.takeDamage(1, DamageTypes.Water);
            P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
        }
        yield return new WaitForSeconds(0.15f);

        if (!P2_Troll_EnterP2WalkMiddle.dodgedIntakill)
        {
            hp.takeDamage(1, DamageTypes.Water);
            P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
        }
        yield return new WaitForSeconds(0.1f);

        if (!P2_Troll_EnterP2WalkMiddle.dodgedIntakill)
        {
            hp.takeDamage(1, DamageTypes.Water);
            P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
        }

    }

    private IEnumerator InstaKill()
    {



        yield return new WaitForSeconds(2f);

        Debug.Log(onlyonceInstaKill + "  this should be false? ");
        if (!onlyonceInstaKill)
        {

            Instantiate(effectTransition[0], effectTransformTransition[0].position, effectTransformTransition[0].rotation);
            onlyonceInstaKill = true;
        }




        yield return new WaitForSeconds(1.5f);

        #region //transition to p2 instakill
        if (anim.GetInteger("Phase") == 1)
        {
            if (!P2_Troll_EnterP2WalkMiddle.dodgedIntakill)
            {
                hp.takeDamage(1, DamageTypes.Water);
                P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;

                if (!takedmgonce)
                {
                    takedmgonce = true;
                    StartCoroutine(TakedmgPanel());
                }

            }

            yield return new WaitForSeconds(0.2f);

            if (!P2_Troll_EnterP2WalkMiddle.dodgedIntakill)
            {
                hp.takeDamage(1, DamageTypes.Water);
                P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
            }
            yield return new WaitForSeconds(0.15f);

            if (!P2_Troll_EnterP2WalkMiddle.dodgedIntakill)
            {
                hp.takeDamage(1, DamageTypes.Water);
                P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
            }
            yield return new WaitForSeconds(0.1f);

            if (!P2_Troll_EnterP2WalkMiddle.dodgedIntakill)
            {
                hp.takeDamage(1, DamageTypes.Water);
                P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
            }
        }
        #endregion

        #region //transition to p3 instakill
        else if (anim.GetInteger("Phase") == 2)
        {
            if (!P3_Troll_EnterP3WalkMiddle.dodgedIntakill)
            {
                hp.takeDamage(1, DamageTypes.Water);
                P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
            }

            yield return new WaitForSeconds(0.2f);

            if (!P3_Troll_EnterP3WalkMiddle.dodgedIntakill)
            {
                hp.takeDamage(1, DamageTypes.Water);
                P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
            }
            yield return new WaitForSeconds(0.15f);

            if (!P3_Troll_EnterP3WalkMiddle.dodgedIntakill)
            {
                hp.takeDamage(1, DamageTypes.Water);
                P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
            }
            yield return new WaitForSeconds(0.1f);

            if (!P3_Troll_EnterP3WalkMiddle.dodgedIntakill)
            {
                hp.takeDamage(1, DamageTypes.Water);
                P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
            }
        }
        #endregion



    }

    private int maxminions;
    public static bool filledspot1 = false;
    public static bool filledspot2 = false;
    public static bool filledspot3 = false;
    public static bool filledspot4 = false;
    public static bool filledspot5 = false;
    public static bool filledspot6 = false;


    private IDictionary<int, string> minionspots;
    private IEnumerator MinionHeal()
    {
        minionhasspawned = true;

        //  anim.SetInteger("Phase",1);
        //phase 1
        if (anim.GetInteger("Phase") == 1)
        {
            maxminions = 6;
            if (amntMinions >= maxminions) { anim.SetBool("P2BlockMinionSpawn", true); }
            bool spawned = false;

            GameObject healminion1;
            GameObject healminion2;
            GameObject healminion3;
            GameObject healminion4;
            GameObject healminion5;
            GameObject healminion6;

            yield return new WaitForSeconds(1f);

            //zuidwest
            if (isoff == 1)
            {
                if (filledspot2 == false && !spawned) { healminion2 = Instantiate(effectP2[9], effectTransformP2[10].position, effectTransformP2[10].rotation); amntMinions += 1; healminion2.name = "Healing Minion2"; filledspot2 = true; spawned = true; }
                else if (filledspot3 == false && !spawned) { healminion3 = Instantiate(effectP2[9], effectTransformP2[11].position, effectTransformP2[11].rotation); amntMinions += 1; healminion3.name = "Healing Minion3"; filledspot3 = true; spawned = true; }
            }
            //zuidoost
            if (isoff == 2)
            {
                if (filledspot1 == false && !spawned)
                {
                    healminion1 = Instantiate(effectP2[9], effectTransformP2[9].position, effectTransformP2[9].rotation);
                    healminion1.name = "Healing Minion1";
                    amntMinions += 1;
                    filledspot1 = true;
                    spawned = true;
                }
                else if (filledspot6 == false && !spawned) { healminion6 = Instantiate(effectP2[9], effectTransformP2[14].position, effectTransformP2[14].rotation); amntMinions += 1; healminion6.name = "Healing Minion6"; filledspot6 = true; spawned = true; }
            }
            //noord oost
            if (isoff == 3)
            {
                if (filledspot5 == false && !spawned) { healminion5 = Instantiate(effectP2[9], effectTransformP2[13].position, effectTransformP2[13].rotation); amntMinions += 1; healminion5.name = "Healing Minion5"; filledspot5 = true; spawned = true; }
                else if (filledspot6 == false && !spawned) { healminion6 = Instantiate(effectP2[9], effectTransformP2[14].position, effectTransformP2[14].rotation); amntMinions += 1; healminion6.name = "Healing Minion6"; filledspot6 = true; spawned = true; }
            }
            //noord west
            if (isoff == 4)
            {
                if (filledspot4 == false && !spawned) { healminion4 = Instantiate(effectP2[9], effectTransformP2[12].position, effectTransformP2[12].rotation); amntMinions += 1; healminion4.name = "Healing Minion4"; filledspot4 = true; spawned = true; }
                else if (filledspot3 == false && !spawned) { healminion3 = Instantiate(effectP2[9], effectTransformP2[11].position, effectTransformP2[11].rotation); amntMinions += 1; healminion3.name = "Healing Minion3"; filledspot3 = true; spawned = true; }
            }

            /*
            else if (filledspot2 == false && !spawned) { healminion2 = Instantiate(effectP2[9], effectTransformP2[10].position, effectTransformP2[10].rotation); amntMinions += 1; healminion2.name = "Healing Minion2"; filledspot2 = true; spawned = true; }
            else if (filledspot3 == false && !spawned) { healminion3 = Instantiate(effectP2[9], effectTransformP2[11].position, effectTransformP2[11].rotation); amntMinions += 1; healminion3.name = "Healing Minion3"; filledspot3 = true; spawned = true; }
            else if (filledspot4 == false && !spawned) { healminion4 = Instantiate(effectP2[9], effectTransformP2[12].position, effectTransformP2[12].rotation); amntMinions += 1; healminion4.name = "Healing Minion4"; filledspot4 = true; spawned = true; }
            else if (filledspot5 == false && !spawned) { healminion5 = Instantiate(effectP2[9], effectTransformP2[13].position, effectTransformP2[13].rotation); amntMinions += 1; healminion5.name = "Healing Minion5"; filledspot5 = true; spawned = true; }
            else if (filledspot6 == false && !spawned) { healminion6 = Instantiate(effectP2[9], effectTransformP2[14].position, effectTransformP2[14].rotation); amntMinions += 1; healminion6.name = "Healing Minion6"; filledspot6 = true; spawned = true; }
            */

            spawned = false;
            //  if (amntMinions == 0)      { healminion1 = Instantiate(effectP2[9], effectTransformP2[9].position, effectTransformP2[9].rotation); amntMinions += 1;  healminion1.name = "Healing Minion1"; filledspot1 = true; }
            // else if (amntMinions == 1) { healminion2 = Instantiate(effectP2[9], effectTransformP2[10].position, effectTransformP2[10].rotation); amntMinions += 1; healminion2.name = "Healing Minion2"; filledspot2 = true; }
            //else if (amntMinions == 2) { healminion3 = Instantiate(effectP2[9], effectTransformP2[11].position, effectTransformP2[11].rotation); amntMinions += 1; healminion3.name = "Healing Minio3"; filledspot3 = true; }
            // else if (amntMinions == 3) { healminion4 = Instantiate(effectP2[9], effectTransformP2[12].position, effectTransformP2[12].rotation); amntMinions += 1; healminion4.name = "Healing Minion4"; filledspot4 = true; }
        }

        //phase 2
        if (anim.GetInteger("Phase") == 2)
        {
            maxminions = 2;
            if (amntMinions >= maxminions) { anim.SetBool("P2BlockMinionSpawn", true); }
            bool spawned = false;

            GameObject healminion1;
            GameObject healminion2;
            GameObject healminion3;
            GameObject healminion4;
            GameObject healminion5;
            GameObject healminion6;

            yield return new WaitForSeconds(1f);
            if (filledspot1 == false && !spawned)
            {
                healminion1 = Instantiate(effectP2[9], effectTransformP2[13].position, effectTransformP2[13].rotation); //ne
                healminion1.name = "Healing Minion1";
                amntMinions += 1;
                filledspot1 = true;
                spawned = true;
            }
            else if (filledspot2 == false && !spawned) { healminion2 = Instantiate(effectP2[9], effectTransformP2[12].position, effectTransformP2[12].rotation); amntMinions += 1; healminion2.name = "Healing Minion2"; filledspot2 = true; spawned = true; }//nw
            else if (filledspot6 == false && !spawned) { healminion6 = Instantiate(effectP2[9], effectTransformP2[14].position, effectTransformP2[14].rotation); amntMinions += 1; healminion6.name = "Healing Minion6"; filledspot6 = true; spawned = true; } //e
            else if (filledspot3 == false && !spawned) { healminion3 = Instantiate(effectP2[9], effectTransformP2[11].position, effectTransformP2[11].rotation); amntMinions += 1; healminion3.name = "Healing Minion3"; filledspot3 = true; spawned = true; } //w
            else if (filledspot5 == false && !spawned) { healminion5 = Instantiate(effectP2[9], effectTransformP2[9].position, effectTransformP2[9].rotation); amntMinions += 1; healminion5.name = "Healing Minion5"; filledspot5 = true; spawned = true; } //se
            else if (filledspot4 == false && !spawned) { healminion4 = Instantiate(effectP2[9], effectTransformP2[10].position, effectTransformP2[10].rotation); amntMinions += 1; healminion4.name = "Healing Minion4"; filledspot4 = true; spawned = true; } //sw



        }
        //phase 2

        yield return null;
    }


    private IEnumerator SlamAttack()
    {
        if (!waitforaa)
        {
            trollMovement.startwalkingPlayer = true;


            yield return new WaitForSeconds(1.65f);
            Instantiate(effect[1], effectTransform[1].position + new Vector3(0, -0.00055f, 0), effectTransform[1].rotation);
            Instantiate(effect[2], effectTransform[2].position, effectTransform[2].rotation);

            trollMovement.stopwalkingIdle = true;

            yield return null;
            waitforaa = false;
        }

    }

    private IEnumerator BasicAttack()
    {
        if (!waitforaa)
        {
            //  trollMovement.startwalkingPlayer = true;

            yield return new WaitForSeconds(0.9f);
            Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);

            // Instantiate(effect[1], effectTransform[0].position, effectTransform[0].rotation);

            //Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);
            yield return new WaitForSeconds(0.4f);



            yield return null;
            waitforaa = false;
        }

    }

    private IEnumerator P2BasicAttack()
    {
        if (!waitforaa)
        {


            yield return new WaitForSeconds(0.9f);
            Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);

            // Instantiate(effect[1], effectTransform[0].position, effectTransform[0].rotation);

            //Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);
            yield return new WaitForSeconds(0.4f);



            yield return null;
            waitforaa = false;
        }

    }

    private IEnumerator P3BasicAttack()
    {
        if (!waitforaa)
        {


            yield return new WaitForSeconds(0.9f);
            Instantiate(effect[3], effectTransform[0].position, effectTransform[0].rotation);

            // Instantiate(effect[1], effectTransform[0].position, effectTransform[0].rotation);

            //Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);
            yield return new WaitForSeconds(0.4f);



            yield return null;
            waitforaa = false;
        }

    }

    private int amntstones = 0;
    private int isoff;
    //1 zuid west
    //2 zood oost
    //3 noord oost
    //4 noord west

    #region gameobject of indicator rocks and rocks

    GameObject rock1;
    GameObject rock2;
    GameObject rock3;
    GameObject rock4;
    GameObject rock5;
    GameObject rock6;

    GameObject indicatorrock1;
    GameObject indicatorrock2;
    GameObject indicatorrock3;
    GameObject indicatorrock4;

    #endregion
    private static List<int> alreadygot;

    public static bool dontadd;
    public IEnumerator delayaddingvalues()
    {
        dontadd = true;
        yield return new WaitForSeconds(10f);
        dontadd = false;


    }

    public void rollRandom()
    {
        //als waarde al in lijst zit, roll nieuwe waarde
        // if (!dontadd)
        {
            if (alreadygot.Contains(isoff))
            {
                isoff = Random.Range(1, 5);
                if (!alreadygot.Contains(isoff))
                {
                    alreadygot.Add(isoff);
                    StartCoroutine(delayaddingvalues());
                }
            }
            if (!alreadygot.Contains(isoff))
            {
                alreadygot.Add(isoff);
                StartCoroutine(delayaddingvalues());
            }

        }
    }

    private IEnumerator P2StoneFromAir()
    {
        //hij cast deze abil 3x dus moet met static   
        if (amntstones == 0)
        {
            //probeer nieuwe waarde
            isoff = Random.Range(1, 5);

            if (alreadygot.Contains(isoff))
            {
                rollRandom();
                alreadygot.Remove(alreadygot.Count - 1);
                yield break;
            }

            if (alreadygot.Count >= 4)
            {
                alreadygot.Clear();
            }
        }


        amntstones++;
        yield return new WaitForSeconds(1.1f);
        rock5 = Instantiate(effectP2[0], effectTransformP2[0].position, effectTransformP2[0].rotation);
        rock6 = Instantiate(effectP2[1], effectTransformP2[1].position, effectTransformP2[1].rotation);

        if (amntstones == 3)
        {

            yield return new WaitForSeconds(1.07f);
            if (isoff != 1) { indicatorrock1 = Instantiate(effectP2[8], effectTransformP2[2].position, effectTransformP2[8].rotation); }
            if (isoff != 2) { indicatorrock2 = Instantiate(effectP2[8], effectTransformP2[3].position, effectTransformP2[8].rotation); }
            if (isoff != 3) { indicatorrock3 = Instantiate(effectP2[8], effectTransformP2[4].position, effectTransformP2[8].rotation); }
            if (isoff != 4) { indicatorrock4 = Instantiate(effectP2[8], effectTransformP2[5].position, effectTransformP2[8].rotation); }

        }

        if (amntstones == 3)
        {

            yield return new WaitForSeconds(2.07f);

            yield return new WaitForSeconds(1);

            //1 van de vier niet
            if (isoff != 1) { rock1 = Instantiate(effectP2[2], effectTransformP2[2].position, effectTransformP2[2].rotation); }
            if (isoff != 2) { rock2 = Instantiate(effectP2[3], effectTransformP2[3].position, effectTransformP2[3].rotation); }
            if (isoff != 3) { rock3 = Instantiate(effectP2[4], effectTransformP2[4].position, effectTransformP2[4].rotation); }
            if (isoff != 4) { rock4 = Instantiate(effectP2[5], effectTransformP2[5].position, effectTransformP2[5].rotation); }

            //altijd - niet meer nodig voor nieuwe map!
            //rock5 = Instantiate(effectP2[6], effectTransformP2[6].position, effectTransformP2[6].rotation);
            //rock6 = Instantiate(effectP2[7], effectTransformP2[7].position, effectTransformP2[7].rotation);



            //resetting for next time spawning rocks
            amntstones = 0;

            yield return new WaitForSeconds(1f);
            if (anim.GetInteger("Phase") == 2)
            {
                #region //Destroying rocks
                Destroy(rock1);
                Destroy(rock2);
                Destroy(rock3);
                Destroy(rock4);
                Destroy(rock5);
                Destroy(rock6);
                Destroy(indicatorrock1);
                Destroy(indicatorrock2);
                Destroy(indicatorrock3);
                Destroy(indicatorrock4);
                #endregion
            }

            yield return new WaitForSeconds(1f);
            if (anim.GetInteger("Phase") == 2)
            {
                #region //Destroying rocks
                Destroy(rock1);
                Destroy(rock2);
                Destroy(rock3);
                Destroy(rock4);
                Destroy(rock5);
                Destroy(rock6);
                Destroy(indicatorrock1);
                Destroy(indicatorrock2);
                Destroy(indicatorrock3);
                Destroy(indicatorrock4);
                #endregion
            }

            yield return new WaitForSeconds(1f);
            if (anim.GetInteger("Phase") == 2)
            {
                #region //Destroying rocks
                Destroy(rock1);
                Destroy(rock2);
                Destroy(rock3);
                Destroy(rock4);
                Destroy(rock5);
                Destroy(rock6);
                Destroy(indicatorrock1);
                Destroy(indicatorrock2);
                Destroy(indicatorrock3);
                Destroy(indicatorrock4);
                #endregion
            }

            yield return new WaitForSeconds(1f);
            if (anim.GetInteger("Phase") == 2)
            {
                #region //Destroying rocks
                Destroy(rock1);
                Destroy(rock2);
                Destroy(rock3);
                Destroy(rock4);
                Destroy(rock5);
                Destroy(rock6);
                Destroy(indicatorrock1);
                Destroy(indicatorrock2);
                Destroy(indicatorrock3);
                Destroy(indicatorrock4);
                #endregion
            }

        }
        yield return null;
    }

    private IEnumerator P2GroundFissure()
    {

        yield return new WaitForSeconds(1.25f);
        Instantiate(effectP22[0], effectTransformP22[0].position, effectTransformP22[0].rotation);
        Instantiate(effectP22[1], effectTransformP22[1].position, effectTransformP22[1].rotation);

        yield return null;
    }


    private Vector3 p3stoneCurPos;
    private Quaternion p3stoneCurRot;
    private Quaternion p3stoneCurRotIndicator;
    private IEnumerator P3StoneEdge()
    {

        yield return new WaitForSeconds(.15f);



        yield return new WaitForSeconds(.10f);
        p3stoneCurPos = effectTransformP3[0].position;
        p3stoneCurRot = effectTransformP3[1].rotation;
        //indicator rocks

        p3stoneCurRotIndicator = effectTransformP3[2].rotation;

        Instantiate(effectP3[0], p3stoneCurPos, p3stoneCurRotIndicator);

        //actual rocks
        yield return new WaitForSeconds(2.6f);
        //p3stoneCurPos = effectTransformP3[0].position;
        //p3stoneCurRot = effectTransformP3[1].rotation;
        Instantiate(effectP3[1], p3stoneCurPos, p3stoneCurRot);

        yield return null;

    }
    public Color begincolor;
    public Color firecolor;
    private bool startcolorlerp;
    private float timeElapsed;
    private float timeElapsed2;
    public float lerpDuration = 3;
    public Color beginlightcolor;
    public Color endlightcolor;
    public Color beginshadowlightcolor;
    public Color endshadowlightcolor;


    public IEnumerator P3EnrageEffect()
    {
        PhaseAudio[0].Play();
        yield return new WaitForSeconds(0.73f);
        startcolorlerp = true;
        startcolorchange = true;
        lightningp3.SetActive(true);


        yield return new WaitForSeconds(0.12f);
        p3LastShadowLights[3].SetActive(true);
        p3LastShadowLights[4].SetActive(false);
    }

    public void StartDelayDragonShineOff()
    {
        StartCoroutine(DelayDragonShineOff());
    }

    public IEnumerator DelayDragonShineOff()
    {
        yield return new WaitForSeconds(2.5f);
        if (anim.GetInteger("Phase") == 1)
        {
            dragonhead[0].SetActive(false);
            dragonhead[2].SetActive(false); //bowl
            //dragonhead[4].SetActive(false);  //bowl sound
        }
        if (anim.GetInteger("Phase") == 2)
        {
            dragonhead[1].SetActive(false);
            dragonhead[3].SetActive(false); //bowl
            //dragonhead[4].SetActive(false); //bowl sound
        }

    }
}