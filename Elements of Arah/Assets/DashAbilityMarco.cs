using CreatingCharacters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

namespace CreatingCharacters.Abilities
{
    [RequireComponent(typeof(ThirdPersonMovement))]
    public class DashAbilityMarco : Ability
    {
        public Animator anim;
        [SerializeField] private float dashForce;
        [SerializeField] private float dashDuration;
        public CinemachineFreeLook fl;
        private ThirdPersonMovement thirdPersonPlayer;
        [SerializeField] private CharacterController charController;
        public GameObject dashdirection;
        public GameObject self;
        private Vector3 dashdir;
        public GameObject dashcampos;
        private void Awake()
        {
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
        }
        private Vector3 dashtransform;

        // Start is called before the first frame update
        void Start()
        {
            dashtransform = transform.position;

        }

        public override void Cast()
        {
            StartCoroutine(Dash());

        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                anim.SetTrigger("Teleport");
            }
            dashdir = (self.transform.position - dashdirection.transform.position); //.normalized;
                                                                                    //  Debug.Log(self.transform.position);
                                                                                    // Debug.Log(dashdirection.transform.position);
            dashtransform = GameObject.Find("dashcampos").transform.position;
           // Debug.Log(dashtransform);

            // Debug.Log(Camera.main.transform.eulerAngles);
        }

        public IEnumerator Dash()
        {
            thirdPersonPlayer.ResetImpactY();
            thirdPersonPlayer.gravity = 0;
            dashtransform = GameObject.Find("dashcampos").transform.position;

         //   charController.enabled = false;
            yield return new WaitForSeconds(0.2f);
            GetComponent<MarcoMovementController>().jumptimer = 2f; //this is a jump
            fl.m_Priority = 11;
 
            yield return new WaitForSeconds(0.2f);
            thirdPersonPlayer.gravity = thirdPersonPlayer.gravity * 2;
            thirdPersonPlayer.gravity = -9.81f;

            float magnitude = 0;
           // charController.enabled = true;
            thirdPersonPlayer.ResetImpactY();
            thirdPersonPlayer.AddForce(dashdir, dashForce);
            yield return new WaitForSeconds(0.5f);
            fl.m_Priority = 9;
 
            /*
            if (Camera.main.transform.eulerAngles.x < 58 && !thirdPersonPlayer.isGrounded)
            {
               // thirdPersonPlayer.ResetImpactY();
               // thirdPersonPlayer.AddForce(dashdir, dashForce);


                magnitude = Camera.main.transform.eulerAngles.x * 0.8f;                     //general movement in camera direction
                thirdPersonPlayer.AddForce(transform.forward.normalized, magnitude * -0.5f);  //move backwards relative to player
                thirdPersonPlayer.AddForce(Vector3.up, magnitude * -0.3f);                  //move upwards
            }
            thirdPersonPlayer.AddForce(Camera.main.transform.forward, -magnitude);
            */
            yield return null;

        }

    }
}