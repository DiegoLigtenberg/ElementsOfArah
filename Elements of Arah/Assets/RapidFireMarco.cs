using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class RapidFireMarco : Ability
    {

        public Animator anim;
        public Animator animboss;
        public GameObject[] effect;
        public Transform[] effectTransform;
        public Transform curCamTransform;
        private Vector3 oldCamTransform;
        private Vector3 jumpCamTransform;
        private Quaternion oldCamRotation;
        public Image abilityImage;   //the hidden image in canvas
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;
        private bool pyramid; //fixes p1 bug when pyramid comes up
                              // Start is called before the first frame update
        private ThirdPersonMovement thirdPersonPlayer;
        public AudioSource[] aus;
        private bool afterpyramid;

        public static int rapidFireHits;
        public bool isFiring;

        public float first_hit_timer;

        private void Awake()
        {
            abilityImage.fillAmount = 0;
           // abilityType = 1;
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
            rapidFireHits = 0;
        }

        public IEnumerator remove_firing()
        {
            yield return new WaitForSeconds(0.3f);
            isFiring = false;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();


            getdmg = AbilityDamage;

            if (animboss.GetBool("Phasing") && !pyramid && !afterpyramid)
            {
                StartCoroutine(removePyramid());
            }

            if (first_hit_timer > 0)
            {
                first_hit_timer -= Time.deltaTime;
            }

            if (isFiring)
            {
                if (Input.GetKey(abilityKey) && energy >= 10 || first_hit_timer>0f)
                {
                    anim.SetBool("rapidFireActive", true);
           
                    if (rapidFireHits > 3)
                    {

                        
                        if (Ability.globalCooldown <= 0.6f)
                        {
                            Ability.globalCooldown = 0.6f;

                        }
                        
                    }
                        if (Ability.animationCooldown <= 1.0f)
                        {
                            Ability.animationCooldown = 0.25f;
                        }
                    
                  
                }
          
                else
                {

                    if (Ability.globalCooldown <= 0.4f)
                    {
                        Ability.globalCooldown = 0.4f;

                    }
                    if (Ability.animationCooldown <= 1.0f)
                    {
                        Ability.animationCooldown = 0.25f;
                    }
                    anim.SetBool("rapidFireActive", false);
                    if (rapidFireHits != 0)
                    {
                        rapidFireHits = -1;
                        anim.ResetTrigger("basicAttack");
                        anim.ResetTrigger("basicAttackx2");
                        anim.ResetTrigger("rapidFire");
                        GetComponent<AE_BowString>().InHand = false;
                        StartCoroutine(remove_firing());
                    }
                }
                

            }
           

        }

        private int doublehit;
        public override void Cast()
        {
            latecast = true;
            doublehit += 1;

            oldCamTransform = curCamTransform.position;
            oldCamRotation = curCamTransform.rotation;
            rapidFireHits = 1;

            isFiring = true;

            StartCoroutine(basicAttack());

            first_hit_timer = 0.6f;
            // Instantiate(effect[1], effectTransform[0].position, effectTransform[1].transform.rotation);
        }

      

        private void CooldownData()
        {
            if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0 && latecast || latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
            }

            if (abilityCooldownLeft != 0)
            {
                abilityImage.fillAmount -= 1 / AbilityCooldown * Time.deltaTime;
                if (abilityImage.fillAmount <= 0.05f)
                {
                    abilityImage.fillAmount = 0;
                }
            }
        }

        public IEnumerator basicAttack()
        {
            jumpCamTransform = curCamTransform.position;

            if (Ability.globalCooldown <= 0.05f)
            {
               Ability.globalCooldown = 0.05f;

            }

        //    if (Input.GetKey(KeyCode.LeftShift))    // (doublehit % 3 == 0)
            {
                anim.SetTrigger("rapidFire");

            }
 
            anim.SetBool("casted", true);

            yield return new WaitForSeconds(0.001f);

            Ability.globalCooldown = 0.6f;

            if (Input.GetKey(KeyCode.LeftShift))//(doublehit % 3 == 0)
            {
                if (Ability.animationCooldown <= 1.3f)
                {
                    Ability.animationCooldown = 1.0f;
                }
            }
            else
            {
                if (Ability.animationCooldown <= 0.4f)
                {
                    Ability.animationCooldown = 0.4f;
                    // Ability.globalCooldown = 0.05f;

                }
            }

        }


        public IEnumerator removePyramid()
        {
            afterpyramid = true;

            yield return new WaitForSeconds(1f);
            pyramid = true;
            yield return new WaitForSeconds(2.5f);
            pyramid = false;

            yield return new WaitForSeconds(15f);
            afterpyramid = false;
        }

        /*
        public void BowReAim()
        {
            oldCamTransform = curCamTransform.position;
            oldCamRotation = curCamTransform.rotation;
            jumpCamTransform = curCamTransform.position;
        }

        public void BowEvent()
        {
            GetComponent<AE_BowString>().InHand = true;

            //GENIUS MOVE TO RIVEN AA STYLE
            // yield return new WaitForSeconds(0.2f);
            if (Ability.globalCooldown <= 0.6f)
            {

                Ability.globalCooldown = 0.6f;
            }
            anim.ResetTrigger("basicAttack");
            anim.ResetTrigger("basicAttackx2");
            anim.ResetTrigger("rapidFire");
            aus[0].Play();


        }

        public void stopBowEvent()
        {
            if (GetComponent<MarcoMovementController>().jumptimer > 0 && Gun.offsetcamera > 7)
            {

                Instantiate(effect[1], new Vector3(curCamTransform.position.x, jumpCamTransform.y, curCamTransform.position.z), oldCamRotation);
            }
            else
            {
                Instantiate(effect[1], curCamTransform.position, oldCamRotation);

            }
            aus[1].Play();

            GetComponent<AE_BowString>().InHand = false; ;


        }
        */
    }
}