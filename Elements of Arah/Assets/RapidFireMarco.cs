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
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;
        private bool pyramid; //fixes p1 bug when pyramid comes up
                              // Start is called before the first frame update
        public AudioSource[] aus;
        private bool afterpyramid;

        public static int rapidFireHits;
        public static int rapidFireHitsDMG;//copy of rapidFireHits but will act for dmg purposes
        public bool isFiring;
        public static bool isFiring_mana; //works also when mana is low
        public static bool RapidFire_is_Channeling;

        public float first_hit_timer;

        public Image abilityImage;   //the hidden image in canvas
        public GameObject textobjectcd;
        public GameObject nomana;

        [HideInInspector] public bool onlyoncerfc;
        private float minimum_active; //removes bug that you aa + rapid fire, but loose ability key too fast resulting in no 2 shot rapid fire 
        private float minimum_active_dmg; // works so that aa dmg can scale up properly aa + rfc restting with basic attack


        private void Awake()
        {
            abilityImage.fillAmount = 0;
           // abilityType = 1;
            rapidFireHits = 0;

            RapidFire_is_Channeling = false;
        }
        private bool remove_anim;

        //this function works if you manually let loose of rapid fire ability key, in basic attack 'bow event' function -> there is rapidfire active bool set to false, for visual clarity at low mana quit without leaving abil key
        public IEnumerator remove_firing()
        {
            isFiring = false;
            var old_rf_hits = rapidFireHits;
            if (old_rf_hits <= 2) { Ability.animationCooldown = 1.3f; yield return new WaitForSeconds(0.3f); remove_anim = true; } // double hit
            else { Ability.animationCooldown = 0.4f; yield return new WaitForSeconds(0.2f); remove_anim = true; } // 3 or more hits
            yield return new WaitForEndOfFrame();
            rapidFireHits = -1;
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

            RapidFire_is_Channeling = isFiring;
            if (Ability.energy > 10f) { isFiring_mana = isFiring; }
            else { isFiring_mana = false; }

            if (minimum_active_dmg>0.5f) { rapidFireHitsDMG = 1; }
            else if (minimum_active_dmg  <0.5f && minimum_active_dmg > 0) { rapidFireHitsDMG = 2; }
            else { rapidFireHitsDMG = rapidFireHits; }

            // resets animation_cooldown to zero when ending rapid fire hits 
            reset_anim_to_zero();

            //2 bug fixes see variable declaration for details
            if (minimum_active > 0) { minimum_active -= Time.deltaTime; }
            if (minimum_active_dmg > 0) { minimum_active_dmg -= Time.deltaTime; }

            if (Ability.energy < thresholdrequirement && AbilityCooldownLeft <= 0)
            {
                nomana.SetActive(true);
            }
            else
            {
                nomana.SetActive(false);
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
                if (Input.GetKey(abilityKey) && energy >= 7.5f || first_hit_timer>0f || minimum_active >=0)
                {
                    if (!onlyoncerfc) { anim.SetBool("rapidFireActive", true); onlyoncerfc = true; }
                  
                    if (Ability.globalCooldown <= 0.6f)
                    {
                        Ability.globalCooldown = 0.6f;

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
                        Ability.globalCooldown = 0.2f;

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


        public override void Cast()
        {
            minimum_active = 0.5f; // works so that ability doesn't stop when quickly releasing ability key
            minimum_active_dmg = 0.9f; // works so that aa dmg can scale up properly aa + rfc restting with basic attack

            latecast = true;
            rapidFireHits = 1;

            isFiring = true;

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
                abilityImage.fillAmount -= 1 / AbilityCooldown * Time.deltaTime;
                if (abilityImage.fillAmount <= 0.05f)
                {
                    abilityImage.fillAmount = 0;
                }
            }
            else
            {
                textobjectcd.SetActive(false);
            }
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