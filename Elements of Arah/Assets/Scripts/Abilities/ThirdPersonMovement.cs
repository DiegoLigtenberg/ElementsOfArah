using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;
using UnityEngine.SceneManagement;


namespace CreatingCharacters.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class ThirdPersonMovement : MonoBehaviour
    {
             
        [SerializeField] protected float movementspeed = 6f;
        [SerializeField] protected float jumpForce = 4f;
        [SerializeField] protected float mass = 1f;
        [SerializeField] protected float damping = 5f;   //dit is een soort 'air resistnce'
        protected CharacterController characterController;
        public static float velocityY;
        protected Vector3 currentImpact;

        [SerializeField] private float turnspeed = 5f;

        //movementcontroller
        public float levitateDuration;
        public static bool isLevitating;
        //private readonly float gravity = Physics.gravity.y;
        public float gravity = -19.81f;

        private bool wasgrounded;  

        //dash variables
        public bool isChargingDash;
        public bool chargeDelay;

        private Vector3 movementInput;
        private Vector3 velocity;
        private float e;
        public static bool canmovecamera;

   

        protected virtual void Awake()
        {
             characterController = GetComponent<CharacterController>();

            resetPlayerStuff();
            Ability.globalCooldown = 0;
            e = (float)System.Math.E;


        }
        public void resetPlayerStuff()
        {
            AvatarMoveLocalPosUp.isRooted = false;

            P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
            P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
            HealthPlayer.playerisdeath = false;

            Phase01AA.filledspot1 = false;
            Phase01AA.filledspot2 = false;
            Phase01AA.filledspot3 = false;
            Phase01AA.filledspot4 = false;
            Phase01AA.filledspot5 = false;
            Phase01AA.filledspot6 = false;
            P1_Troll_Walk.fixbug = true;
        }

        private float newpos;
        private float RealYvelocity;
        public static float scaledvelocity;
        private float oldpos;
        float lastStep, timeBetweenSteps = 0.1f;
        float lastStep2, timeBetweenSteps2= 0.1f;

        private void LateUpdate()
        {
            if (Time.time + 0.005f - lastStep2 > timeBetweenSteps2)
            {
                lastStep2 = Time.time;
                newpos = this.transform.position.y;
                RealYvelocity = (newpos - oldpos) * 100f;
                scaledvelocity = 10 * (-0.5f + (1.0f / (1.0f + Mathf.Pow(e, -RealYvelocity / 150))));
                
            }
           

        }
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
        }
        protected virtual void Update()
        {
         

            if (Time.time - lastStep > timeBetweenSteps)
            {
              
                lastStep = Time.time;
                oldpos = this.transform.position.y;

            }
        


                Move();
                if (!HealthPlayer.playerisdeath)
                {
                    Jump();
                }


                //remember in trollcotnroller is er ook een reset knop! verander die samen!
                if (Input.GetKey(KeyCode.Tab))
                {
                    phasingToMiddle.transition_contact_TC = false;

                    //rocks visual not gona for instakill
                    P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
                    P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
                    P1_Troll_Walk.fixbug = true;

                    resetPlayerStuff();
                    Phase01AA.amntMinions = 0;
                    HealthPlayer.playerisdeath = false;
                    SceneManager.LoadScene("Saved");
                }

            
        }
        
        protected virtual void Move()
        {

            //niet in dash abilility
            
            //normal movement
            if (Ability.animationCooldown <= 0)
            {
                if (!HealthPlayer.playerisdeath)
                {
                    movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                    movementInput = transform.TransformDirection(movementInput);


                    velocity = movementInput.normalized * movementspeed + Vector3.up * velocityY;
                }
                else
                {
                    if (velocity.y < 0)
                    {
                        if (!ThirdPersonMovement.isLevitating)
                        {
                            velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp(velocity.y, -3, 0.999f * velocity.y) - 0.1f, 0.97f * velocity.z);
                        }
                        else
                        {
                            velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp(velocity.y, -3, 0.999f * velocity.y) - 0.1f, 0.97f * velocity.z);
                        }
                       
                    }
                    else
                    {
                        velocity.y *= 0.7f;
                        velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp(velocity.y, -3, 2) - 0.17f, 0.97f * velocity.z);
                    }
                    
                }
            }

            //movement in air when casting / rooted
            else if (Ability.animationCooldown >= 0 && !characterController.isGrounded)
            {
                if (!HealthPlayer.playerisdeath)
                {
                    movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

                    movementInput = transform.TransformDirection(movementInput);

                    velocity = (0.3f * movementInput.normalized) * movementspeed + Vector3.up * velocityY;
                }
                else
                {
                    if (velocity.y < 0)
                    {
                        if (!ThirdPersonMovement.isLevitating)
                        {
                            velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp(velocity.y, -3, 0.999f * velocity.y) -0.1f, 0.97f * velocity.z);
                        }
                        else
                        {
                            velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp(velocity.y, -3, 0.999f * velocity.y) - 0.15f, 0.97f * velocity.z);
                        }
                    }
                    else
                    {
                        velocity.y *= 0.7f;
                        velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp(velocity.y, -3, 2) - 0.17f, 0.97f * velocity.z);
                    }
                }
            }

            //movement on ground when casting / rooted
            else if (Ability.animationCooldown > 0 && characterController.isGrounded)
            {
                // Debug.Log(Ability.animationCooldown);
                if (!HealthPlayer.playerisdeath)
                {
                    movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

                    movementInput = transform.TransformDirection(movementInput);

                    velocity = (0f * movementInput.normalized) * movementspeed + Vector3.up * velocityY;
                }
                else
                {
                    if (velocity.y < 0)
                    {
                        if (!ThirdPersonMovement.isLevitating)
                        {
                            velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp(velocity.y, -3, 0.999f * velocity.y)-0.1f, 0.97f * velocity.z);
                        }
                        else
                        {
                            velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp(velocity.y, -3, 0.999f * velocity.y) - 0.15f, 0.97f * velocity.z);
                        }
                    }
                    else
                    {
                        velocity.y *= 0.7f;
                        velocity = new Vector3(0.97f * velocity.x, Mathf.Clamp (velocity.y, -3,2) - 0.17f, 0.97f * velocity.z);
                    }
                }
            }
            

            //reset velocity y als je op de grond komt naar 0 (geen bounce)
            if (characterController.isGrounded && velocityY < 0f)
            {
                velocityY = 0f;
            }

            //set velocity Y
            velocityY += gravity * Time.deltaTime;

          //  Vector3 velocity = movementInput.normalized * movementspeed + Vector3.up * velocityY;
                   

            //zorgt ervoor dat je niet slide als je door iets kleins gehit wordt
            if (currentImpact.magnitude > 0.2f)
            {
                velocity += currentImpact;
            }

            //jump
            if (!chargeDelay)
            {
               characterController.Move(velocity * Time.deltaTime);             
            }
          
            //dit is je 'drag'
            currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, damping * Time.deltaTime);

        }

        protected virtual void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) )
            {
                if (characterController.isGrounded && !isChargingDash)
                {
                    AddForce(Vector3.up, jumpForce);
                }
            }
        }

        public void AddForce(Vector3 direction, float magnitude)
        {
            //hoe groter mass -> hoe kleiner impact
            currentImpact += direction.normalized * magnitude / mass;

        }

        public void ResetImpact()
        {
            currentImpact = Vector3.zero;
            velocityY = 0f;
        }

        public void ResetImpactY()
        {
            currentImpact.y = 0;
            velocityY = 0f;
        }

    }
}
