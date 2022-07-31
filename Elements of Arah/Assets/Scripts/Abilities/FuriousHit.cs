using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Player;
using UnityEditor;
using UnityEngine.UI;
using TMPro;


namespace CreatingCharacters.Abilities
{

    public class FuriousHit : Ability
    {

        public Animator anim;
        public GameObject[] effect;
        public Transform[] effectTransform;

        public GameObject[] FullCircle;
        public Transform[] FullCircleTransform;

        public GameObject go;
        public PlayerSpinAoe psa;
        public Transform cube;

        float lastStep, timeBetweenSteps = 0.065f;
        private bool abilIsActive;

        public CharacterController characterController;

        private int stateOfAbil;

        public Transform beginningPoint;

        public Rigidbody rb;

        public float cooldownFireBreath;
        private float maxcooldownFireBreath;

        public float setMaxCooldownCharges;
        public float setMaxCooldownFinalCharge;

        public float setTickCooldown;

        public Transform parent;

        [SerializeField] private AnimatorOverrideController[] overrideControllers;
        [SerializeField] private AnimatorOverrider overrider;

        public Image abilityImage;  //the hidden image in canvas
        public Image tickImage;

        public TextMeshProUGUI text;
        public GameObject textobject;
        public GameObject textobjectcd;
        public GameObject tickcircle;

        public GameObject nomana;


        public void SetAnim(int value)
        {
            overrider.SetAnimations(overrideControllers[value]);
        }

        public void SetAnimation(AnimatorOverrideController overrideController)
        {
            anim.runtimeAnimatorController = overrideController;
        }


        public bool latecast;
        private bool coroutineonce;

        public int showImageNumber;




        private void Awake()
        {
            //text = FindObjectOfType<TextMeshProUGUI>();

            abilityImage.fillAmount = 0;
            tickImage.fillAmount = 0;
            cooldownFireBreath = 0;
            textobjectcd.SetActive(false);
            abilityKey = InputManager.instance.getKeyCode("furioushit");
        }


        private bool tickcooldownswitch;

        /*
        private void TickCooldownData()
        {
    
            if (Ability.tickCooldown >0 && !tickcooldownswitch && cooldownFireBreath <= 0 ) // && !tickcooldownswitch  && abilityImage.fillAmount < 0.1f && cooldownFireBreath <= 0)
            {
                tickcooldownswitch = true;
                latecast = false;
                tickImage.fillAmount = 1;
            }

            if (tickCooldown != 0)
            {                                  //settick is 2.4

                if ( Ability.tickCooldown >= cooldownFireBreath )
                {
                    tickImage.fillAmount -= 1 / setTickCooldown * Time.deltaTime;
                }
                if (Ability.tickCooldown < cooldownFireBreath)
                {
                    Debug.Log("should be 0 ");

                    tickImage.fillAmount = 0;
                }
              

                if (tickImage.fillAmount <= 0)
                {
                    tickImage.fillAmount = 0;
                    tickcooldownswitch = false;
                }
            }
            
        }
        */
        private void CooldownData()
        {
            if (showImageNumber < 3) { tickcircle.SetActive(true); }
            else { tickcircle.SetActive(false); }

            if (!PauseMenu.GameIsPaused)
            {
                if (latecast || reducedcooldown) //abilitycooldownleft
                {

                    latecast = false;
                    test = true;
                    reducedcooldown = false;

                    // cooldownFireBreath = 0;
                    abilityImage.fillAmount = 1;
                }

                //abilitycooldownleft
                if (cooldownFireBreath != 0)
                {
                    textobjectcd.SetActive(true);
                    //+0.0001f zorgt ervoor dat je geen zwart vlakje erover krijgt als je game paused
                    abilityImage.fillAmount -= 1 / (maxcooldownFireBreath + 0.0001f) * Time.deltaTime;

                    if (cooldownFireBreath < 0.05f)
                    {
                        textobjectcd.SetActive(false);
                    }

                    if (abilityImage.fillAmount <= 0)
                    {
                        abilityImage.fillAmount = 0;
                        test = false;

                    }
                }
            }

        }

        private bool reducedcooldown;
        private bool startReducingImage;

        public void ChargeCooldown()
        {
            //////////////////////////////////
            if (allowChargeChange)
            {


                if (oncecharge)
                {
                    oncecharge = false;
                    tickImage.fillAmount = 1;
                    startReducingImage = true;

                }


                if (curChargeDuration > 0)
                {
                    curChargeDuration -= Time.deltaTime;
                }

                if (startReducingImage)
                {
                    tickImage.fillAmount -= 1 / 3.5999f * Time.deltaTime; //used to be 3.599

                    if (tickImage.fillAmount <= 0 && stateOfAbil <= 3)
                    {
                        tickImage.fillAmount = 0;
                        allowChargeChange = false;
                        oncecharge = false;
                        startReducingImage = false;

                        showImageNumber = 3;

                        if (stateOfAbil != 4)
                        {
                            Debug.Log("changing cooldown");
                            maxcooldownFireBreath = setMaxCooldownFinalCharge;
                            cooldownFireBreath = maxcooldownFireBreath;
                        }


                        reducedcooldown = true;
                        StartCoroutine(reducestate());

                    }

                }

            }
        }

        private bool allowChargeChange;
        private bool oncecharge;
        private float maxChargeDuration = 3.6f; //used to be 3.6
        private float curChargeDuration;

        private bool reducestateonce;
        public IEnumerator reducestate()
        {
            if (!reducestateonce)
            {
                reducestateonce = true;

                textobject.SetActive(false);
                yield return new WaitForSeconds(setMaxCooldownFinalCharge - 0.1f);
                allowChargeChange = false;

                // GetComponent<Ability>().onlyonce = false;
                // yield return new WaitForSeconds(0.1f);
                Debug.Log("set only once false");
                // GetComponent<Ability>().onlyonce = false;
                cooldownFireBreath = 0f;
                textobject.SetActive(true);
                stateOfAbil = 1;
                reducestateonce = false;
            }

        }

        private void ChargeRemover()
        {
            oncecharge = true;
            allowChargeChange = true;
            curChargeDuration = maxChargeDuration;


        }

        private IEnumerator ChargeRemoveCouroutine()
        {

            yield return new WaitForSeconds(1.8f);


            if (curChargeDuration > 0)
            {
                curChargeDuration -= Time.deltaTime;
            }

            else if (curChargeDuration <= 0)
            {
                stateOfAbil = 1;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            stateOfAbil = 1;
            maxcooldownFireBreath = 0f;
            cooldownFireBreath = 0;
            abilityType = 2;
            showImageNumber = 3;


            //    onlyonce = false;
        }


        public override void Cast()
        {
            //  if (Ability.tickCooldown <= 0)
            {


                if (cooldownFireBreath <= 0)
                {

                    ChargeRemover();
                    latecast = true;
                    StartCoroutine(AoeActive());
                    coroutineonce = false;
                    tickCooldown = setTickCooldown;
                    GetComponent<Ability>().onlyonce = false;

                    // StartCoroutine(GetComponent<CooldownReducer>().ShortBuff(abilityType));
                }

                else if (cooldownFireBreath > 0 && cooldownFireBreath < 0.4f)
                {
                    if (!coroutineonce)
                    {
                        StartCoroutine(RecastFireBreath());

                    }
                }
            }

        }


        public IEnumerator RecastFireBreath()
        {
            coroutineonce = true;

            // if (GetComponent<BeamAbility>().usingbeamF == true)
            yield return new WaitForSeconds(cooldownFireBreath + .01f);
            Debug.Log("we actually recasted abil");
            Cast();
            yield break;

        }


        //this boolean makes it so we only perform the cooldown once! no double castS!!  -> it used to be only once in every iteration -> this is easier
        private bool test;


        private IEnumerator InstantiateEffect()
        {
            GameObject[] newobj = new GameObject[8];
            for (int i = 2; i < 7; i++)
            {
                newobj[i] = Instantiate(effect[2], effectTransform[i].position, effectTransform[i].rotation);


                //zodat je niet door targets heen schiet
                if (Gun.TrueDistanceOfCrosshair > 1.5f)
                {

                    if (!ThirdPersonMovement.isLevitating && Ability.animationCooldown < 0.5f)
                    {
                        if (Input.GetKey(KeyCode.W) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))))
                        {

                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(0, 0, 1).normalized * 1.3f;
                            newobj[i].transform.SetParent(null);

                        }
                        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(1, 0, 1f).normalized * 0.5f;
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(-1, 0, 1f).normalized * 0.6f;
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.S) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(0, 0, -1f).normalized * 0f;
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(1, 0, -1f).normalized * 0.25f;
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(-1, 0, -1f).normalized * 0.4f;
                            newobj[i].transform.SetParent(null);
                        }

                        else if (Input.GetKey(KeyCode.D))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(1, 0, 0).normalized * 0.6f;
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(-1, 0, 0f).normalized * 0.7f;
                            newobj[i].transform.SetParent(null);
                        }

                    }

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///levitaing
                    if (ThirdPersonMovement.isLevitating)
                    {
                        if (Input.GetKey(KeyCode.W) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(0, 0, 1).normalized * 1.3f + new Vector3(0, -0.3f, 0);
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(1, 0, 1f).normalized * 0.5f + new Vector3(0, -0.3f, 0);
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(-1, 0, 1f).normalized * 0.6f + new Vector3(0, -0.3f, 0);
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.S) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(0, 0, -1f).normalized * 0f + new Vector3(0, -0.3f, 0);
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(1, 0, -1f).normalized * 0.25f + new Vector3(0, -0.3f, 0);
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(-1, 0, -1f).normalized * 0.4f + new Vector3(0, -0.3f, 0);
                            newobj[i].transform.SetParent(null);
                        }

                        else if (Input.GetKey(KeyCode.D))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(1, 0, 0).normalized * 0.6f + new Vector3(0, -0.5f, 0);
                            newobj[i].transform.SetParent(null);
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            newobj[i].transform.SetParent(parent);
                            newobj[i].transform.localPosition = newobj[i].transform.localPosition + new Vector3(-1, 0, 0f).normalized * 0.7f + new Vector3(0, -0.5f, 0);
                            newobj[i].transform.SetParent(null);

                        }
                    }

                }


                yield return null;
            }
        }

        private IEnumerator imageNumbReducer()
        {
            yield return new WaitForSeconds(setMaxCooldownFinalCharge);

            showImageNumber = 3;

        }

        protected override void Update()
        {
            //Debug.Log(curChargeDuration);
            base.Update();
            CooldownData();
            ChargeCooldown();

            // match generalized  cooldownleft with charged cooldown due to charge ability
            AbilityCooldownLeft = cooldownFireBreath;  
            //TickCooldownData();
            //  Debug.Log(stateOfAbil);         
            // text.text = showImageNumber.ToString();
            if (stateOfAbil >= 4)
            {
                // stateOfAbil = 1;

            }

            if (showImageNumber <= 0)
            {
                showImageNumber = 3;
                //StartCoroutine(imageNumbReducer());
            }

            if (stateOfAbil == 1)
            {
                if (cooldownFireBreath >= -0 - 0.01)
                {
                    cooldownFireBreath -= Time.deltaTime;
                }
            }
            if (stateOfAbil == 2)
            {
                if (cooldownFireBreath >= -0 - 0.01)
                {
                    cooldownFireBreath -= Time.deltaTime;
                }
            }
            if (stateOfAbil == 3)
            {
                if (cooldownFireBreath >= -0.01)
                {
                    cooldownFireBreath -= Time.deltaTime;
                }
            }
            if (stateOfAbil == 4)
            {
                if (cooldownFireBreath >= -0.01)
                {
                    cooldownFireBreath -= Time.deltaTime;
                }
            }



            if (abilIsActive)
            {
                // if (Time.time - lastStep > timeBetweenSteps)
                {
                    lastStep = Time.time;

                    //  Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);
                    //Instantiate(effect[2], effectTransform[0].position, effectTransform[0].rotation);
                    //  rb.AddForce(rb.transform.forward * 10f,ForceMode.VelocityChange);
                    //   rb.AddForce(rb.transform.up * 100f,ForceMode.VelocityChange);
                }
            }

            // Debug.Log(Ability.energy);
            // Debug.Log(stateOfAbil);

            if (Ability.energy < 30 && cooldownFireBreath <= 0 && stateOfAbil != 4)
            {

                nomana.SetActive(true);
            }
            if ((Ability.energy >= 30))
            {
                nomana.SetActive(false);
            }

            if (stateOfAbil <= 3 && cooldownFireBreath <= 1f && cooldownFireBreath > 0.01f)
            {
                nomana.SetActive(false);
            }


            if (stateOfAbil >= 3 && cooldownFireBreath >= 0)
            {
                nomana.SetActive(false);
            }
        }


        //     private bool onlyonce;

        public virtual IEnumerator AoeActive()
        {

            if (cooldownFireBreath <= 0)
            {

                if (stateOfAbil == 1)//&& //!onlyonce)
                {


                    rb.velocity = Vector3.zero;
                    rb.transform.position = beginningPoint.transform.position;
                    if (characterController.isGrounded)
                    {
                        //onlyonce = true;
                        showImageNumber--;
                        SetAnim(0);
                        maxcooldownFireBreath = setMaxCooldownCharges;

                        //set iteration & rotation of visual
                        psa.castIteration = 1;
                        psa.XrotationAbil = 0; //300
                        psa.YrotationAbil = 300; //300
                        beginningPoint.localRotation = Quaternion.EulerAngles(0, 0, 0);

                        //play animation

                        if (Ability.animationCooldown <= 0f)
                        {
                            Ability.animationCooldown = 0.3f;
                        }
                        if (Ability.globalCooldown <= .2f && GetComponent<BeamAbility>().usingbeamF == false)
                        {
                            Ability.globalCooldown = .2f;
                        }


                        //global and animation cooldown
                        yield return new WaitForSeconds(0.1f);
                        anim.SetInteger("skillNumber", 1);
                        anim.SetTrigger("playSkill");

                        if (Ability.globalCooldown <= .7f)
                        {
                            Ability.globalCooldown = .7f;
                        }




                        cooldownFireBreath = maxcooldownFireBreath;


                        //Sound effect on
                        yield return new WaitForSeconds(0.1f);
                        Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);

                        yield return new WaitForSeconds(0.1f);

                        //delayed animation cooldown
                        //Ability.animationCooldown = .4f;
                        yield return new WaitForSeconds(.2f); //USED TO BE 0.2f

                        //reset the rotation
                        go.SetActive(true);
                        psa.resetRotation();

                        //Delay for actual visual 
                        yield return new WaitForSeconds(0.01f);
                        abilIsActive = true;
                        //instantiating the ability
                        StartCoroutine(InstantiateEffect());


                        //put ability off
                        yield return new WaitForSeconds(.2f);
                        abilIsActive = false;
                        go.SetActive(false);
                        stateOfAbil++;
                        //onlyonce = false;

                    }

                    else if (!characterController.isGrounded)
                    {

                        showImageNumber--;
                        // onlyonce = true;
                        SetAnim(0);
                        maxcooldownFireBreath = setMaxCooldownCharges;
                        //set iteration & rotation of visual
                        psa.castIteration = 1;
                        psa.XrotationAbil = 0;
                        psa.YrotationAbil = 300;
                        beginningPoint.localRotation = Quaternion.EulerAngles(0, 0, 0);

                        //play animation


                        if (Ability.animationCooldown <= 0f)
                        {
                            Ability.animationCooldown = 0.3f;
                        }

                        //global and animation cooldown
                        yield return new WaitForSeconds(0.1f);
                        Ability.globalCooldown = .7f;
                        cooldownFireBreath = maxcooldownFireBreath;
                        anim.SetInteger("skillNumber", 1);
                        anim.SetTrigger("playSkill");

                        //Sound effect on
                        yield return new WaitForSeconds(0.1f);
                        Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
                        yield return new WaitForSeconds(0.1f);

                        //delayed animation cooldown
                        // Ability.animationCooldown = .4f;
                        yield return new WaitForSeconds(.2f); //USED TO BE 0.2f

                        //reset the rotation
                        go.SetActive(true);
                        psa.resetRotation();

                        //Delay for actual visual 
                        yield return new WaitForSeconds(0.01f);
                        abilIsActive = true;

                        //instantiating the ability
                        StartCoroutine(InstantiateEffect());


                        //put ability off
                        yield return new WaitForSeconds(.2f);
                        abilIsActive = false;
                        go.SetActive(false);
                        stateOfAbil++;
                        //onlyonce = false;


                    }
                }

                else if (stateOfAbil == 2)//&& !onlyonce)
                {


                    rb.velocity = Vector3.zero;
                    rb.transform.position = beginningPoint.transform.position;
                    if (characterController.isGrounded)
                    {
                        showImageNumber--;
                        //onlyonce = true;
                        SetAnim(1);
                        maxcooldownFireBreath = setMaxCooldownCharges;
                        //set iteration & rotation of visual
                        psa.castIteration = 2;
                        psa.XrotationAbil = -0f;  //-300
                        psa.YrotationAbil = 300;    //300
                        psa.ZrotationAbil = -0f;  //-300
                        beginningPoint.localRotation = Quaternion.EulerAngles(0, 45.5f, 0);
                        psa.ZrotationAbil = 0;



                        if (Ability.animationCooldown <= 0f)
                        {
                            Ability.animationCooldown = 0.3f;
                        }
                        if (Ability.globalCooldown <= 0.2f && GetComponent<BeamAbility>().usingbeamF == false)
                        {
                            Ability.globalCooldown = .2f;
                        }


                        //global and animation cooldown
                        yield return new WaitForSeconds(0.1f);
                        if (Ability.globalCooldown <= .7f)
                        {
                            Ability.globalCooldown = .7f;
                        }
                        //play animation
                        anim.SetInteger("skillNumber", 1);
                        anim.SetTrigger("playSkill");
                        cooldownFireBreath = maxcooldownFireBreath;

                        //sound effect on
                        yield return new WaitForSeconds(0.1f);
                        Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
                        yield return new WaitForSeconds(0.1f);

                        //delayed animation cooldown 
                        //   Ability.animationCooldown = .4f;
                        yield return new WaitForSeconds(.2f);//USED TO BE 0.2f

                        //reset the rotation
                        go.SetActive(true);
                        psa.resetRotation();

                        //Delay for the actual visual 
                        yield return new WaitForSeconds(0.01f);
                        abilIsActive = true;
                        //instantiating the ability
                        StartCoroutine(InstantiateEffect());

                        //put the ability off
                        yield return new WaitForSeconds(.2f);
                        abilIsActive = false;
                        go.SetActive(false);
                        stateOfAbil++;
                        // onlyonce = false;


                    }

                    else if (!characterController.isGrounded)
                    {

                        showImageNumber--;
                        // onlyonce = true;
                        SetAnim(1);
                        maxcooldownFireBreath = setMaxCooldownCharges;
                        //set iteration & rotation of visual

                        psa.castIteration = 2;
                        psa.XrotationAbil = -0f;
                        psa.YrotationAbil = 300;
                        psa.ZrotationAbil = -0f;
                        beginningPoint.localRotation = Quaternion.EulerAngles(0, 45.5f, 0);
                        psa.ZrotationAbil = 0;

                        //play animation


                        if (Ability.animationCooldown <= 0f)
                        {
                            Ability.animationCooldown = 0.3f;
                        }

                        //global and animation cooldown
                        yield return new WaitForSeconds(0.1f);
                        Ability.globalCooldown = .7f;
                        cooldownFireBreath = maxcooldownFireBreath;
                        anim.SetInteger("skillNumber", 1);
                        anim.SetTrigger("playSkill");


                        //sound effect on
                        yield return new WaitForSeconds(0.1f);
                        Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
                        yield return new WaitForSeconds(0.1f);

                        //delayed animation cooldown 
                        //   Ability.animationCooldown = .4f;
                        yield return new WaitForSeconds(.2f);//USED TO BE 0.2f

                        //reset the rotation
                        go.SetActive(true);
                        psa.resetRotation();

                        //Delay for the actual visual 
                        yield return new WaitForSeconds(0.01f);
                        abilIsActive = true;
                        //instantiating the ability
                        StartCoroutine(InstantiateEffect());


                        //put the ability off
                        yield return new WaitForSeconds(.2f);
                        abilIsActive = false;
                        go.SetActive(false);
                        stateOfAbil++;
                        //   onlyonce = false;

                    }
                }


                else if (stateOfAbil == 3) //&& !onlyonce)
                {
                    reducedcooldown = false;
                    StartCoroutine(reducestate());
                    rb.velocity = Vector3.zero;
                    rb.transform.position = beginningPoint.transform.position;
                    if (characterController.isGrounded)
                    {
                        showImageNumber--;
                        // onlyonce = true;
                        SetAnim(2);
                        maxcooldownFireBreath = setMaxCooldownFinalCharge;
                        //set iteration & rotation of visual
                        psa.castIteration = 3;
                        beginningPoint.localRotation = Quaternion.EulerAngles(0, 0, 0);
                        psa.XrotationAbil = 0;
                        psa.YrotationAbil = 1000;
                        psa.ZrotationAbil = 0;


                        if (Ability.animationCooldown <= 0f)
                        {
                            Ability.animationCooldown = 0.3f;
                        }
                        if (Ability.globalCooldown <= .2f && GetComponent<BeamAbility>().usingbeamF == false)
                        {
                            Ability.globalCooldown = .2f;
                        }
                        //global and animation cooldown
                        yield return new WaitForSeconds(0.1f);
                        if (Ability.globalCooldown <= .7f)
                        {
                            Ability.globalCooldown = .7f;
                        }
                        anim.SetInteger("skillNumber", 1);
                        anim.SetTrigger("playSkill");
                        cooldownFireBreath = maxcooldownFireBreath;
                        //sound effect on
                        yield return new WaitForSeconds(0.1f);
                        Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
                        yield return new WaitForSeconds(0.1f);

                        //delayed animation cooldown
                        // Ability.animationCooldown = .4f;
                        yield return new WaitForSeconds(.2f); //USED TO BE 0.2f

                        //reset the rotation
                        go.SetActive(true);
                        psa.resetRotation();

                        //Delay for actual visual
                        yield return new WaitForSeconds(0.01f);
                        abilIsActive = true;

                        //instantiating the ability
                        StartCoroutine(InstantiateEffect());

                        //put the ability off
                        yield return new WaitForSeconds(.375f);
                        abilIsActive = false;
                        go.SetActive(false);
                        stateOfAbil++;
                        //  onlyonce = false;

                    }

                    else if (!characterController.isGrounded)
                    {
                        // onlyonce = true;
                        StartCoroutine(reducestate());
                        showImageNumber--;
                        SetAnim(2);
                        maxcooldownFireBreath = setMaxCooldownFinalCharge;

                        //set iteration & rotation of visual
                        psa.castIteration = 3;
                        beginningPoint.localRotation = Quaternion.EulerAngles(0, 0, 0);
                        psa.XrotationAbil = 0;
                        psa.YrotationAbil = 1000;
                        psa.ZrotationAbil = 0;


                        if (Ability.animationCooldown <= 0f)
                        {
                            Ability.animationCooldown = 0.3f;
                        }
                        //global and animation cooldown
                        yield return new WaitForSeconds(0.1f);
                        Ability.globalCooldown = .7f;
                        cooldownFireBreath = maxcooldownFireBreath;
                        anim.SetInteger("skillNumber", 1);
                        anim.SetTrigger("playSkill");

                        //sound effect on
                        yield return new WaitForSeconds(0.1f);
                        Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
                        yield return new WaitForSeconds(0.1f);

                        //delayed animation cooldown
                        Ability.animationCooldown = .4f;
                        yield return new WaitForSeconds(.2f); //USED TO BE 0.2f

                        //reset the rotation
                        go.SetActive(true);
                        psa.resetRotation();

                        //Delay for actual visual
                        yield return new WaitForSeconds(0.01f);
                        abilIsActive = true;
                        //instantiating the ability
                        StartCoroutine(InstantiateEffect());

                        //put the ability off
                        //put the ability off
                        yield return new WaitForSeconds(.375f);
                        abilIsActive = false;
                        go.SetActive(false);
                        stateOfAbil++;
                        // onlyonce = false;

                    }
                }
            }

        }
    }

}