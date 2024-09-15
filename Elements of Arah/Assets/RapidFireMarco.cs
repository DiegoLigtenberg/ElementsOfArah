using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;
using System.Linq;

namespace CreatingCharacters.Abilities
{
    public class RapidFireMarco : Ability
    {

        public Animator anim;
        public Animator animboss;
        public GameObject[] effect;
        public Transform[] effectTransform;
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;
        private bool pyramid; //fixes p1 bug when pyramid comes up
                              // Start is called before the first frame update
        public AudioSource[] aus;
        private bool afterpyramid;

        public static int rapidFireHits; //this variable does the logic
        public static int rapidFireHitsDMG;//copy of rapidFireHits but will act for dmg purposes
        public bool isFiring;
        public static bool isFiring_BowEffect; //works also when mana is low

        public float first_hit_timer;

        public Image abilityImage;   //the hidden image in canvas
        public GameObject textobjectcd;
        public GameObject nomana;
        private bool manaactive;

        [HideInInspector] public bool onlyoncerfc;
        private float minimum_active; //removes bug that you aa + rapid fire, but loose ability key too fast resulting in no 2 shot rapid fire 
        private float minimum_active_dmg; // works so that aa dmg can scale up properly aa + rfc restting with basic attack
        [HideInInspector] public float minimum_active_time_ability;
        public static bool TRUE_CHANNEL;

        public Image abil_img;
        public Image img;

        public GameObject violated_ui;
        public static bool TRUE_CHANNEL_HOTFIX; // fixes animation after letting loose of rapid fire early

        private bool once;
        private void Awake()
        {
            abilityImage.fillAmount = 0;
           // abilityType = 1;
            rapidFireHits = 0;
            TRUE_CHANNEL = false;
            isFiring_BowEffect = false;
            rapidFireHitsDMG = 0;
            TRUE_CHANNEL_HOTFIX = false;
            abilityKey = InputManager.instance.getKeyCode("rapidfire");

        }
        private bool remove_anim;

        private bool getAbilityConditions()
        {
            //if at least 1 condition isn't met, then there is a violation thus this function should return false
            bool condition_1 = (FrictionMarco.friction_stacks >= 0 || FrictionMarco.friction_active) ; // friction stacks moet groter dan tien zijn wanneeer je niet ult om rapid fire te knne doen
            bool[] conditions = { condition_1, };
            return !conditions.All(x => x); //  not all <=> at least one not -> we return true if at least 1 condition is not met, aka violated
        }

        //this function works if you manually let loose of rapid fire ability key, in basic attack 'bow event' function -> there is rapidfire active bool set to false, for visual clarity at low mana quit without leaving abil key
        public IEnumerator remove_firing()
        {
            if (!onlyoncerfc && isFiring)
            {
                isFiring_Hotfix = false;
                TRUE_CHANNEL_HOTFIX = false;
                var old_rf_hits = rapidFireHits;
                if (old_rf_hits <= 2) { Ability.animationCooldown = 0.3f; yield return new WaitForSeconds(0.3f); remove_anim = true; } // double hit
                else { Ability.animationCooldown = 0.05f; yield return new WaitForSeconds(0.2f); remove_anim = true; } // 3 or more hits
                yield return new WaitForEndOfFrame();

                yield return new WaitForSeconds(0.05f);
                isFiring = false;
                rapidFireHits = -1;

                fire_once = false;
            }

        }

        public void reset_anim_to_zero()
        {
            if (Ability.animationCooldown <= 1.6f && remove_anim && !AvatarMoveLocalPosUp.isRooted)
            {
                Ability.animationCooldown = 0f;
                remove_anim = false;
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();

            abilityConditionsViolated = getAbilityConditions();


            if (Ability.energy > 10f) { isFiring_BowEffect = isFiring; }
            else { isFiring_BowEffect = false; }

           // if (minimum_active_dmg>0.5f) { rapidFireHitsDMG = 1; }
            //else if (minimum_active_dmg  <0.5f && minimum_active_dmg > 0) { rapidFireHitsDMG = 2; } // min_active not needed for dmg calculations
            //else
            { rapidFireHitsDMG = rapidFireHits; }

            if (isFiring || minimum_active_time_ability > 0) { TRUE_CHANNEL = true; }
            else { TRUE_CHANNEL = false; }
            //added for hotfix
            if (isFiring_Hotfix || minimum_active_time_ability > 0) { TRUE_CHANNEL_HOTFIX = true; }
            else { TRUE_CHANNEL_HOTFIX = false; }

            // resets animation_cooldown to zero when ending rapid fire hits 
            reset_anim_to_zero();

            //2 bug fixes see variable declaration for details
            if (minimum_active > 0) { minimum_active -= Time.deltaTime; }
            if (minimum_active_dmg > 0) { minimum_active_dmg -= Time.deltaTime; }
            if (minimum_active_time_ability > 0) { minimum_active_time_ability -= Time.deltaTime; }

            //Debug.Log(isFiring);

            if (Ability.energy < thresholdrequirement && AbilityCooldownLeft <= 0)
            {
                nomana.SetActive(true);
                manaactive = true;
            }
            else
            {
                manaactive = false;
                nomana.SetActive(false);
                violated_ui.SetActive(false);
            }
            //outrange AND from cd
            if (abilityConditionsViolated)
            {
                if (manaactive == false)
                {
                    violated_ui.SetActive(true);
                }
            }
            else
            {
                violated_ui.SetActive(false);
            }

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
                if (Input.GetKey(abilityKey) && energy >= 7.5f && (GetComponent<BasicAttackMarco>().remaining_enhanced_rfc_hits >=1||FrictionMarco.friction_active)  || first_hit_timer>0f || minimum_active >=0)
                {
                    if (!onlyoncerfc && minimum_active>0) { anim.SetBool("rapidFireActive", true); onlyoncerfc = true;  }
                  
                    if (Ability.globalCooldown <= 0.9f)
                    {
                        Ability.globalCooldown = 0.6f; // was 1.6 pre ability gcd add to regular recast ("casted")

                    }
                    else
                    {

                    }
                        
                    
                    if (Ability.animationCooldown <= 1.0f)
                    {
                        Ability.animationCooldown = 0.25f;
                    }
                    once = false;
                }
          
                else
                {

                    if (Ability.globalCooldown <= 0.9f && !once)
                    {
                        once = true;
                        Ability.globalCooldown = 0.6f; // magic fix 

                    }
                   
                    if (Ability.animationCooldown <= 1.0f)
                    {
                        Ability.animationCooldown = 0.25f;
                    }
                    anim.SetBool("rapidFireActive", false);
                    

                    if (rapidFireHits != 0)
                    {
               
                        anim.ResetTrigger("basicAttack");
                        anim.ResetTrigger("rapidFire");
                        GetComponent<AE_BowString>().InHand = false;
                        //remove_anim = true;
                        StartCoroutine(remove_firing());
                      
                    }
                    onlyoncerfc = false;
                }

            }
     
        }

        [HideInInspector] public bool fire_once;
        public bool isFiring_Hotfix;
      
        public override void Cast()
        {
            minimum_active = 0.5f; // works so that ability doesn't stop when quickly releasing ability key
            minimum_active_dmg = 0.9f; // works so that aa dmg can scale up properly aa + rfc restting with basic attack

            minimum_active_time_ability = 1.5f; 

            anim.SetBool("bug_rfc", false);
            if (Ability.globalCooldown < 0.8f)
            {
                Ability.globalCooldown = 0.8f;
            }
            

            GetComponent<BasicAttackMarco>().remaining_enhanced_rfc_hits = FrictionMarco.friction_stacks/10; //GetComponent<DashAbilityMarco>().remainingDashes;


            latecast = true;
            rapidFireHits = 1;

            if (!fire_once) { isFiring = true; fire_once = true; isFiring_Hotfix = true; }

 

            GetComponent<BasicAttackMarco>().update_oldcam_rotation(); //fixes that you don't fail aim when rfc is first ability used.
            StartCoroutine(RFbasicAttack());

            // instead of float 0.6 first_hit timer = > makes it so that also when delaying rfc after aa -> still only 2 shots when letting loose early -> still wait minimum of 0.4 sec for bug fix
            first_hit_timer = Mathf.Min(Mathf.Max(0.9f - ActivePlayerManager.ActivePlayerGameObj.GetComponent<BasicAttackMarco>().AbilityCooldownLeft,0.43f), 0.6f); 
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
                abilityImage.fillAmount =  abilityCooldownLeft/ AbilityCooldown ;
                if (abilityCooldownLeft <= 0.03f)
                {
                    abilityImage.fillAmount = 0;
                }
            }
            else
            {
                textobjectcd.SetActive(false);
            }
        }

        public void Update_AbilityUI_CD_Reductione(float reduction_value)
        {
            //Debug.Log(AbilityCooldown);
            //Debug.Log(abilityCooldownLeft);
            //Debug.Log(reduction_value);
            // Debug.Log((AbilityCooldown - (abilityCooldownLeft - reduction_value)) / AbilityCooldown);
            textobjectcd.SetActive(true);

            //abilityImage.fillAmount -=   1- ((AbilityCooldown - (abilityCooldownLeft - reduction_value))/AbilityCooldown);
            AbilityCooldownLeft = reduction_value;
           
  
        }

        public IEnumerator RFbasicAttack()
        {
            if (Ability.globalCooldown <= 0.05f)
            {
               Ability.globalCooldown = 0.05f;
            }
      
            anim.SetTrigger("rapidFire");
 
            anim.SetBool("casted", true);

            yield return new WaitForSeconds(0.001f);


            Instantiate(effect[0], effectTransform[0].position, Quaternion.identity);

            if (Ability.globalCooldown <= 0.8f)
            {
                Ability.globalCooldown = 0.8f;
            }

            if (Ability.animationCooldown <= 0.4f)
            {
                Ability.animationCooldown = 0.4f;
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
    }
}