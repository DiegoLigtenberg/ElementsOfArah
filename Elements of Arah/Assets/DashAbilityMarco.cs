using CreatingCharacters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;

namespace CreatingCharacters.Abilities
{
    [RequireComponent(typeof(ThirdPersonMovement))]
    public class DashAbilityMarco : Ability
    {
        public Animator anim;
        [SerializeField] private float dashForce;
        [SerializeField] private float dashDuration;
        public CinemachineFreeLook fl;
        private ThirdPersonMovement thirdPersonPlayer;
        [SerializeField] private CharacterController charController;
        public GameObject dashdirection;
        public GameObject self;
        private Vector3 dashdir;
        public GameObject dashcampos;

        private bool onlyonce_dash;

        public static float AACorrection;


        public GameObject nomana;

        private float outofcombatmultiplier;
        public static bool PhasingBugFixAA;
        [HideInInspector] public bool quickfix = false;
        [HideInInspector] public bool isactivated = false;
        [HideInInspector] public bool aabugActivate = false; //free var- not used - is when dash is activated


        [HideInInspector] public BeamAbility beam;

        public Image abilityImage; //the hidden image in canvas

        float lastStep, timeBetweenSteps = 0.2f;

        //  public GameObject playerPathFindHitBox;

        /// <summary>
        ///Animator control
        /// </summary>
        public GameObject[] effect;
        public Transform[] effectTransform;


        public static float Beamready;
        [SerializeField] private float dashRechargeTime;
        [SerializeField] private int maxDashes = 3;
        [HideInInspector] public int remainingDashes = 1;
        private float currentDashRechargeTime;
        private float currentDashRechargeTime2;

        [HideInInspector] public int orbCount;

        private void Awake()
        {
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();

            abilityImage.fillAmount = 0;
            remainingDashes = 1;
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
            ThirdPersonMovement.canmovecamera = true;
            beam = GetComponent<BeamAbility>();
            currentDashRechargeTime = 0;
            currentDashRechargeTime2 = 0;
            outofcombatmultiplier = 2;

            PhasingBugFixAA = false;

            abilityKey = InputManager.instance.getKeyCode("marcodash");

        }
        private Vector3 dashtransform;

        // Start is called before the first frame update
        void Start()
        {
            dashtransform = transform.position;
            isDashing = false;

        }




        //dont run this, because you get dmg before it resets
        public void ResetDashes()
        {
            remainingDashes = 1;
            currentDashRechargeTime = 0;
            abilityImage.fillAmount = 1;
            outofcombatmultiplier = 1f;
        }

        private void CooldownData()
        {



            if (!onlyonce_dash)
            {

                abilityImage.fillAmount = 1;
                onlyonce_dash = true;
            }

            if (remainingDashes < maxDashes)
            {
                abilityImage.fillAmount -= 1 / (dashRechargeTime / outofcombatmultiplier) * Time.deltaTime;
                if (abilityImage.fillAmount <= 0)
                {
                    abilityImage.fillAmount = 0;

                    if (remainingDashes < maxDashes)
                    {
                        onlyonce_dash = false;
                    }
                    if (CooldownHandler.outOfCombat) { outofcombatmultiplier = 2f; }
                    else { outofcombatmultiplier = 1f; }

                }

            }
            else
            {
                abilityImage.fillAmount = 0;
                onlyonce_dash = true;
            }
        }


        public override void Cast()
        {
            if (quickfix == false)
            {
                StartCoroutine(Dash());
            }




        }

        // Update is called once per frame
        void Update()
        {
            CooldownData();
            base.Update();


            dashdir = (self.transform.position - dashdirection.transform.position); //.normalized;
                                                                                    //  Debug.Log(self.transform.position);
                                                                                    // Debug.Log(dashdirection.transform.position);
            dashtransform = GameObject.Find("dashcampos").transform.position;
            // Debug.Log(dashtransform);

            // Debug.Log(Camera.main.transform.eulerAngles);

            if (remainingDashes < maxDashes)
            {
                currentDashRechargeTime += Time.deltaTime * outofcombatmultiplier;
                if (currentDashRechargeTime >= dashRechargeTime)
                {
                    remainingDashes++;
                    currentDashRechargeTime = 0f;

                }
            }
            if (remainingDashes == maxDashes)
            {
                currentDashRechargeTime2 = 0;
            }

            if (remainingDashes == 0)
            {
                nomana.SetActive(true);
            }
            if (remainingDashes > 0)
            {
                nomana.SetActive(false);
            }

            if (isDashing)
            {

            }

                // doesnt work -> attempt to not be able to aa when in air from dash
                /*
                if (isDashing)
                {
                    if (Ability.globalCooldown <= 1.0f)
                    {
                        Ability.globalCooldown = 1.0f;

                        if (!charController.isGrounded) { isDashing = false; CooldownHandler.Instance.ReduceAbilityCooldownToValue(GetComponent<BasicAttackMarco>(), 0.9f); }
                    }
                }
                */





                if (remainingDashes == 0)
            {
                orbCount = 0;
            }

            if (remainingDashes == 1)
            {
                orbCount = 1;
            }

            if (remainingDashes == 2)
            {
                orbCount = 2;
            }
            if (remainingDashes == 3)
            {
                orbCount = 3;
            }


        }
        public static bool isDashing;
        public IEnumerator Dash()
        {

            // set global cd early so we cant hit aftrer dashing
            if (Ability.globalCooldown <= 0.74f)
            {
                Ability.globalCooldown = 1.35f;
            }

            if (remainingDashes <= 0) { yield break; }
            if (GetComponent<RapidFireMarco>().isFiring) { yield break; } //less strict than true channel

            isDashing = true;
            anim.SetTrigger("Teleport");
            quickfix = true;


            thirdPersonPlayer.ResetImpactY();
            thirdPersonPlayer.gravity = 0;
            dashtransform = GameObject.Find("dashcampos").transform.position;
            Ability.animationCooldown = 0.8f;  //je kan al iets eerder loop input geven dan dat je weer ability kan doen!
            yield return new WaitForSeconds(0.091f);


            remainingDashes--;

            Ability.animationCooldown = 1.0f;  //je kan al iets eerder loop input geven dan dat je weer ability kan doen!

            if (Ability.globalCooldown <= 0.74f)
            {
                Ability.globalCooldown = 0.74f;
            }


           // charController.enabled = false;
            //thirdPersonPlayer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            
            thirdPersonPlayer.gravity = -9.81f;
            thirdPersonPlayer.gravity = thirdPersonPlayer.gravity * 2;
            GetComponent<MarcoMovementController>().jumptimer = 2f; //this is a jump
            fl.m_Priority = 11;

            yield return new WaitForSeconds(0.1f);

            float magnitude = 0;
      
            thirdPersonPlayer.ResetImpactY();
            thirdPersonPlayer.AddForce(dashdir, dashForce);
            yield return new WaitForSeconds(0.1f);
            //charController.enabled = true;
            //thirdPersonPlayer.enabled = true;
            yield return new WaitForSeconds(0.2f);
            fl.m_Priority = 9;
            yield return new WaitForSeconds(0.4f);
            isDashing = false;
  

            quickfix = false;
            /*
            if (Camera.main.transform.eulerAngles.x < 58 && !thirdPersonPlayer.isGrounded)
            {
               // thirdPersonPlayer.ResetImpactY();
               // thirdPersonPlayer.AddForce(dashdir, dashForce);


                magnitude = Camera.main.transform.eulerAngles.x * 0.8f;                     //general movement in camera direction
                thirdPersonPlayer.AddForce(transform.forward.normalized, magnitude * -0.5f);  //move backwards relative to player
                thirdPersonPlayer.AddForce(Vector3.up, magnitude * -0.3f);                  //move upwards
            }
            thirdPersonPlayer.AddForce(Camera.main.transform.forward, -magnitude);
            */
            yield return null;

        }

    }
}