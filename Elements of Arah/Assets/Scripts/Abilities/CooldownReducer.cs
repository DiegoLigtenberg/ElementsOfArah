using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class CooldownReducer : MonoBehaviour
    {

        float lastStep, timeBetweenSteps = 0.1f;
       private bool onlyonce;

        public static bool shortBuff1;
        public static bool shortBuff2;
        public static bool shortBuff3;

        public GameObject[] circlingBalls;
        public int orbCount;

        private float outofcombatmultiplier;

        private void Start()
        {
            Ability.energy = 100;
           // amount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (CooldownHandler.outOfCombat) { outofcombatmultiplier = 6.0f;}
            else { outofcombatmultiplier = 1f; }
           
            if (Ability.animationCooldown >= 0) { Ability.animationCooldown -= Time.deltaTime; }
            if (Ability.globalCooldown >= 0) { Ability.globalCooldown -= Time.deltaTime; }
            if (Ability.tickCooldown >= 0) { Ability.tickCooldown -= Time.deltaTime; }
           
            // Debug.Log(Ability.globalCooldown);
            if (Time.time - lastStep > timeBetweenSteps)
            {
                lastStep = Time.time;
                if (Ability.energy < 100)
                {
                    if (!ThirdPersonMovement.isLevitating)
                    {
                        Ability.energy += (.33f *outofcombatmultiplier);
                    }
                 
                }

            }
        }

            /*
            if (GameObject.Find("heraklios_a_dizon@Jumping (2)").GetComponent<DashAbility>().isactivated)
            {
                circlingBalls[0].SetActive(false);
                circlingBalls[1].SetActive(false);
                circlingBalls[2].SetActive(false);
            }
            else
            {

                if (shortBuff1 || shortBuff2 || shortBuff3)
                {

                    /*
                    if (remainingDashes == 0)
                    {
                        circlingBalls[0].SetActive(false);
                        circlingBalls[1].SetActive(false);
                        circlingBalls[2].SetActive(false);
                        orbCount = 1;

                    }
                    //////////////////////////////////////////////
                    ///

                    if (basicattackTypeBooster == 1)
                    {
                        circlingBalls[0].SetActive(true);

                        circlingBalls[1].SetActive(false);
                        circlingBalls[2].SetActive(false);
                        orbCount = 1;
                    }

                    if (basicattackTypeBooster == 2)
                    {
                        circlingBalls[0].SetActive(true);
                        circlingBalls[1].SetActive(true);

                        circlingBalls[2].SetActive(false);
                        orbCount = 2;
                    }
                    if (basicattackTypeBooster == 3)
                    {
                        circlingBalls[0].SetActive(true);
                        circlingBalls[1].SetActive(true);
                        circlingBalls[2].SetActive(true);
                        orbCount = 3;
                    }
                }


                else
                {
                    circlingBalls[0].SetActive(false);
                    circlingBalls[1].SetActive(false);
                    circlingBalls[2].SetActive(false);
                    orbCount = 0;
                }

            }




            if (targetTime1 > 0)
            {
                targetTime1 -= Time.deltaTime;
                if (targetTime2 <= 0 && targetTime3 <= 0)
                {
                    basicattackTypeBooster = 1;
                }
            }
            if (targetTime2 > 0)
            {
                targetTime2 -= Time.deltaTime;

                if (targetTime3 <= 0)
                {
                    basicattackTypeBooster = 2;
                }
           
            }
            if (targetTime3 > 0)
            {
                targetTime3 -= Time.deltaTime;
                
                basicattackTypeBooster = 3;
            }



            if (targetTime1 <= 0.0f)
            {
                shortBuff1 = false;
            }

            if (targetTime2 <= 0.0f)
            {
                shortBuff2 = false;
            }
            if (targetTime3 <= 0.0f)
            {
                shortBuff3 = false;
            }


        }
        
        public int basicattackTypeBooster;

        private int amount;

        public float targetTime1;
        public float targetTime2;
        public float targetTime3;

        
        public IEnumerator ShortBuff( int abilitytype)
        {

            if (abilitytype == 2)
            {

                shortBuff1 = true;
                targetTime1 = 1f;
            }

            if (abilitytype == 3)
            {

                shortBuff2 = true;
                targetTime2 = 1f;
            }
            if (abilitytype == 4)
            {

                shortBuff3 = true;
                targetTime3 = 1f;
            }
            
            yield return null;
            
        }
        */

            public void setGlobalcd()
        {
            if (!onlyonce)
            {
                onlyonce = true;
                StartCoroutine(GlobalCd());
            }
        }
        private IEnumerator GlobalCd()
        {
            yield return new WaitForSeconds(0.0f);       
            
                Ability.globalCooldown = 0.9f;
                onlyonce = false;
            }
        }
        
    }
