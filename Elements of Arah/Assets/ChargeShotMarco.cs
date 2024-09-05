using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;
using System;

namespace CreatingCharacters.Abilities
{
    public class ChargeShotMarco : Ability
    {

        public Animator anim;
        public Animator animboss;
        public GameObject[] effect;
        public Transform[] effectTransform;
        public Transform curCamTransform;
        private Quaternion oldCamRotation;
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;
        private bool pyramid; //fixes p1 bug when pyramid comes up
                              // Start is called before the first frame update
        public AudioSource[] aus;
        public int enhanced_attack;

        public Image abilityImage;   //the hidden image in canvas
        public GameObject textobjectcd;
        public GameObject nomana;

        private bool afterpyramid;
        private int start_dmg;
        public static int preResetFrictionStacks;

        private void Start()
        {

            abilityImage.fillAmount = 0;
            start_dmg = AbilityDamage;
            activated_during_friction = false;

            abilityKey = InputManager.instance.getKeyCode("chargeshot");
            //FrictionMarco.friction_stacks = 20;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();


            if (Ability.energy < basicrequirement  && AbilityCooldownLeft <= 0)
            {
                nomana.SetActive(true);
            }
            else
            {
                nomana.SetActive(false);
            }

            if (CooldownHandler.casted > 0) { enhanced_attack = 2; }
            else { enhanced_attack = 1; }

            // Debug.Log(RapidFireMarco.rapidFireHits);
            //getdmg = AbilityDamage;

            if (animboss.GetBool("Phasing") && !pyramid && !afterpyramid)
            {
                StartCoroutine(removePyramid());
            }          
        }

        public bool activated_during_friction;
        public override void Cast()
        {
           
            latecast = true;       
            oldCamRotation = curCamTransform.rotation;

            StartCoroutine(AvatarMoveLocalPosUp.manual_root(1f));
            Ability.globalCooldown = 0.8f; // no double casting
            RapidFireMarco.rapidFireHits = 0; // fix bug later, hardforce

            StartCoroutine(chargeShotVisual());
            // Instantiate(effect[1], effectTransform[0].position, effectTransform[1].transform.rotation);

          
            if (FrictionMarco.friction_active && FrictionMarco.friction_stacks >=10)
            {
                activated_during_friction = true;
            }

        }  



        public IEnumerator chargeShotVisual() //this name can be freely changed , currently it is only the explosion animation -> not the dmg
        {
       

         

            yield return new WaitForSeconds(0.05f);

            if (Ability.animationCooldown <= 0.9f)
            {
                Ability.animationCooldown = 0.9f;
            }

            anim.SetTrigger("Explosion Shot");
            anim.SetBool("casted", true);

            yield return new WaitForSeconds(.02f);
            if (Ability.globalCooldown <= 0.8f)
            {
                Ability.globalCooldown = 0.8f;
            }

            // incentive is that when low on stacks, you definitely want to prioritize farming stacks with low rfc cooldown, high stacks = can do full arrow rain

            //als je in ult doet, dan kan je gratis veel dmg doen maar verlies je wel stacks
            if (activated_during_friction && FrictionMarco.friction_stacks >= 10)
            {

                //getdmg =  (int)((start_dmg * Mathf.Pow(1.065f, (float)FrictionMarco.friction_stacks)) / 1.5f) ;
                //FrictionMarco.friction_stacks /= 2;
                //float poww = Mathf.Pow((500f - 0 * 75f) / 75f, (float)(1f / 50f));
               // int added_dmg =  (int) Math.Ceiling( (start_dmg *  Mathf.Pow(poww,(float) FrictionMarco.friction_stacks)));
                //getdmg = (int)((start_dmg * Mathf.Pow(1.065f, (float)FrictionMarco.friction_stacks)) / 1.5f);
               // getdmg = start_dmg + added_dmg;

                getdmg =  FrictionMarco.friction_stacks * 10;
                Instantiate(effect[0], effectTransform[3].position, effectTransform[0].rotation);
                preResetFrictionStacks = FrictionMarco.friction_stacks;
                FrictionMarco.friction_stacks -=10;
            }

            //als je uit ult doet, verlies je alle stacks, maar wellicht nodig voor specific moment om high priority target te killen OF execute (biggest burst) -
            // if this kills target, only half stacks are lost.
            /*
            else if (Input.GetKey(KeyCode.LeftShift) && FrictionMarco.friction_stacks > 10)
            {

                float poww = Mathf.Pow((500f - 0*75f)/75f, (float)(1f / 50f));
                Debug.Log(poww);
               // Debug.Log(Type
                //Debug.Log(start_dmg);
               // float added_dmg =  (int) Math.Ceiling( (start_dmg *  Mathf.Pow(poww,(float)60 - FrictionMarco.friction_stacks)));
                getdmg =  FrictionMarco.friction_stacks * 10;
               // getdmg = start_dmg + added_dmg;
                Instantiate(effect[0], effectTransform[3].position, effectTransform[0].rotation);
                
                preResetFrictionStacks = FrictionMarco.friction_stacks;
                
                FrictionMarco.friction_stacks = 0;
            }
            */

            else
            {
                getdmg = start_dmg;
            }
            // Instantiate(effect[0], effectTransform[3].position, effectTransform[0].rotation);
        }

        public void BowEventChargeShot()
        {
            GetComponent<AE_BowString>().InHand = true;

            //GENIUS MOVE TO RIVEN AA STYLE
            // yield return new WaitForSeconds(0.2f);
            if (Ability.globalCooldown <= 0.7f)
            {

                Ability.globalCooldown = 0.7f;
            }
            anim.ResetTrigger("basicAttack");
            anim.ResetTrigger("rapidFire");
            anim.ResetTrigger("isJumping");
            aus[0].Play();
        }

        // bow event that calls ChargeShot (damage of this ability)
        public void stopBowEventChargeShot()
        {
            StartCoroutine(ChargeShot());
        }

        public IEnumerator ChargeShot() //this is the animation based dmg hit -> don't change name
        {
            yield return new WaitForSeconds(0.25f);

            Debug.Log(Ability.animationCooldown);
            if (Ability.animationCooldown <= 0.7f)
            {
                Ability.animationCooldown = 0.37f; //.17f for moving after q -> .37f for not moving after q  (when basic attack next)
            }
    
            Instantiate(effect[enhanced_attack], curCamTransform.position, oldCamRotation);            
            aus[1].Play();
            GetComponent<AE_BowString>().InHand = false; ;
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

        private void CooldownData()
        {
            if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0 && latecast || latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
            }

            if (abilityCooldownLeft != 0)
            {
                textobjectcd.SetActive(true);
                abilityImage.fillAmount = abilityCooldownLeft/ AbilityCooldown;
                if (AbilityCooldownLeft <= 0.03f)
                {
                    abilityImage.fillAmount = 0;
                }
            }
            else
            {
                textobjectcd.SetActive(false);
            }
        }
    }
}