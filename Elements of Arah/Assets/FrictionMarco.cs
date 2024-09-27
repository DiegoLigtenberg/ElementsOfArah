using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;
using TMPro;

namespace CreatingCharacters.Abilities
{
    public class FrictionMarco : Ability
    {

        public Animator anim;
        public Animator animboss;
        public GameObject[] effect;
        public Transform[] effectTransform;
        public Transform curCamTransform;
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;

        private ThirdPersonMovement thirdPersonPlayer;
        public AudioSource[] aus;

        public static int rapidFireHits;
        public bool isFiring;

        public float first_hit_timer;

        public Image abilityImage;   //the hidden image in canvas
        public GameObject textobjectcd;
        public GameObject nomana;

        public static int friction_stacks;
        public static bool friction_active;
        public static int stored_friction_stacks;

        public TMP_Text textUnleash;
        public float timer;
        private void Awake()
        {
            abilityImage.fillAmount = 0;
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
            rapidFireHits = 0;
            friction_active = false;
            friction_stacks = 0;
            stored_friction_stacks = 0;
            isInFrictionTimer = 0f;

            abilityKey = InputManager.instance.getKeyCode("friction");
            //friction_stacks = 30;
        }

        public IEnumerator remove_firing()
        {
            yield return new WaitForSeconds(0.3f);
            isFiring = false;
        }


        private float isInFrictionTimer;
        private CapsuleCollider playerCollider;
        private CapsuleCollider frictionOverlapCollider;

        public void ResetStacks()
        {
            friction_stacks = 0;
        }

        bool hasLeftFriction = false;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();
            //Debug.Log(timer);
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                //Debug.Log(timer);
            }

            if (isInFrictionTimer > 0f)
            {
                isInFrictionTimer -= Time.deltaTime;
            }

            if (isInFrictionTimer > 0)
            {
                // Initialize playerCollider if it is null
                if (playerCollider == null)
                {
                    playerCollider = ActivePlayerManager.ActivePlayerGameObj.GetComponentInChildren<CapsuleCollider>();
                }

                // Initialize frictionOverlapCollider if it is null
                if (frictionOverlapCollider == null)
                {
                    GameObject frictionOverlapObj = GameObject.Find("FrictionOverLap");
                    if (frictionOverlapObj != null)
                    {
                        frictionOverlapCollider = frictionOverlapObj.GetComponent<CapsuleCollider>();
                    }
                }

                // Check for overlap between the two CapsuleColliders
                friction_active = playerCollider && frictionOverlapCollider &&
                                  playerCollider.bounds.Intersects(frictionOverlapCollider.bounds);


                // Check for overlap between the two CapsuleColliders -> the intersection logic is to reset elemental purple dmg after an ability when its in ult
                bool isIntersecting = playerCollider && frictionOverlapCollider &&
                                      playerCollider.bounds.Intersects(frictionOverlapCollider.bounds);
                if (isIntersecting)
                {
                    hasLeftFriction = false;
                }
                else
                {
                    // If the player has left the friction area and hasn't already triggered the cooldown reset
                    if (!hasLeftFriction)
                    {
                        CooldownHandler.casted = 0; // this removes 'just used ability extra hit' when leaving ult after doing ability in ult -> because in ult, we dont want elemental dmg, we only get stacks in ult
                        hasLeftFriction = true; // Prevents triggering multiple times
                    }
                }
            }
            else
            {
                friction_active = false;
                playerCollider = null;
                frictionOverlapCollider = null;
                hasLeftFriction = false; // Reset flag if friction timer ends
            }


            if (Ability.energy < 75 && AbilityCooldownLeft <= 0)
            {
                nomana.SetActive(true);
            }
            else
            {
                nomana.SetActive(false);
            }


            getdmg = AbilityDamage;

            if (!GetComponent<ChargeShotMarco>().activated_during_friction)
            {
                if (friction_stacks > 50) { friction_stacks = 50; }
            }
            else
            {
                if (friction_stacks > 50) { friction_stacks = 50; }
                // can go above cap of 50!
            }
            

        }


        public override void Cast()
        {

            latecast = true;
            stored_friction_stacks = 0;
            //no double cast
            if (Ability.globalCooldown <= 0.8f)
            {
                Ability.globalCooldown = 0.8f;
            }

            StartCoroutine(CrownOfFire());
        }



        private void CooldownData()
        {
            textUnleash.text = FrictionMarco.friction_stacks.ToString();

            // If the ability is cast and off cooldown, reset the ability cooldown
            if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0 && latecast || latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
                abilityCooldownLeft = AbilityCooldown;  // Set the cooldown time to the full ability cooldown
            }

            // If the ability is on cooldown
            if (abilityCooldownLeft > 0)
            {
                textobjectcd.SetActive(true);

                // Update the fill amount based on remaining cooldown
                abilityImage.fillAmount = abilityCooldownLeft / AbilityCooldown;

                // Clamp abilityImage fill to zero to avoid negative values
                if (abilityCooldownLeft <= 0)
                {
                    abilityCooldownLeft = 0;
                    abilityImage.fillAmount = 0;
                }
            }
            else
            {
                textobjectcd.SetActive(false);
            }
        }


        public IEnumerator CrownOfFire()
        {


            yield return new WaitForSeconds(0.001f);

            if (Ability.globalCooldown <= 0.05f)
            {
                Ability.globalCooldown = 0.05f;
            }
            anim.SetTrigger("frictionShot");

            Instantiate(effect[0], effectTransform[0].position - new Vector3(0, 1.7f, 0), Quaternion.identity);

           // friction_active = true;
            isInFrictionTimer = 13.5f;
            //timer = 13f;

            if (Ability.globalCooldown <= 1.0f) 
            { 
                Ability.globalCooldown = 1.0f;
            }

            if (Ability.animationCooldown <= 1.6f)
            {
                Ability.animationCooldown = 1.6f;
            }

            yield return new WaitForSeconds(13.0f);
            //friction_active = false;
            isInFrictionTimer = 0f;
            

            yield return new WaitForSeconds(1f); // add one second delay such that long hits can still travel towards target and don't lose their dmg
            if (GetComponent<ChargeShotMarco>().activated_during_friction)
            {
                GetComponent<ChargeShotMarco>().activated_during_friction = false;
                friction_stacks = 0;
                //friction_stacks += stored_friction_stacks;
                yield return new WaitForSeconds(0f);
                stored_friction_stacks = 0;
            }
            
        }
    }
}