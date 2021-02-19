using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using CreatingCharacters.Player;
using UnityEngine.UI;

namespace CreatingCharacters.Abilities
{


    public class BeamAbility : Ability
    {
        public Animator anim;
        public GameObject[] effect;
        public Transform[] effectTransform;

        public Image abilityImage;  //the hidden image in canvas

        private bool usingBeam = false;
        public bool usingBeamP = false; //beamP is zowel voor basic attack als voor animation delay nu!
        public bool usingbeamF = false; //voor furious hit

        float lastStep, timeBetweenSteps = 0.2f;
        float lastStep2, timeBetweenSteps2 = 0.167f; //3 ticks!
        float timer;

        private bool latecast;
        public float setTickCooldown;

        public GameObject nomana;

        public GameObject textobjectcd;
        [HideInInspector] public float textcdleft;

        private void Awake()
        {
            abilityType = 3; // trheshold

          
            abilityImage.fillAmount = 0;

            abilityKey = InputManager.instance.getKeyCode("beam");
            cancelvar = true;
           
        }

        public bool canceldash;

        private void CooldownData()
        {
            //text that shows when ability from cd
            textcdleft = abilityCooldownLeft;
          
            

           // if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0)
           if (latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
            }

            if (abilityCooldownLeft != 0)  
            {
                textobjectcd.SetActive(true);
                abilityImage.fillAmount -= 1 / AbilityCooldown * Time.deltaTime;
                if (abilityImage.fillAmount <= 0)
                {
                    abilityImage.fillAmount = 0;                 
                }
            }
            else
            {
                textobjectcd.SetActive(false);
            }
        }

    
        private void queuedbutcanceled()
        {
            //queued cast cancel

            if (!Input.GetKey(abilityKey))
            {

                
                    usingBeam = false;
                    usingBeamP = false;
                    cancelvar = false;
                    canceldash = false;

                    Ability.animationCooldown = 0f;
                    Ability.globalCooldown = 0f;
                
            }

        }

        protected override void Update()
        {
            base.Update();

            //info about cooldown
            CooldownData();

            timer += Time.deltaTime;


            if ((Input.GetKeyUp(abilityKey) || canceldash ) && !AvatarMoveLocalPosUp.isRooted )
            {
                if (usingBeam)
                {
                    Ability.animationCooldown = 0f;
                    Ability.globalCooldown = 0f;
                }
         
                usingBeam = false;
                usingBeamP = false;
                cancelvar = false;
                canceldash = false;
             
            }


            
            if (usingBeam && cancelvar)
            {
          
                //visual
                Instantiate(effect[2], effectTransform[2].position, effectTransform[2].rotation);

           
                if (timer- lastStep > timeBetweenSteps)
                {
                    lastStep = timer;
                            
                    Instantiate(effect[3], effectTransform[3].position, effectTransform[3].rotation);
                }
                if (timer - lastStep2 > timeBetweenSteps2)
                {
                    lastStep2 = timer;
                    Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
               
                }

            }

            if (Ability.energy < 70 && abilityCooldownLeft <= 0f)
            {
                nomana.SetActive(true);
            }
            else
            {
                nomana.SetActive(false);
            }

            if (!usingBeamP)
            {
                anim.SetBool("isBeaming", false);
            }
        }

        public override void Cast()
        {

                if (DashAbility.Beamready > 0)
            {
                StartCoroutine(RecastBeam());
            
            }

            else
            {
                latecast = true;
                tickCooldown = setTickCooldown;
                anim.SetInteger("skillNumber", 3);
                anim.SetTrigger("playSkill");
         
          
                StartCoroutine(castBeam());

                //StartCoroutine(GetComponent<CooldownReducer>().ShortBuff(abilityType));

            }
            
        }

    

        private bool cancelvar;

        public virtual IEnumerator castBeam()
        {
        
            yield return new WaitForSeconds(0.002f); // 02 want dit cast later
            Ability.animationCooldown = 2.26f;
            Ability.globalCooldown = 2.25f;
            anim.SetBool("isBeaming", true);
            usingBeamP = true;
            cancelvar = true;
            usingbeamF = true;

            yield return new WaitForSeconds(0.5f);
            usingBeam = true;

            //queued abil but canceled 
            queuedbutcanceled();
            if (cancelvar)
            {
                Instantiate(effect[4], effectTransform[4].position, effectTransform[4].rotation);
            }
      
            yield return new WaitForSeconds(1.5f);

            //can cast aa and other things
            cancelvar = false;
            usingBeamP = false;
            yield return new WaitForSeconds(0.25f);
            usingbeamF = false;
            yield return new WaitForSeconds(0.01f);
            usingBeam = false;
            
          
           

            yield return null;
        }

        //for delay when dashing (can only cast when arriving
        public IEnumerator RecastBeam()
        {
            yield return new WaitForSeconds(DashAbility.Beamready);
            Cast();
        }

    
    }
}