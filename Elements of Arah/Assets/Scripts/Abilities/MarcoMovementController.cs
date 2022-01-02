using CreatingCharacters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;


namespace CreatingCharacters.Abilities
{
    public class MarcoMovementController : ThirdPersonMovement
    {

        public Animator anim;
        private int jumpCount = 0;
        public int energyCostJump;
        public float energyCostLevitating;
        public Animator animator;
        ThirdPersonMovement thirdPersonPlayer;
        private float lastStepm, timeBetweenStepsm = 0.1f;
        public float jumptimer;
        public float distanceToGround;

        void onground()
        {
            jumpCount = 0;
        }


        private void Start()
        {
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
            //    fireJetPack = GameObject.Find("FireSpawn");
            //   fireJetPack.SetActive(false);

        }


        public void movementAnimation()
        {
            anim.SetFloat("velocityX", Input.GetAxisRaw("Horizontal"), 0.2f, Time.deltaTime);
            anim.SetFloat("velocityZ", Input.GetAxisRaw("Vertical"), 0.2f, Time.deltaTime);
        }
        protected override void Update()
        {
            movementAnimation();
            landOnGround();
            if (Input.GetKey(KeyCode.C))
            {
                anim.SetBool("crunch_shot", true);
            }
            if (!PauseMenu.GameIsPaused)
            {
                base.Update();

                if (jumptimer > 0)
                {
                    jumptimer -= Time.deltaTime;
                }

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

                if (characterController.isGrounded)
                {
                    gravity = -19.81f;
                    Invoke("onground", 0.1f);

                }
            }

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                {
                    distanceToGround = hit.distance;
                }
            }


            if (Input.GetKeyDown(KeyCode.O))
            {
                anim.SetTrigger("Explosion Shot");
            }

        }




        public IEnumerator castTime()
        {

            animator.SetBool("castTime", true);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("casted", true);
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("castTime", false);
            animator.SetBool("casted", false);
            animator.SetBool("jump", false);
            animator.ResetTrigger("isJumping");
        }



        public void landOnGround()
        {
            if(distanceToGround < 2.75 && distanceToGround > 0.3f && !isGrounded)
            {
                anim.SetBool("landGround",true);
            }
            else
            {
                anim.SetBool("landGround", false);
            }

        }


        public IEnumerator removeJump()
        {
            anim.SetBool("jump", true);
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("jump", false);

        }

        protected override void Jump()
        {

            if (Input.GetKeyDown(KeyCode.Space) && !AvatarMoveLocalPosUp.isRooted)
            {
                jumptimer = 1.5f;
                if (jumpCount == 0)
                {
                    ResetImpactY();

                    if (Ability.energy > 0)
                    {
                        if (characterController.isGrounded)
                        {
                            if (Ability.energy >= 30)
                            {
                                StartCoroutine(removeJump());
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

                                //levitating (levitate duration the higher the faster gravity drops!
                                gravity = Mathf.Lerp(-10, -19.81f, Time.deltaTime * levitateDuration);
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


                }


            }
        }
    }
}

