using CreatingCharacters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CreatingCharacters.Abilities
{

    public class AnimationControl : ThirdPersonMovement
    {

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
            beamAbility = GetComponent<BeamAbility>();
        }
        public void dontAllowLeftRight()
        {
            if ((Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)))
            {
                anim.SetBool("isRunningRight", false);
                anim.SetBool("isRunningLeft", false);

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
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                anim.SetBool("isRunningBackRight", true);
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                anim.SetBool("isRunningBackLeft", true);
            }
            if (!(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
            {
                anim.SetBool("isRunningBackRight", false);
            }

            if (!(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
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

            if (!characterController.isGrounded)
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

            if (Input.GetKey(KeyCode.Space))
            {
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
            }

            else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))//&& !Input.GetKey("w"))//   && Input.GetKey("left shift"))
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isRunningLeft", true);
                anim.SetBool("isRunningRight", false);

                dontAllowLeftRight();
            }

            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) && !Input.GetKey("w"))  //&& Input.GetKey("left shift"))
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