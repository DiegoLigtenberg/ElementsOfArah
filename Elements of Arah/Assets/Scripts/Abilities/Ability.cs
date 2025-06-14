﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreatingCharacters.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] private string abilityName = "New Ability Name";
        [SerializeField] private string abilityDescription = "New Ability Description";
        [SerializeField] private float abilityCooldown = 1f;
        [SerializeField] private int abilityDamage;
        [SerializeField] public int abilityType;

        [HideInInspector] protected float abilityCooldownLeft;
        [HideInInspector] protected bool abilityConditionsViolated;
        [SerializeField] public KeyCode abilityKey;

        public static float globalCooldown = 0f;
        public static float animationCooldown = 0f;
        public static float tickCooldown = 0f;

        public static bool channel_ability_active;

        public static float energy = 0f;


        public float AbilityCooldownLeft {get  { return abilityCooldownLeft; } set { abilityCooldownLeft = value; } }

        public bool AbilityConditionsViolated { get { return abilityConditionsViolated; } set { abilityConditionsViolated = value; } }

        public string AbilityName { get { return abilityName; } }

        public string AbilityDescription { get { return abilityDescription; } }

        //max cooldown
        public float AbilityCooldown { get { return abilityCooldown; } }

        public int AbilityDamage { get { return abilityDamage; } }

        

        // public int AbilityType { get { return abilityType; } }

            
        //prevent double cast
        [HideInInspector] public bool onlyonce;
        private bool alreadyglobal;

        private int basicAttackManaGain = 10;
        private int basicAbilManaCost = 30;
        private int thresholdAbilManaCost = 50;
        private int ultimateAbilManaCost = 90;

        [HideInInspector] public int basicrequirement = 30;
        [HideInInspector] public int thresholdrequirement = 70;
        [HideInInspector] public int ultimaterequirement = 90;

        private string furioushitabil = "Furious Hit";
        private string avalancheabil = "Avalanche";
        private string chargeshotabil = "Charge Shot Marco";


        private int abilityCountType;
        private bool manaproblem = false;

        private void OnEnable()
        {
            manaonce = true;
        }

        private void Start()
        {
            globalCooldown = 0;
            animationCooldown = 0;
            manaonce = true;
        }


        //abilities
        public abstract void Cast();

        public void check_if_channeling()
        {
            // list of all channeled abilities -> if one active, can't use recast for basic attack
            if (BeamAbility.Beam_is_channeling || RapidFireMarco.TRUE_CHANNEL_HOTFIX  ) {channel_ability_active = true; }
            else { channel_ability_active = false; }
        }


        protected virtual void Update()
        {
            //check how long it takes before ability is on cd
            abilityCooldownLeft = CooldownHandler.Instance.CooldownSeconds(this);

            check_if_channeling();


            //manage out of bound energy
            if (energy > 100) { energy = 100; }
            if (energy < 0) { energy = 0; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKeyDown(abilityKey) && !PauseMenu.GameIsPaused)
            {
                if (abilityConditionsViolated) { return; }

                //cast zonder gcd
                if (this.abilityType == 0)
                {
                    if (!AvatarMoveLocalPosUp.isRooted)
                    {
                        Cast();

                    }
                }
                Debug.Log(onlyonce);

                //kan niet casten want abil is nog op cooldown
                if (CooldownHandler.Instance.IsOnCooldown(this))

                {
                    // Debug.Log("this value should be lower than 0.3: " + CooldownHandler.alreadyCasting);
                    // Debug.Log(onlyonce);

                    //this is delay for auto ability queing
                    if (CooldownHandler.alreadyCasting < 0.6f && !onlyonce )
                    {
                        onlyonce = true;
                        Debug.Log(this.abilityName + " is on cd for " + CooldownHandler.alreadyCasting);
                        StartCoroutine(Recast());
                    }
                    else
                    {
                        //Debug.Log("WTF");
                        return;
                    }
                }

                //GLOBAL ABILITY Cooldown Que
                //tijd dat je niks kan doen door dash of channel beam -> geeft nu tijd voor nieuwe cast
                else if (globalCooldown > 0 && globalCooldown < 0.8f && !onlyonce)
                {
                    //    if (this.abilityName != "Basic attack")
                    if (true)  //!alreadyglobal)// &&!GameObject.Find("heraklios_a_dizon@Jumping (2)").GetComponent<DashAbility>().aabugActivate)
                    {
                        StartCoroutine(RecastGlobal());
                    }
                    else
                    {
                        Debug.Log("p1 buf");
                    }
                }


                //OWN ABILITY Cooldown Que
                //tijd dat je eigen ability niet kan doen door cooldown ability zelf.
                else if (globalCooldown <= 0 && !onlyonce) //&& !(this.AbilityName == "Dash Ability"))
                {

                    onlyonce = true;
                    //actually casting the ability
                    Debug.Log("casted double");
                    if (this.abilityType == 1)
                    {
                        Cast();
                        energy += basicAttackManaGain;
                        //na casten gaat die op cooldown
                        CooldownHandler.Instance.PutOnCooldown(this);
                        CooldownHandler.alreadyCasting = 1f;
                        onlyonce = false;
                        alreadyglobal = false;

                    }

                    if (this.abilityType == 2)
                    {
                        if (energy >= basicrequirement)
                        {
                            if (this.AbilityName == furioushitabil && GetComponent<FuriousHit>().cooldownFireBreath >= 0.3f || this.abilityName == chargeshotabil && !GetComponent<MarcoMovementController>().isGrounded)
                            {
                                onlyonce = false;
                                alreadyglobal = false;
                            }
                            else
                            {
                                Cast();
                                energy -= basicAbilManaCost;
                                //na casten gaat die op cooldown
                                CooldownHandler.Instance.PutOnCooldown(this);
                                CooldownHandler.alreadyCasting = 1f;
                                onlyonce = false;
                                alreadyglobal = false;
                            }
                        }

                        else
                        {
                            onlyonce = false;
                            alreadyglobal = false;
                            CooldownHandler.alreadyCasting = 1f;
                            // return;
                        }
                    }
                    if (this.abilityType == 3)
                    {
                        // implicatie
                        if (energy >= thresholdrequirement)
                        {
                            Cast();

                            energy -= thresholdAbilManaCost;
                            //na casten gaat die op cooldown
                            CooldownHandler.Instance.PutOnCooldown(this);
                            CooldownHandler.alreadyCasting = 1f;
                            onlyonce = false;
                            alreadyglobal = false;
                        }
                        else
                        {

                            onlyonce = false;
                            alreadyglobal = false;
                        }


                    }
                    if (this.abilityType == 4)
                    {

                        if (energy >= ultimaterequirement)
                        {
                            Cast();
                            energy -= ultimateAbilManaCost;
                            //na casten gaat die op cooldown
                            CooldownHandler.Instance.PutOnCooldown(this);
                            CooldownHandler.alreadyCasting = 1f;
                            onlyonce = false;
                            alreadyglobal = false;
                        }
                        else
                        {

                            onlyonce = false;
                            alreadyglobal = false;
                        }
                    }

                    if (!onlyonce)
                    {
                        // globalCooldown = 0.1f;
                    }
                }

            }
        }

        private bool manaonce = true;
        public IEnumerator manaproblemStop()
        {
            yield return new WaitForSeconds(1.6f);
            manaonce = true;
        }
        //global cooldown
        public IEnumerator RecastGlobal()
        {
            // alreadyglobal = true; //staat al bij begin
            onlyonce = true;
            alreadyglobal = true;
            Debug.Log("global cooldown = " + globalCooldown);
            Debug.Log(this.abilityName);

            //de + 0.01f is zodat je energy goed registered en  (anders is het te laag bij recasten!)
            yield return new WaitForSeconds(globalCooldown + 0.01f);
            Debug.Log("casted2");

            if (true)//!GameObject.Find("heraklios_a_dizon@Jumping (2)").GetComponent<DashAbility>().isactivated)
            {
                if (this.abilityType == 1)
                {

                    if (globalCooldown <= 0.3f)
                    {
                        //yield return new WaitForSeconds(globalCooldown + 0.1f);
                        Cast();
                        energy += basicAttackManaGain;
                        CooldownHandler.Instance.PutOnCooldown(this);
                        CooldownHandler.alreadyCasting = 1f;
                        //na casten gaat die op cooldown

                        onlyonce = false;
                        alreadyglobal = false;
                    }
                    else
                    {
                        //this makes it so that you don't gain mana if global cooldown isnt 0
                        RecastGlobal(); //this doesn't work intentionally
                        onlyonce = false;
                        alreadyglobal = false;
                    }

                    yield return null;
                }
                if (this.abilityType == 2)
                {

                    if (energy >= basicrequirement)
                    {
                        if (this.AbilityName == furioushitabil && GetComponent<FuriousHit>().cooldownFireBreath > 0.3f)
                        {
                            onlyonce = false;
                        }
                        else
                        {
                            Debug.Log("we got at recast global AND CASTED");
                            Cast();
                            energy -= basicAbilManaCost;
                            //na casten gaat die op cooldown
                            CooldownHandler.Instance.PutOnCooldown(this);
                            CooldownHandler.alreadyCasting = 1f;
                            onlyonce = false;
                            alreadyglobal = false;
                            yield return null;
                        }


                    }

                    else if (energy < basicrequirement)
                    {
                        yield return new WaitForSeconds(0.05f);
                        Debug.Log("too low energy " + energy);

                        if (energy >= basicrequirement - 10)
                        {

                            //yield return new WaitForSeconds(Mathf.Min(globalCooldown +0.1f,0.3f));


                            // if (manaonce)íf (energy >= basicAbilManaCost)
                            if (energy >= basicrequirement)
                            {

                                Cast();
                                //StartCoroutine(RecastGlobal());
                                energy -= basicAbilManaCost;
                                //na casten gaat die op cooldown
                                CooldownHandler.Instance.PutOnCooldown(this);
                                CooldownHandler.alreadyCasting = 1f;
                                onlyonce = false;
                                alreadyglobal = false;
                                yield return null;
                            }
                        }
                        onlyonce = false;
                        alreadyglobal = false;
                    }


                }
                if (this.abilityType == 3)
                {
                    // implicatie
                    if (energy >= thresholdrequirement)
                    {
                        Cast();
                        energy -= thresholdAbilManaCost;
                        //na casten gaat die op cooldown
                        CooldownHandler.Instance.PutOnCooldown(this);
                        CooldownHandler.alreadyCasting = 1f;
                        onlyonce = false;
                        alreadyglobal = false;
                        yield return null;
                    }
                    else
                    {
                        onlyonce = false;
                        alreadyglobal = false;
                    }
                }

                if (this.abilityType == 4)
                {

                    if (energy >= ultimaterequirement)
                    {
                        Cast();
                        energy -= ultimateAbilManaCost;
                        //na casten gaat die op cooldown
                        CooldownHandler.Instance.PutOnCooldown(this);
                        CooldownHandler.alreadyCasting = 1f;
                        onlyonce = false;
                        alreadyglobal = false;
                        yield return null;
                    }
                    else
                    {
                        onlyonce = false;
                        alreadyglobal = false;
                    }
                }



            }
            else
            {
                onlyonce = false;
                alreadyglobal = false;
                Debug.Log("bugg");
            }
            //  globalCooldown = 0.1f;

        }

        //own ability
        public IEnumerator Recast()
        {
            //de + 0.01f is zodat je energy goed registered en  (anders is het te laag bij recasten!)
            yield return new WaitForSeconds(CooldownHandler.alreadyCasting + 0.01f);
            if (channel_ability_active) // || GetComponent<BeamAbility>() == null)   // IF we would add this last commented line->marco bug
            {
                onlyonce = false;
                alreadyglobal = false;
                //  yield break;
            }
            Debug.Log(" this is probably also true -,- " + alreadyglobal);
            Debug.Log(channel_ability_active);
            Debug.Log(globalCooldown);
            //yield return new WaitForSeconds(globalCooldown + 0.01f);

            if (!alreadyglobal && (channel_ability_active == false))
            {

                if (globalCooldown > 0 && globalCooldown < 0.8f)
                {
                    //kan niet casten want abil is nog op cooldown
                    if (CooldownHandler.Instance.IsOnCooldown(this))

                    {
                        if (CooldownHandler.alreadyCasting < 0.4f)
                        {
                            // yield return new WaitForSeconds(CooldownHandler.alreadyCasting + .001f);
                            onlyonce = false;
                            alreadyglobal = false;
                            StartCoroutine(Recast());
                        }

                    }
                    else
                    {
                        //eigen abil maar ook global cooldown langer dan 0.8                      
                        yield return new WaitForSeconds(globalCooldown);

                        if (!alreadyglobal)
                        {
                            Debug.Log("casted3");


                            //casting ability
                            if (this.abilityType == 1)
                            {
                                Cast();
                                energy += basicAttackManaGain;
                                //na casten gaat die op cooldown
                                CooldownHandler.Instance.PutOnCooldown(this);
                                //globalCooldown = 0.1f;
                                CooldownHandler.alreadyCasting = 1f;
                                onlyonce = false;
                                alreadyglobal = false;
                                yield return null;
                            }
                            if (this.abilityType == 2)
                            {

                                if (energy >= basicrequirement)
                                {
                                    if (this.AbilityName == furioushitabil && GetComponent<FuriousHit>().cooldownFireBreath >= 0.4f ||this.abilityName == chargeshotabil && !GetComponent<MarcoMovementController>().isGrounded)
                                    {
                                        onlyonce = false;
                                        alreadyglobal = false;
                                    }
                                    else
                                    {
                                        Cast();
                                        energy -= basicAbilManaCost;
                                        //na casten gaat die op cooldown
                                        CooldownHandler.Instance.PutOnCooldown(this);
                                        CooldownHandler.alreadyCasting = 1f;
                                        onlyonce = false;
                                        alreadyglobal = false;
                                        yield return null;
                                    }
                                }

                                if (energy < basicrequirement)
                                {


                                    onlyonce = false;
                                    alreadyglobal = false;
                                }
                            }
                            if (this.abilityType == 3)
                            {
                                //implciatie
                                if (energy >= thresholdrequirement )
                                {
                                    Cast();
                                    energy -= thresholdAbilManaCost;
                                    //na casten gaat die op cooldown
                                    CooldownHandler.Instance.PutOnCooldown(this);
                                    // globalCooldown = 0.1f;
                                    CooldownHandler.alreadyCasting = 1f;
                                    alreadyglobal = false;
                                    onlyonce = false;
                                    yield return null;
                                }
                                else
                                {
                                    alreadyglobal = false;
                                    onlyonce = false;
                                }
                            }

                            if (this.abilityType == 4)
                            {

                                if (energy >= ultimaterequirement)
                                {
                                    Cast();
                                    energy -= ultimateAbilManaCost;
                                    //na casten gaat die op cooldown
                                    CooldownHandler.Instance.PutOnCooldown(this);
                                    // globalCooldown = 0.1f;
                                    CooldownHandler.alreadyCasting = 1f;
                                    alreadyglobal = false;
                                    onlyonce = false;
                                    yield return null;
                                }
                                else
                                {
                                    alreadyglobal = false;
                                    onlyonce = false;
                                }
                            }
                        }

                        /*
                            would double cast both global and ability , thus we reset if already global cast
                        */
                        else //fixes bug of double aa in p1
                        {
                            Debug.Log("THIS IS P1 BUG");
                            onlyonce = false;
                            alreadyglobal = false;
                        }
                    }

                }


                else
                {
                    //THIS WAS SUPER BAD DONT WAIT!!!                                       
                    // yield return new WaitForSeconds(CooldownHandler.alreadyCasting);

                  //  yield return new WaitForSeconds(globalCooldown); //TODO THIS WAS NOT HERE AND ONLY EDITED IN 14-12-2022, can delete 
                    Debug.Log("casted");

                    //casting ability
                    if (this.abilityType == 1)
                    {

                        Cast();
                        CooldownHandler.Instance.PutOnCooldown(this);
                        energy += basicAttackManaGain;
                        //na casten gaat die op cooldown

                        // globalCooldown = 0.1f;
                        CooldownHandler.alreadyCasting = 1f;
                        onlyonce = false;
                        yield return null;
                    }
                    if (this.abilityType == 2)
                    {

                        if (energy >= basicrequirement)
                        {
                            if (this.AbilityName == furioushitabil && GetComponent<FuriousHit>().cooldownFireBreath >= 0.4f ||this.abilityName == chargeshotabil && !GetComponent<MarcoMovementController>().isGrounded)
                            {
                                //risky may cause bug
                                onlyonce = false;
                            }
                            else
                            {
                                Cast();
                                energy -= basicAbilManaCost;
                                //na casten gaat die op cooldown
                                CooldownHandler.Instance.PutOnCooldown(this);
                                CooldownHandler.alreadyCasting = 1f;
                                onlyonce = false;
                                alreadyglobal = false;
                                yield return null;
                            }
                        }
                        if (energy < basicrequirement)
                        {

                            alreadyglobal = false;
                            onlyonce = false;
                        }
                    }
                    if (this.abilityType == 3)
                    {
                        // implicatie
                        if (energy >= thresholdrequirement )
                        {
                            Cast();
                            energy -= thresholdAbilManaCost;
                            //na casten gaat die op cooldown
                            CooldownHandler.Instance.PutOnCooldown(this);
                            //   globalCooldown = 0.1f;
                            CooldownHandler.alreadyCasting = 1f;
                            onlyonce = false;
                            alreadyglobal = false;
                            yield return null;
                        }
                        else
                        {
                            onlyonce = false;
                            alreadyglobal = false;
                        }
                    }

                    if (this.abilityType == 4)
                    {

                        if (energy >= ultimaterequirement)
                        {
                            Cast();
                            energy -= ultimateAbilManaCost;
                            //na casten gaat die op cooldown
                            CooldownHandler.Instance.PutOnCooldown(this);
                            //   globalCooldown = 0.1f;
                            CooldownHandler.alreadyCasting = 1f;
                            onlyonce = false;
                            alreadyglobal = false;
                            yield return null;
                        }
                        else
                        {
                            onlyonce = false;
                            alreadyglobal = false;
                        }
                    }

                }

            }

            else if (channel_ability_active )
            {
                onlyonce = false;
                alreadyglobal = false;
            }
        }
    }
}