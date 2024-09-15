using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class BasicAttackMarco : Ability
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
        public AudioSource[] aus;
        public int enhanced_attack;
        private bool no_sound;
        private DashAbilityMarco dam;
        public int remaining_enhanced_rfc_hits;

        private RapidFireMarco rfm;

        public Color startcolor;
        public Color endcolor;
        public Color endcolormana;
        private Color tempcollor;
        public Color darkcolor;
        public Color lightcolor;
        public float time_elapsed;
        private float lerp_duration;

        private void Awake()
        {
            abilityImage.fillAmount = 0;
            abilityType = 1;
            dam = GetComponent<DashAbilityMarco>();
            rfm = GetComponent<RapidFireMarco>();
            lerp_duration = 0.22f;
            rfc_enhanced = 1;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();

           // Debug.Log(RapidFireMarco.rapidFireHits);
            getdmg = AbilityDamage;

            if (animboss.GetBool("Phasing") && !pyramid && !afterpyramid)
            {
                StartCoroutine(removePyramid());
            }   

            if (highlight_timer > 0) { highlight_timer -= Time.deltaTime; }
            if (lerping && onlylerponce)
            {
                highlight_color();
            }
        }

        public void update_oldcam_rotation()
        {
            oldCamRotation = curCamTransform.rotation;
        }

        public override void Cast()
        {
            latecast = true;
            no_sound = false;
            oldCamTransform = curCamTransform.position;
            oldCamRotation = curCamTransform.rotation;

            RapidFireMarco.rapidFireHits = 0; // fix bug later, hardforce
            StartCoroutine(basicAttack());
            // Instantiate(effect[1], effectTransform[0].position, effectTransform[1].transform.rotation);
        }

        private bool afterpyramid;

        private void CooldownData()
        {
            if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0 && latecast || latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
            }

            //if doing max(abilcd + gcd, then get more accurate aa cooldown, but kinda confusing and inconsistent aswell
            if (abilityCooldownLeft != 0) // Mathf.Max(AbilityCooldown, globalCooldown)
            {
                abilityImage.fillAmount = abilityCooldownLeft / AbilityCooldown; // Mathf.Max( AbilityCooldown,globalCooldown)
                if (abilityCooldownLeft <= 0.03f)
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
        
            anim.SetTrigger("basicAttack");
            anim.SetBool("casted", true);

            yield return new WaitForSeconds(0.001f); 
       
            if (Ability.animationCooldown <= 0.4f)
            {
                Ability.animationCooldown = 0.5f;
                // Ability.globalCooldown = 0.05f;
            }            

            //GENIUS MOVE TO RIVEN AA STYLE
            yield return new WaitForSeconds(0.3f);
            if (Ability.globalCooldown <= 0.6f)
            {
                Ability.globalCooldown = 0.6f;
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


        public void BowReAim()
        {
                oldCamTransform = curCamTransform.position;
                oldCamRotation = curCamTransform.rotation;
                jumpCamTransform = curCamTransform.position;
            
        }

        //function runs at begin of bow animation
        public void StopRapidFire()
        {
            rfm.minimum_active_time_ability= 0.45f;
        }
        public void BowEvent()
        {
        
            GetComponent<AE_BowString>().InHand = true;
     

            if (Ability.animationCooldown <= 0.2f)
            {
              //  Ability.animationCooldown = 0.2f;
                // Ability.globalCooldown = 0.05f;

            }

            anim.ResetTrigger("basicAttack");
            anim.ResetTrigger("rapidFire");
            anim.ResetTrigger("isJumping");
        
            if (! no_sound ) { aus[0].Play(); }
        }

        public void removeBowString()
        {
            GetComponent<AE_BowString>().InHand = false;   
        }

        private bool lerping;
        private float highlight_timer;

        public void highlight_color()
        {
            if (highlight_timer < 0.025f)
            {
               
                rfm.img.color = startcolor;
                time_elapsed = 0;
                rfm.abil_img.color = lightcolor;

            }
            else
            {
                rfm.img.color = Color.Lerp(startcolor, endcolor, time_elapsed / lerp_duration); 
                time_elapsed += Time.deltaTime; 
                tempcollor = rfm.img.color;
            }
        }

        private bool onlylerponce;
        private bool reocur;  // help variabele voor ui white flash
        private bool finished; // dient voor niet resetten van ui white flash bij meerdere rfc hits
        public IEnumerator potential_reset(int remaining_hits)
        {

            onlylerponce = true;

            int index = RapidFireMarco.rapidFireHitsDMG-1; //  1 * ((int)FrictionMarco.friction_stacks / 10);

            yield return new WaitForSeconds(.69f);

            if (!RapidFireMarco.TRUE_CHANNEL)
            {

                Debug.Log("hits left bug");
                Debug.Log(remaining_hits);
                if (remaining_hits > 0)
                {
                    //0,9 * (5,7,9,11,13, ..., 15, 17, 19, 21) + 0.2f
                    //float[] cd_timers = { 4.7f, 6.5f, 8.3f, 10.1f, 11.9f,  //this is for the first 5 values
                    //13.7f, 15.5f, 17.3f, 19.1f }; //this is cooldown for when you do a full ultimate

                    float cd_time = (0.9f * ((index * 1.0f) + 3)) + 0.2f;

                    CooldownHandler.Instance.ReduceAbilityCooldownToValue(rfm, cd_time);
                    rfm.Update_AbilityUI_CD_Reductione(cd_time);
                    lerping = true;
                    //color start
                    rfm.img.color = startcolor;
                    rfm.abil_img.color = darkcolor;
                    highlight_timer = .32f;

                    //wait lerp
                    yield return new WaitForSeconds(.32f);



                    lerping = false;
                    onlylerponce = false;
                }

            }
            else
            {
                reocur = true;
                if (RapidFireMarco.TRUE_CHANNEL && !reocur)
                {
                    //Debug.Log("This loop can not happen multiple times");
                    yield return new WaitForSeconds(10f);
                    if (!finished)
                    {
                        reocur = false;
                    }
                }
                if (!reocur)
                {
                    finished = false;
                    reocur = true;
                    float cd_time = (0.9f * ((index * 1.0f) + 3)) + 0.2f;

                    CooldownHandler.Instance.ReduceAbilityCooldownToValue(rfm, cd_time);
                    rfm.Update_AbilityUI_CD_Reductione(cd_time);
                    lerping = true;
                    //color start
                    rfm.img.color = startcolor;
                    rfm.abil_img.color = darkcolor;
                    highlight_timer = .32f;

                    //wait lerp
                    yield return new WaitForSeconds(.32f);



                    lerping = false;
                    onlylerponce = false;
                    finished = true;
                    onlylerponce = false;
                }
            }
         
       

         
        }

        public void remove_mana_delay(int attack) //attack == 0 (aa), attack == 1 (rf)
        {
            //yield return new WaitForSeconds(0.25f);
            if ((RapidFireMarco.rapidFireHits >= 2 || RapidFireMarco.rapidFireHits == -1) && rfm.isFiring && rfm.first_hit_timer <= 0)
            {
                //7.5 caused problems based manual delay of aa and rapid fire cast -> dont want that
                energy = energy - 7.4f;
                StartCoroutine(AvatarMoveLocalPosUp.manual_root(0.4f));
            }

            //    if (RapidFireMarco.rapidFireHits == 1) { CooldownHandler.casted = 0; }

            if (CooldownHandler.casted > 0) { enhanced_attack = 2; }
            else { enhanced_attack = 1; }

            //here should be the logic for how many rapid fire hits should be enhanced -> BASED ON AVAILABLE DASHES!
            if (remaining_enhanced_rfc_hits > 0) { if (FrictionMarco.friction_active) { rfc_enhanced = 2; }; } //make some consume_enhanced_dash func
            else { rfc_enhanced = 1; }

            if (rfm.isFiring)
            {
                RapidFireMarco.rapidFireHits += 1;
            }

            if (attack == 1) { StartCoroutine(potential_reset(Mathf.Max(remaining_enhanced_rfc_hits,1))); } // Max 1 -> means so that on 0 stacks you also just have a 4 escond cd
            
       
        }

        //updates amount of basic attacks or rapid fire attacks after using an ability (s.t. aiblity =/= basic attack)
        private void consume_enhanced_attack(GameObject aa)
        {
            if (CooldownHandler.casted > 0)
            {
                CooldownHandler.casted -= 1;
                if (!FrictionMarco.friction_active && FrictionMarco.friction_stacks >0 )
                {
                    aa.GetComponentInChildren<AE_PhysicsMotion>().buff_next_basic_attack = true;

                    //if (FrictionMarco.friction_active)
                    {
                        FrictionMarco.friction_stacks -= 1;
                    }
                }
               
            }

       
        }

        private void consume_enhanced_rfc(GameObject eaa)
        {
            if (remaining_enhanced_rfc_hits > 0)
            {
                
                remaining_enhanced_rfc_hits -= 1;
                if (!FrictionMarco.friction_active)
                {

                    
                   // FrictionMarco.friction_stacks += 1;
                }
                else
                { 
                    //eaa.GetComponentInChildren<AE_PhysicsMotion>().buff_next_basic_attack = true;
                    FrictionMarco.friction_stacks += 4;
                    
                }
               
            }
       
        }

        private int rfc_enhanced;

        public void stopBowEvent(AnimationEvent animationEvent)
        {

            // logic for when ulting
            if (FrictionMarco.friction_active || GetComponent<ChargeShotMarco>().activated_during_friction) 
            { CooldownHandler.casted = 1; remaining_enhanced_rfc_hits = 1; }

            remove_mana_delay(animationEvent.intParameter);
        

            // attacks when not jumping OR when jumping and camera is not looking in air (then don't need to reposition arrow for jump position)
            if (GetComponent<MarcoMovementController>().jumptimer > 0 && Gun.offsetcamera > 7)
            {

                // basic attack
                if (animationEvent.intParameter == 0)
                {
                    GameObject aaj = Instantiate(effect[enhanced_attack], new Vector3(curCamTransform.position.x, jumpCamTransform.y, curCamTransform.position.z), oldCamRotation);
               
                    consume_enhanced_attack(aaj);
                }
                // rapid fire attack
                else
                {
                    BowReAim();
                    GameObject eaaj = Instantiate(effect[rfc_enhanced], new Vector3(curCamTransform.position.x, jumpCamTransform.y, curCamTransform.position.z), oldCamRotation);
                    consume_enhanced_rfc(eaaj);
                }                
            }
            // attacks when not jumping
            else
            {
                // basic attack
                if (animationEvent.intParameter == 0)
                {
                    GameObject aa = Instantiate(effect[enhanced_attack], curCamTransform.position, oldCamRotation);
                    consume_enhanced_attack(aa);
                }

                // rapid fire attack
                else
                {
                    BowReAim();
                    GameObject eaa = Instantiate(effect[rfc_enhanced], curCamTransform.position, oldCamRotation); //als we wel rfc firen -> al1 bonus met 3 stacks!
                    consume_enhanced_rfc(eaa);
                }
            }

            if (!(Ability.energy < 7.5 && no_sound)) { aus[1].Play(); }

            GetComponent<AE_BowString>().InHand = false;;


            if (energy < 7.5) { anim.SetBool("bug_rfc", true); if (Ability.animationCooldown <= 0.25) { Ability.animationCooldown = 0.15f; } }
            if (energy < 7.5f)
            {
                anim.SetBool("rapidFireActive", false);

                rfm.onlyoncerfc = false;

            }
            if (energy < 7.5f)
            {
                // no_sound = true;
                no_sound = true;
                rfm.isFiring = false;
                rfm.isFiring_Hotfix = false;


                RapidFireMarco.rapidFireHits = -1;
                rfm.fire_once = false;
                if (Ability.globalCooldown <= 0.6f)
                {
                    Ability.globalCooldown = 0.6f;
                }
                   
            
              
            }
            if (energy >= 7.5f)
            {
                //no_sound = false;
                no_sound = false;
                anim.SetBool("bug_rfc", false);
            }     
        }
    }
}