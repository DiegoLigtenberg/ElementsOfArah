using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using CreatingCharacters.Abilities;

public class Health : MonoBehaviour
{


    [SerializeField] private int startingHealth = 50;
    [SerializeField] private DamageResistances damageResistance;
    [SerializeField] HealthBar healthBar;


    [HideInInspector] public int currentHealth;
    [HideInInspector] public float currentHealthPCT = 1;
    public Animator anim;
    public Animator animboss;

    public TrollPhasingLaser tpl;
    private bool AOEcooldown;
    public bool isinvulnerable;

    public static int tookThisDmg;
    [SerializeField] private Transform hitSplashPrefab;
    public GameObject[] effect;
    public Transform[] effectTransform;
    public GameObject floatingTextPrefab;

    [HideInInspector] public bool phaselaser;
    Camera camera;
    public TextMeshProHitSplat settext;

    private float lastStep2, timeBetweenSteps2 = 0.1f;
    private float lastStep3, timeBetweenSteps3 = 0.25f;
    // private float lastStep3, timeBetweenSteps3 = 0.01f;
    public static float transformmover;

    public Transform splatspawn;

    public static bool phasedKillMinion;
    public AudioSource aus;
    //remove hp bar when boss dead
    public GameObject Canvashpboss;

    private int phaseMaxHealth;

    GameObject basicSplat;
    GameObject abilitySplat;
    GameObject healSplat;
    GameObject elementalSplat;
    public GameObject[] firetotems1;
    public GameObject[] firetotems2;
    public GameObject[] dragonhead; //also fire basekt

    public GameObject wendigo;

    private Color[] hitSplatColors;


    private void OnEnable()
    {
        currentHealth = startingHealth;

    }


    private void Awake()
    {



        camera = Camera.main;
        currentHealthPCT = 1;

        //zorgt ervoor dat je aan begin maeteen max hp hebt
        healthBar.SetMaxHealth(currentHealthPCT);

        currentHealth = startingHealth;
        phaseMaxHealth = startingHealth;

        instantiateColors();

    }

    private bool miniondeathonce;
    public IEnumerator putminiondeathoff()
    {
        miniondeathonce = true;
        yield return new WaitForSeconds(4f);
        phasedKillMinion = false;
        miniondeathonce = false;
    }

    public void instantiateColors()
    {
        hitSplatColors = new Color[4];
        hitSplatColors[0] = new Color32(231, 24, 0, 255); //orannje
        hitSplatColors[1] = new Color32(32, 162, 231, 255); //blue
        hitSplatColors[2] = Color.magenta;                 //purple       
        hitSplatColors[3] = Color.green; //green
    }

    private bool removeinvuln;
    private void Update()
    {


        //kan niet meer dan max hp van phase krijgen
        if (phasingToMiddle.SetNewMaxHp)
        {
            phaseMaxHealth = currentHealth;
            phasingToMiddle.SetNewMaxHp = false;
        }

        if (currentHealth > phaseMaxHealth)
        {
            currentHealth = phaseMaxHealth;
        }

        if (CheckRangeArea1.OutRange && this.gameObject.name.Contains("Warrior"))
        {
            isinvulnerable = true;
            removeinvuln = false;
        }
        else
        {
            if (!removeinvuln && this.gameObject.name.Contains("Warrior"))
            {
                removeinvuln = true;
                isinvulnerable = false;
            }
        }




        //minion death force

        if (phasedKillMinion)
        {
            if (!miniondeathonce)
            {
                StartCoroutine(putminiondeathoff());
            }
        }

        if (gameObject.name.Contains("Healing Minion"))
        {

            if (phasedKillMinion)
            {
                if (Time.time - lastStep3 > timeBetweenSteps3)
                {
                    lastStep3 = Time.time;

                    if (this.currentHealth > 0)
                    {
                        //maybe if they die to slow minion still heals which makes it not phase ;/ (15 hits)
                        this.takeDamage(20, DamageTypes.Water);

                    }
                    if (this.currentHealth <= 0)
                    {
                        //dit gaat automatisch uit via coroutine

                    }
                }
            }


        }



        if (movehits)
        {

            if (Time.time - lastStep2 > timeBetweenSteps2 && transformmover <= 1)
            {
                lastStep2 = Time.time;

                transformmover = transformmover + 0.01f;


            }

            if (transformmover > 1)
            {
                transformmover = 0;
            }
        }
        if (!movehits)
        {
            transformmover = 0;
        }

        if (this.gameObject.name.Contains("Warrior"))
        {

            if (basicSplat != null)
            {
                basicSplat.transform.position = splatspawn.position + new Vector3(0, 3, 0);
                basicSplat.transform.rotation = splatspawn.rotation;
            }

            if (abilitySplat != null)
            {
                abilitySplat.transform.position = splatspawn.position + new Vector3(0, 4.7f, 0);
                abilitySplat.transform.rotation = splatspawn.rotation;
            }

            if (healSplat != null)
            {
                healSplat.transform.position = splatspawn.position + new Vector3(-1, 5, 0);
                healSplat.transform.rotation = splatspawn.rotation;
            }

            if (elementalSplat != null)
            {
                elementalSplat.transform.position = splatspawn.position + new Vector3(0, 3.85f, 0);
                elementalSplat.transform.rotation = splatspawn.rotation;
            }
        }

        else if (this.gameObject.name.Contains("Healing Minion"))
        {
            if (basicSplat != null)
            {

                basicSplat.transform.position = transform.position + new Vector3(0, 2.5f, -1.5f);
                basicSplat.transform.rotation = transform.rotation;
            }
            if (abilitySplat != null)
            {
                abilitySplat.transform.position = transform.position + new Vector3(0, 3.2f, -1.5f);
                abilitySplat.transform.rotation = transform.rotation;
            }
            if (healSplat != null)
            {
                healSplat.transform.position = transform.position + new Vector3(0, 3.85f, -1.5f);
                healSplat.transform.rotation = transform.rotation;
            }
            if (elementalSplat != null)
            {
                elementalSplat.transform.position = transform.position + new Vector3(0, 3.2f, -1.5f);
                elementalSplat.transform.rotation = transform.rotation;
            }
        }
        else
        {
            if (basicSplat != null)
            {
                basicSplat.transform.position = transform.position + new Vector3(0, 1, 0);
                basicSplat.transform.rotation = transform.rotation;
            }
            if (abilitySplat != null)
            {
                abilitySplat.transform.position = transform.position + new Vector3(0, 1.7f, 0);
                abilitySplat.transform.rotation = transform.rotation;
            }
            if (healSplat != null)
            {
                healSplat.transform.position = transform.position + new Vector3(0, 2, 0);
                healSplat.transform.rotation = transform.rotation;
            }
            if (elementalSplat != null)
            {
                elementalSplat.transform.position = transform.position + new Vector3(0, 1.85f, 0);
                elementalSplat.transform.rotation = transform.rotation;
            }

        }
    }

    private bool movehits;
    private bool onlyoncehit;
    public IEnumerator moveHitSplat()
    {
        if (!onlyoncehit)
        {
            onlyoncehit = true;
            yield return new WaitForSeconds(0.3f);
            movehits = false;
            onlyoncehit = false;
        }

    }



    public void ShowFloatingTextHeal()
    {
        if (this.gameObject.name.Contains("Warrior"))
        {
            healSplat = Instantiate(floatingTextPrefab, splatspawn.position + new Vector3(0, 2, 0), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            healSplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            healSplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[3];
        }
        else
        {
            healSplat = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 2, -2f), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            healSplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            healSplat.GetComponentInChildren<TextMeshProHitSplatMinion>().colorText = hitSplatColors[3];
        }


    }

    public void ShowFloatingTextFire()
    {
        if (this.gameObject.name.Contains("Warrior"))
        {
            basicSplat = Instantiate(floatingTextPrefab, splatspawn.position + new Vector3(0, 2, 0), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            basicSplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            basicSplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[0];
        }
        else
        {
            basicSplat = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 2, 0f), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            basicSplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            try { basicSplat.GetComponentInChildren<TextMeshProHitSplatMinion>().colorText = hitSplatColors[0]; }
            catch { basicSplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[0]; }

        }
    }

    public void ShowFloatingTextWater()
    {
        if (this.gameObject.name.Contains("Warrior"))
        {
            basicSplat = Instantiate(floatingTextPrefab, splatspawn.position + new Vector3(0, 2, 0), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            basicSplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            basicSplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[1];
        }
        else
        {
            basicSplat = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 2, 0f), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            basicSplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            try { basicSplat.GetComponentInChildren<TextMeshProHitSplatMinion>().colorText = hitSplatColors[1]; }
            catch { basicSplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[1]; }
        }
    }

    public void ShowFloatingTextAOE()
    {
        if (this.gameObject.name.Contains("Warrior"))
        {
            abilitySplat = Instantiate(floatingTextPrefab, splatspawn.position + new Vector3(0, 2, 0f), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            abilitySplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            abilitySplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[1];
        }
        else
        {
            abilitySplat = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 2, 0f), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            abilitySplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            try { abilitySplat.GetComponentInChildren<TextMeshProHitSplatMinion>().colorText = hitSplatColors[1]; }
            catch { abilitySplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[1]; }
        }
    }

    public void ShowFloatingTextElemental()
    {
        if (this.gameObject.name.Contains("Warrior"))
        {
            elementalSplat = Instantiate(floatingTextPrefab, splatspawn.position + new Vector3(0, 2, 0f), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            elementalSplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            elementalSplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[2];
        }
        else
        {
            elementalSplat = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 2, 0f), new Quaternion(Quaternion.identity.w * 1, Quaternion.identity.x * 1, Quaternion.identity.y * 1, Quaternion.identity.z * 1));
            elementalSplat.GetComponentInChildren<TextMesh>().text = tookThisDmg.ToString();
            try { elementalSplat.GetComponentInChildren<TextMeshProHitSplatMinion>().colorText = hitSplatColors[2]; }
            catch { elementalSplat.GetComponentInChildren<TextMeshProHitSplat>().colorText = hitSplatColors[2]; }
        }
    }


    public IEnumerator AOECooldownReset()
    {
        yield return new WaitForSeconds(.8f);
        AOEcooldown = false;
    }

    private float phasingDmgReduction = .1f;


    public IEnumerator delayElementalSplat()
    {
        yield return new WaitForSeconds(0.15f);
        ShowFloatingTextElemental();
    }

    public void delayp2instakill()
    {

        // yield return new WaitForSeconds(.3f);
        P2_Troll_EnterP2WalkMiddle.dodgedIntakill = true;
        anim.SetTrigger("P2_Enter");
    }

    public void delayp3instakill()
    {

        // yield return new WaitForSeconds(.1f);
        P3_Troll_EnterP3WalkMiddle.dodgedIntakill = true;
        anim.SetTrigger("P3_Enter");
        anim.ResetTrigger("P2_Enter"); //this gets called somehow so we stop it here
    }


    public IEnumerator setStartFightTrue()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("StartFight", true);
    }

    private bool startfightonce;
    private int spelldmg; // ability dmg    = water/arcane
    private int aadmg; // aa dmg         = fire
    private int purpledmg; // purple dmg     = purple
    private bool phaseonce;
    private bool phasetwice;

    public void takeDamage(int damageAmount, DamageTypes damageType)
    {
        if (this.name == "extended hitbox")
        {
            this.transform.parent.gameObject.GetComponent<Health>().takeDamage(damageAmount, damageType);

        }

        if (damageType == DamageTypes.Elemental && !anim.GetBool("StartFight"))
        {
            // Debug.Log(damageType);
            if (damageType == DamageTypes.Elemental)
            {
                damageAmount = 10;

            }
        }

        if (this.name == "Warrior Idle" && currentHealthPCT < 9999f)
        {

            if (!startfightonce)
            {
                //fix later
                try
                {
                    GameObject.Find(ActivePlayerManager.ActivePlayerName).GetComponent<DashAbility>().ResetDashes();
                }
                catch { Debug.Log("FIX LaTER THAT START FIGHT IS ALSO OK WITH MARCO"); }
                startfightonce = true;
                StartCoroutine(setStartFightTrue());
            }


        }

        if (this.name == "Warrior Idle" && currentHealthPCT < .6666666f && phasingToMiddle.Phasecount == 0)
        {

            if (currentHealth > 0)
            {
                damageAmount = (int)(phasingDmgReduction * damageAmount);
                showDmgDist();
            }

        }

        if (this.name == "Warrior Idle" && currentHealthPCT < .3333333f && phasingToMiddle.Phasecount == 1)
        {
            if (currentHealth > 0)
            {
                damageAmount = (int)(phasingDmgReduction * damageAmount);
                showDmgDist();
            }
        }


        movehits = true;

        //kan geen dmg krijgen als invuln is
        if (isinvulnerable)
        {
            return;
        }

        //sent to hitsplat
        if (damageType == DamageTypes.Arcane)
        {
            tookThisDmg = 1 * damageAmount;


        }
        if (damageType == DamageTypes.Elemental)
        {
            tookThisDmg = 1 * damageAmount;
            purpledmg += damageAmount;
        }
        if (damageType == DamageTypes.Fire)
        {
            tookThisDmg = 1 * damageAmount;
            aadmg += damageAmount;
        }

        else
        {
            tookThisDmg = 1 * damageAmount;


        }





        //aoe dmg kan maar 1x hitten per aoe cooldown



        if (damageType == DamageTypes.Arcane && damageAmount >= 0)
        {

            if (!AOEcooldown)
            {
                spelldmg += damageAmount;

                AOEcooldown = true;
                currentHealth -= damageResistance.CalculateDamageWithResistance(damageAmount, damageType);

                StartCoroutine(AOECooldownReset());

                if (floatingTextPrefab != null)
                {
                    if (AOEcooldown == true)
                    {
                        ShowFloatingTextAOE();
                    }
                }
            }
            //else gebeurt er dus niks

        }


        if (damageAmount < 0 && damageType == DamageTypes.Water)
        {

            //makes heal cancel when phased
            if (!phasedKillMinion)
            {
                currentHealth -= damageResistance.CalculateDamageWithResistance(damageAmount, damageType);
            }

            if (floatingTextPrefab != null)
            {
                ShowFloatingTextHeal();
            }
        }
        if (damageType == DamageTypes.Elemental && damageAmount >= 0)
        {

            currentHealth -= damageResistance.CalculateDamageWithResistance(damageAmount, damageType);

            if (floatingTextPrefab != null)
            {
                ShowFloatingTextElemental();
                //  StartCoroutine(delayElementalSplat());//  ShowFloatingTextElemental();
            }
        }
        if (damageType == DamageTypes.Water && damageAmount >= 0)
        {



            currentHealth -= damageResistance.CalculateDamageWithResistance(damageAmount, damageType);

            spelldmg += damageResistance.CalculateDamageWithResistance(damageAmount, damageType);

            if (floatingTextPrefab != null)
            {
                ShowFloatingTextWater();
            }
        }

        if (damageType == DamageTypes.Fire && damageAmount >= 0)
        {


            currentHealth -= damageResistance.CalculateDamageWithResistance(damageAmount, damageType);

            if (floatingTextPrefab != null)
            {
                ShowFloatingTextFire();
            }
        }





        //procentueel hp
        currentHealthPCT = (float)currentHealth / (float)startingHealth;

        //set hp to current health
        healthBar.SetHealth(currentHealthPCT);


        //object specific triggers
        // if (this.name == "Warrior Idle" && currentHealthPCT < .5f) { anim.SetInteger("Phase", 1); }
        if (this.name == "Warrior Idle" && currentHealthPCT < .6666666f) { anim.SetInteger("Phase", 1); if (!phaseonce) { anim.SetBool("Phasing", true); phaseonce = true; } }
        if (this.name == "Warrior Idle" && currentHealthPCT < .33333333f) { anim.SetInteger("Phase", 2); if (!phasetwice) { anim.SetBool("Phasing", true); } phasetwice = true; }





        //if glass box is killed -> start function that sets p2 enter true
        if (this.name == "GlassBoxG")
        {
            if (anim.GetInteger("Phase") == 1)
            {
                if (currentHealthPCT <= .9999f)
                {
                    //start charging fire blast
                    firetotems1[0].SetActive(true);
                    dragonhead[0].SetActive(true); //dragonhead fire glow
                    dragonhead[2].SetActive(true); //basket
                }

                if (currentHealthPCT <= .66666f)
                {
                    //spawn fire
                    firetotems1[1].SetActive(true);
                    firetotems1[2].SetActive(true);
                }

                if (currentHealthPCT <= .33333f)
                {
                    //spawn fire
                    firetotems1[3].SetActive(true);
                    firetotems1[4].SetActive(true);

                }
            }

            if (anim.GetInteger("Phase") == 2)
            {
                if (currentHealthPCT <= .9999f)
                {
                    //start charging fire blast
                    firetotems2[0].SetActive(true);
                    firetotems1[0].GetComponent<ChaneLightIntensityReduce>().enabled = true;
                    dragonhead[1].SetActive(true); //dragonhead fire glow
                    dragonhead[3].SetActive(true); //basket
                }

                if (currentHealthPCT <= .66666f)
                {
                    //spawn fire
                    firetotems2[1].SetActive(true);
                    firetotems2[2].SetActive(true);
                }

                if (currentHealthPCT <= .33333f)
                {
                    //spawn fire
                    firetotems2[3].SetActive(true);
                    firetotems2[4].SetActive(true);

                }
            }


            if (currentHealthPCT <= 0f)
            {
                if (anim.GetInteger("Phase") == 1)
                {

                    for (int i = 1; i <= 4; i++) { firetotems1[i].SetActive(false); }  //quickly put out fire
                    for (int i = 5; i < 7; i++) { firetotems1[i].SetActive(false); } //remove double sound
                    for (int i = 1; i <= 4; i++) { firetotems1[i].SetActive(true); } //double fire

                    Phase01AA functioncaller = GameObject.Find("Warrior Idle").GetComponent<Phase01AA>();
                    functioncaller.StartDelayDragonShineOff();

                }

                if (anim.GetInteger("Phase") == 2)
                {

                    for (int i = 1; i <= 4; i++) { firetotems2[i].SetActive(false); }  //quickly put out fire
                    for (int i = 5; i < 7; i++) { firetotems2[i].SetActive(false); } //remove double sound
                    for (int i = 1; i <= 4; i++) { firetotems2[i].SetActive(true); } //double fire
                    Phase01AA functioncaller = GameObject.Find("Warrior Idle").GetComponent<Phase01AA>();
                    functioncaller.StartDelayDragonShineOff();
                }


                tpl.StartBeam();

                if (anim.GetInteger("Phase") == 1)
                {

                    Invoke("delayp2instakill", 0.25f);
                }
                if (anim.GetInteger("Phase") == 2)
                {
                    Invoke("delayp3instakill", 0.25f);

                }

            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void CreateHitsplat()
    {

    }


    //death animations
    #region

    public void Die()
    {


        if (gameObject.name.Contains("Warrior")) // || gameObject.name == "Healing Minion(Clone)")
        {
            TextMeshProStopWatchSecond.counting = false;

            isinvulnerable = true;
            Debug.Log("SHOULD START COROuTINE");
            Canvashpboss.SetActive(false);
            StartCoroutine(DieBoss());
            this.gameObject.GetComponent<TrollController>().enabled = false;
            //wendigo.SetActive(true);

        }

        if (gameObject.name.Contains("Healing Minion")) // || gameObject.name == "Healing Minion(Clone)")
        {
            if (Phase01AA.amntMinions >= 0)
            {
                Phase01AA.amntMinions -= 1;
            }
            if (gameObject.name.Contains("Healing Minion1")) { Phase01AA.filledspot1 = false; Phase01AA.alreadygot.Remove(2); }
            if (gameObject.name.Contains("Healing Minion2")) { Phase01AA.filledspot2 = false; Phase01AA.alreadygot.Remove(1); }
            if (gameObject.name.Contains("Healing Minion3")) {
                Phase01AA.filledspot3 = false;
                if (Phase01AA.filledspot2 == true) { Phase01AA.alreadygot.Remove(1); }
                if (Phase01AA.filledspot4 == true) { Phase01AA.alreadygot.Remove(4); }
            }
            if (gameObject.name.Contains("Healing Minion4")) { Phase01AA.filledspot4 = false; Phase01AA.alreadygot.Remove(4); }
            if (gameObject.name.Contains("Healing Minion5")) { Phase01AA.filledspot5 = false; Phase01AA.alreadygot.Remove(3); }
            if (gameObject.name.Contains("Healing Minion6")) {
                Phase01AA.filledspot6 = false;
                if (Phase01AA.filledspot1 == true) { Phase01AA.alreadygot.Remove(2); }
                if (Phase01AA.filledspot5 == true) { Phase01AA.alreadygot.Remove(3); }
            }


            Debug.Log("dA should start");
            StartCoroutine(DieMinion());
        }
        else
        {
            if (!gameObject.name.Contains("Warrior"))
            {
                gameObject.SetActive(false);
            }


        }

    }
    private bool bossisdeath = false;


    public void showDmgDist()
    {
        Debug.Log("abilitydmg " + spelldmg);
        Debug.Log("basic attack dmg " + aadmg);
        Debug.Log("purple dmg " + purpledmg);
    }

    public IEnumerator DieBoss()
    {
        showDmgDist();

        phasedKillMinion = true;
        animboss.SetBool("isDead", true);
        aus.Play();
        yield return new WaitForSeconds(4.5f);
        animboss.SetBool("isDead", false);
        gameObject.SetActive(false);
        yield return null;
        wendigo.SetActive(true);

    }


    public IEnumerator DieMinion()
    {

        //death animation minion!
        //   if (this.name == "")

        anim.SetBool("isDeath", true);
        anim.SetTrigger("Die");

        yield return new WaitForSeconds(4.5f);
        anim.SetBool("isDeath", false);
        anim.ResetTrigger("Die");
        gameObject.SetActive(false);

        yield return null;
    }
    #endregion 

}
