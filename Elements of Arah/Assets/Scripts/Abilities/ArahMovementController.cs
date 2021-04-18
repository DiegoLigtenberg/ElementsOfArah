using CreatingCharacters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CreatingCharacters.Abilities
{
    public class ArahMovementController : ThirdPersonMovement
    {

        // protected float levitateDuration;
        public Animator anim;
        private int jumpCount = 0;
        GameObject fireJetPack;
        private DashAbility dash;
        private BeamAbility beam;
        public int energyCostJump;
        public float energyCostLevitating;

        ThirdPersonMovement thirdPersonPlayer;

        private float lastStepa, timeBetweenStepsa = 0.1f;

        void onground()
        {
            jumpCount = 0;
        }

        public float distplayerboss;



        private void Start()
        {
            dash = GetComponent<DashAbility>();
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
            beam = GetComponent<BeamAbility>();
            fireJetPack = GameObject.Find("FireSpawn");
            fireJetPack.SetActive(false);
        }

        public IEnumerator putOffLevitation()
        {
            yield return new WaitForSeconds(0.3f);
            isLevitating = false;
            fireJetPack.SetActive(false);
            gravity = -19.81f;
        }


        protected override void Update()
        {

            if (!PauseMenu.GameIsPaused)
            {
                base.Update();


                if (characterController.isGrounded)
                {
                    // Debug.Log(characterController.isGrounded);
                    thirdPersonPlayer.AddForce(-thirdPersonPlayer.transform.up, 0.3f);

                    anim.SetBool("IsGrounded", true);
                }

                if (!characterController.isGrounded)
                {
                    // Debug.Log("jumping");
                    //   gravity = -9.81f;
                    anim.SetBool("IsGrounded", false);
                }



                if (isLevitating && dash.isactivated)
                {
                    fireJetPack.SetActive(false);
                }
                if (isLevitating && !dash.isactivated)
                {
                    fireJetPack.SetActive(true);

                    if (Time.time - lastStepa > timeBetweenStepsa)
                    {
                        lastStepa = Time.time;

                        if (CooldownHandler.outOfCombat) { Ability.energy -= (energyCostLevitating / 2); }
                        else { Ability.energy -= energyCostLevitating; }

                        if (Ability.energy <= 0)
                        {
                            StartCoroutine(putOffLevitation());
                        }
                    }
                }



                if (characterController.isGrounded)
                {

                    isLevitating = false;
                    fireJetPack.SetActive(false);

                    gravity = -19.81f;
                    Invoke("onground", 0.1f);

                }
            }
        }


        public IEnumerator getBigger()
        {
            if (GetComponent<DashAbility>().isactivated == false)
            {

                if (!characterController.isGrounded)
                {
                    characterController.height = 1.6f;
                    yield return new WaitForSeconds(0.1f);
                    characterController.height = 1.7f;
                    yield return new WaitForSeconds(0.1f);
                    characterController.height = 1.8f;
                    yield return new WaitForSeconds(0.1f);
                    characterController.height = 1.9f;
                    yield return new WaitForSeconds(0.1f);
                    characterController.height = 2f;

                }
                else
                {
                    characterController.height = 2f;
                }


                yield return null;
            }

        }


        protected override void Jump()
        {

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (isLevitating)
                {
                    isLevitating = false;
                    fireJetPack.SetActive(false);
                    gravity = -19.81f;
                }
            }



            if (Input.GetKeyDown(KeyCode.Space) && !AvatarMoveLocalPosUp.isRooted && !beam.usingBeamP && !isChargingDash)
            {
                // characterController.height = 1.4f;
                // StartCoroutine(getBigger());

                if (jumpCount == 0)
                {

                    ResetImpactY();

                    if (Ability.energy > 0)
                    {
                        if (characterController.isGrounded)
                        {
                            if (Ability.energy >= 30)
                            {
                                anim.SetTrigger("isJumping");
                                AddForce(Vector3.up, 2.5f * jumpForce);
                                if (CooldownHandler.outOfCombat) { Ability.energy -= (energyCostJump / 2); }
                                else { Ability.energy -= energyCostJump; };
                                jumpCount = 1;
                            }
                        }
                        else
                        {
                            if (Ability.energy > 0)
                            {
                                // Debug.Log("this WORKS");
                                isLevitating = true;
                                //levitating (levitate duration the higher the faster gravity drops!
                                gravity = Mathf.Lerp(-1, -19.81f, Time.deltaTime * levitateDuration);


                                //fire animation
                                fireJetPack.SetActive(true);

                                ResetImpactY();
                                AddForce(Vector3.up, 2f * jumpForce);
                                jumpCount = 2;
                            }
                        }
                    }

                }
                else if (jumpCount == 1)
                {
                    jumpCount = 2;
                }
                else if (jumpCount == 2)
                {
                    //isLevitating = false;

                }
                /*
                     //hitting spacebar to quit levitate
                     else if(jumpCount == 2)
                     {
                         isLevitating = false;
                         gravity = -9.81f;
                     }
                */

            }
        }
    }
}

