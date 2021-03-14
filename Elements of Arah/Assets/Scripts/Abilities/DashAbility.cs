using CreatingCharacters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

namespace CreatingCharacters.Abilities
{
    [RequireComponent(typeof(ThirdPersonMovement))]
    public class DashAbility : Ability
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float dashDuration;

        private ThirdPersonMovement thirdPersonPlayer;

        [SerializeField] private SkinnedMeshRenderer mr;

        [SerializeField] private CharacterController charController;

        [SerializeField] private float dashRechargeTime;
        [SerializeField] private int maxDashes = 3;
        [HideInInspector] public int remainingDashes = 1;
        private float currentDashRechargeTime;
        private float currentDashRechargeTime2;

        public CinemachineFreeLook fl;
        [HideInInspector] public bool quickfix = false;
        [HideInInspector] public bool isactivated = false;
        [HideInInspector] public bool aabugActivate = false; //free var- not used - is when dash is activated

        public CapsuleCollider cc;
        [HideInInspector] public BeamAbility beam;

        public Image abilityImage; //the hidden image in canvas

        float lastStep, timeBetweenSteps = 0.2f;

        public GameObject playerPathFindHitBox;

        /// <summary>
        ///Animator control
        /// </summary>
        static Animator anim;
        public GameObject[] effect;
        public Transform[] effectTransform;


        public static float Beamready;
        public static float AACorrection;

        public GameObject[] circlingBalls;

        [HideInInspector] public GameObject nomana;

        private float outofcombatmultiplier;
        public static bool PhasingBugFixAA;

   

        private void Awake()
        {

            abilityImage.fillAmount = 0;
            remainingDashes = 1;
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
            ThirdPersonMovement.canmovecamera = true;
            beam = GetComponent<BeamAbility>();
            currentDashRechargeTime = 0;
            currentDashRechargeTime2 = 0;
            outofcombatmultiplier = 2;

            PhasingBugFixAA = false;
        }

        private void Start()
        {
            abilityKey = InputManager.instance.getKeyCode("dash");

        }

        private bool onlyonce_dash;

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

        private bool shortBuff;
        [HideInInspector] public int orbCount;
        public IEnumerator DashBuff()
        {
            shortBuff = true;
            yield return new WaitForSeconds(5f);
            shortBuff = false;
        }
        protected override void Update()
        {

            CooldownData();
            //  Debug.Log(remainingDashes);

            //runt ook base update
            base.Update();
            // Cast();

            if (Beamready >= 0) { Beamready -= Time.deltaTime; }
            if (AACorrection >= 0) { AACorrection -= Time.deltaTime; }

            //Debug.Log(remainingDashes);


            //
            /* 
            currentDashRechargeTime2 += Time.deltaTime;
            if (currentDashRechargeTime2 >= dashRechargeTime)
            {

                if (currentDashRechargeTime2 >= dashRechargeTime)
                {
                    StartCoroutine(DashBuff());
                    currentDashRechargeTime2 = 0f;

                }
            }
            */

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




            if (GameObject.Find(ActivePlayerManager.ActivePlayerName).GetComponent<DashAbility>().isactivated)
            {
                circlingBalls[0].SetActive(false);
                circlingBalls[1].SetActive(false);
                circlingBalls[2].SetActive(false);
            }
            else
            {

                // if (shortBuff) {

                /*
                if (remainingDashes == 0)
                {
                    circlingBalls[0].SetActive(false);
                    circlingBalls[1].SetActive(false);
                    circlingBalls[2].SetActive(false);
                    orbCount = 0;

                }
                */
                if (remainingDashes == 0)
                {
                    circlingBalls[0].SetActive(false);
                    circlingBalls[1].SetActive(false);
                    circlingBalls[2].SetActive(false);
                    orbCount = 0;
                }

                if (remainingDashes == 1)
                {
                    circlingBalls[0].SetActive(true);

                    circlingBalls[1].SetActive(false);
                    circlingBalls[2].SetActive(false);
                    orbCount = 1;
                }

                if (remainingDashes == 2)
                {
                    circlingBalls[0].SetActive(true);
                    circlingBalls[1].SetActive(true);

                    circlingBalls[2].SetActive(false);
                    orbCount = 2;
                }
                if (remainingDashes == 3)
                {
                    circlingBalls[0].SetActive(true);
                    circlingBalls[1].SetActive(true);
                    circlingBalls[2].SetActive(true);
                    orbCount = 3;
                }
                // }





            }






        }



        public override void Cast()
        {
            //  if (Input.GetKeyDown(KeyCode.B) && quickfix == false)
            if (quickfix == false)
            {
                StartCoroutine(Dash());
                beam.canceldash = true;
                StartCoroutine(DashBuff());

                if (currentDashRechargeTime == maxDashes)
                {
                    onlyonce_dash = false;
                }




            }
        }




        public virtual IEnumerator Dash()
        {
            if (remainingDashes <= 0) { yield break; }


            //the whole actions of ability
            if (isactivated == false)
            {
                ThirdPersonMovement.canmovecamera = true;
                cc.enabled = false; //of  cilinder ollision


                charController.enabled = false;

                Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);
                Beamready = 1.1f;
                PhasingBugFixAA = true;
                AACorrection = 2.2f;
                yield return new WaitForSeconds(0.001f);
                //cooldown setting
                Ability.animationCooldown = 0.8f;  //je kan al iets eerder loop input geven dan dat je weer ability kan doen!

                if (Ability.globalCooldown <= 0.74f)
                {
                    Ability.globalCooldown = 0.74f;
                }

                playerPathFindHitBox.SetActive(false);
                //////////////////////////////////

                isactivated = true;
                remainingDashes--;
                fl.m_Priority = 11;
                quickfix = true;



                mr.enabled = false;
                thirdPersonPlayer.isChargingDash = true;
                thirdPersonPlayer.chargeDelay = true;

                thirdPersonPlayer.gravity = 0;

                yield return new WaitForSeconds(0.24f);
                aabugActivate = true;

                yield return new WaitForSeconds(0.24f);

                charController.enabled = true;
                //////////////////////////////////////////////

                isactivated = false;
                isactivated = true;
                ThirdPersonMovement.canmovecamera = false;

                yield return new WaitForSeconds(0.1f);
                //////////////////////////////////////////////


                charController.height = 0.0f;
                charController.radius = 0.0f;


                thirdPersonPlayer.chargeDelay = true;
                thirdPersonPlayer.gravity = 0;

                yield return new WaitForSeconds(0.2f);
                Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
                yield return new WaitForSeconds(0.2f);
                //////////////////////////////////////////////



                thirdPersonPlayer.chargeDelay = false;
                thirdPersonPlayer.AddForce(Camera.main.transform.forward * dashForce, dashForce);
                //waarde tussen 0 en maximaal 50 erbij door downward vel (Zodat je in midden komt)
                thirdPersonPlayer.AddForce(Camera.main.transform.up * dashForce, 20 + Mathf.Clamp(50f * Mathf.Abs(ThirdPersonMovement.velocityY), 0, 5f) * Time.deltaTime);

                ThirdPersonMovement.velocityY = 0f;
                ThirdPersonMovement.canmovecamera = true;

                yield return new WaitForSeconds(dashDuration);
                //////////////////////////////////////////////

                cc.enabled = true;
                playerPathFindHitBox.SetActive(true);

                charController.height = 2;
                charController.radius = 0.5f;
                mr.enabled = true;
                thirdPersonPlayer.isChargingDash = false;
                thirdPersonPlayer.ResetImpact();

                if (ThirdPersonMovement.isLevitating)
                {
                    thirdPersonPlayer.gravity = Mathf.Lerp(-1, -9.81f, Time.deltaTime * thirdPersonPlayer.levitateDuration);
                }
                if (!ThirdPersonMovement.isLevitating)
                {
                    thirdPersonPlayer.gravity = -9.81f;
                }
                //////////////////////////////////////////////

                isactivated = false;
                aabugActivate = false;
                quickfix = false;

                //animation of camera going smooth
                yield return new WaitForSeconds(0.2f);
                fl.m_Priority = 9;
                Debug.Log("we finished ");

                yield return new WaitForSeconds(2f); //the total couroutine should be 2.5 + .4f seconds
                PhasingBugFixAA = false;

            }
        }


    }

}
