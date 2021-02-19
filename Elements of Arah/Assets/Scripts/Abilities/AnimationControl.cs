using CreatingCharacters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CreatingCharacters.Abilities
{

    public class AnimationControl : ThirdPersonMovement
    {


        float lastStep, timeBetweenSteps = 0.2f;

        public MeshRenderer mr;

        static Animator anim;

        //dezwe 2 var bepalen nu of je voor of achteruit loopt -> moet aanpassen
        public float speed = 10.0f;
        public float rotationSpeed = 100.0f;

        public GameObject[] effect;
        public Transform[] effectTransform;

        private DashAbility dashAbility;

        private BeamAbility beamAbility;

        private void Start()
        {
           
            anim = GetComponent<Animator>();

            dashAbility = GetComponent<DashAbility>();
           beamAbility= GetComponent<BeamAbility>();
        }
        void delay()
        {

        }

        void Portal()
        {
            //Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);

            //geel blokje voor direction hoeft niet meer
           // mr.enabled = false;
        }

        public void dontAllowLeftRight()
        {
            if ((Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)))
            {
                anim.SetBool("isRunningRight", false);
                anim.SetBool("isRunningLeft", false);

            }
        }

        private void Abilities()
        {
            /*
            if (Input.GetKeyDown("1"))
            {
                anim.SetInteger("skillNumber", 1);
                anim.SetTrigger("playSkill");
                Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);


            }
            */
            if (Input.GetKeyDown("2") || Input.GetKeyDown(KeyCode.Mouse0))
            {
              //  anim.SetInteger("skillNumber", 2);
             //   anim.SetTrigger("playSkill");
              //  Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
            }


            /*

            if (Input.GetKeyDown("g"))
            {
                anim.SetInteger("skillNumber", 3);
                anim.SetTrigger("playSkill");
                Instantiate(effect[11], effectTransform[11].position, effectTransform[11].rotation);
                Instantiate(effect[8], effectTransform[8].position, effectTransform[8].rotation);
            }
            */

            /*
            if (Input.GetKey("g"))
            {

                Instantiate(effect[9], effectTransform[9].position, effectTransform[9].rotation);

                if (Time.time - lastStep > timeBetweenSteps)
                {
                    lastStep = Time.time;
                    Instantiate(effect[10], effectTransform[10].position, effectTransform[10].rotation);
                    Instantiate(effect[12], effectTransform[12].position, effectTransform[12].rotation);
                }
            }
            */


            /*
            if (Input.GetKeyDown("4"))
            {
                anim.SetInteger("skillNumber", 4);
                anim.SetTrigger("playSkill");

                
                Instantiate(effect[3], effectTransform[3].position, effectTransform[3].rotation);

                Instantiate(effect[4], effectTransform[4].position, effectTransform[4].rotation);
                Instantiate(effect[5], effectTransform[5].position, effectTransform[5].rotation);
                Instantiate(effect[6], effectTransform[6].position, effectTransform[6].rotation);
           
            }
                 */


            if (Input.GetKeyDown("b") && dashAbility.isactivated == false && dashAbility.remainingDashes >0)
            {
                //Debug.Log(dashAbility.isactivated);
                //Debug.Log(dashAbility.remainingDashes);
              
               // mr.enabled = true;
                //Instantiate(effect[7], effectTransform[7].position, effectTransform[7].rotation);
                Invoke("Portal", 0.7f);
            }

        }



        private bool wasonground;

        private void LateUpdate()
        {
           
            if (!isLevitating)
            {
                anim.SetBool("isLevitating", false);
            }


            if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool("isRunningRight", true);
                dontAllowLeftRight();
            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetBool("isRunningLeft", true);
                dontAllowLeftRight();
            }

            ///////////// this upper part is not done for arrow keys - small detail
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) )
            {
                anim.SetBool("isRunningBackRight", true);
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                anim.SetBool("isRunningBackLeft", true);
            }
            if (! ( Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
            {
                anim.SetBool("isRunningBackRight", false);
            }

            if (! (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
            {
                anim.SetBool("isRunningBackLeft", false);
            }
            ////////////


            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
               // Debug.Log("set false");
                anim.SetBool("isRunningLeft", false);
       
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                anim.SetBool("isRunningRight", false);
            
            }

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                anim.SetBool("isRunningBackwards", false);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                anim.SetBool("isRunningBackwards", true);

            }

            if (characterController.isGrounded && wasonground)
            {
                anim.SetTrigger("isIdle");
                anim.ResetTrigger("isJumping");
                wasonground = true;

            }

            if (!characterController.isGrounded )
            {
                anim.SetTrigger("isIdle");
                wasonground = false;
            }

        }

        protected override void Update()
        {



            //for animating forward or backwards
            float translation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            //transform.Translate(0, 0, translation);
            //transform.Rotate(0, rotation, 0);

            Abilities();
           // Debug.Log(translation);




            if (Input.GetKey(KeyCode.Space))
            {
                /*
                if (Input.GetKeyDown(KeyCode.Space) && !beamAbility.usingBeamP)
                {
                    Debug.Log(Ability.energy);
                    if (characterController.isGrounded)
                    {
                        if (Ability.energy  >= 10)
                        {
                            anim.SetTrigger("isJumping");
                        }
                 
                    }
                    
                }
                */

                 if (isLevitating)
                {
                    anim.ResetTrigger("isJumping");
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isLevitating", true);
                }


                if (!isLevitating)
                {
                    anim.SetBool("isLevitating", false);
                    if (Input.GetKey("w"))
                    {
                        anim.SetBool("isRunning", true);
                    }
                }
            }




            else if (translation != 0)
            {

                if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
                {
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }

                if (Input.GetKeyUp("w") || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    anim.SetBool("isRunning", false);
                }


                if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
                {
                    anim.SetBool("isRunningBackwards", true);

                    anim.SetBool("isIdle", false);
                }

                if (Input.GetKeyUp("s") || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    anim.SetBool("isRunningBackwards", false);
                }

                


                /*
                if (translation > 0.05)
                {
                    //forward
                    anim.SetBool("isRunning", true);
                }
                if (translation < -0.1)
                {
                    //backward
                    anim.SetBool("isRunningBackwards", true);
                }
                */
            }

       

            else if (Input.GetKey("a") ||Input.GetKey(KeyCode.LeftArrow) )//&& !Input.GetKey("w"))//   && Input.GetKey("left shift"))
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isRunningLeft", true);
                anim.SetBool("isRunningRight", false);

                dontAllowLeftRight();


            }

            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)&& !Input.GetKey("w"))  //&& Input.GetKey("left shift"))
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isRunningRight", true);
                anim.SetBool("isRunningLeft", false);
                dontAllowLeftRight();

            }

            else
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isRunningBackwards", false);

                anim.SetBool("isRunningLeft", false);
                anim.SetBool("isRunningRight", false);

                anim.SetBool("isLevitating", false);

            }
        }
    }
}